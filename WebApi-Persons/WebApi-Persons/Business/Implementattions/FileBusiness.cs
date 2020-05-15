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
using System.IO;

namespace WebApi_Persons.Business.Implementattions
{
    public class FileBusiness : IFileBusiness
    {
        public byte[] GetPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fulPath = path + "\\Other\\DiagramaAtividade-ProcessamentodeImagem.pdf";
            return File.ReadAllBytes(fulPath);
        }
    }
}
