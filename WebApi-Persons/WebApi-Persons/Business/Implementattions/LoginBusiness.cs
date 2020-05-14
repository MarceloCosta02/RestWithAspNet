using System.Collections.Generic;
using WebApi_Persons.Model;
using System.Threading;
using WebApi_Persons.Model.Context;
using System;
using System.Linq;
using WebApi_Persons.Repository;
using WebApi_Persons.Repository.Generic;
using WebApi_Persons.Data.Converters;
using WebApi_Persons.Data.VO;
using WebApi_Persons.Security.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

namespace WebApi_Persons.Business.Implementattions
{
    public class LoginBusiness : ILoginBusiness
    {

        private IUserRepository _repository;
        private SigninConfigurations _signinConfigurations;
        private TokenConfiguration _tokenConfiguration;
        
        public LoginBusiness(IUserRepository repository, 
                             SigninConfigurations signinConfigurations, 
                             TokenConfiguration tokenConfiguration)
        {
            _repository = repository;
            _signinConfigurations = signinConfigurations;
            _tokenConfiguration = tokenConfiguration;
        }
        
        public object FindByLogin(UserVO user)
        {
            bool credentialIsValid = false;

            if(user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = _repository.FindByLogin(user.Login);

                credentialIsValid = (baseUser != null && user.Login == baseUser.Login && user.AccessKey == baseUser.AccessKey);
            }
            if (credentialIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    }
                );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SucessObject(createDate, expirationDate,token);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signinConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,             
                message = "Failed to authenticate"
            };
        }

        private object SucessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
