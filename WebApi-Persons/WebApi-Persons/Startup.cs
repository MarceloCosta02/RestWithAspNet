using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi_Persons.Model.Context;
using WebApi_Persons.Business;
using WebApi_Persons.Business.Implementattions;
using WebApi_Persons.Repository;
using WebApi_Persons.Repository.Generic;
using WebApi_Persons.Repository.Implementattions;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using WebApi_Persons.Security.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_Persons
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {   
            //Inicializa as variáveis no construtor
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }
                
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // É atribuida a string de conexão configurada no appsettings.json
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            // Adiciona o contexto do MySQL
            services.AddDbContext<MySQLContext>(Options => Options.UseMySql(connectionString));

            // Chama um método para funcionar o Migration / Logger
            ExecuteMigrations(connectionString);

            // ---------------------------------------------------------------------------------
            // Inicia o código para funcionar o JWT TOKEN

            var signingConfigurations = new SigninConfigurations();
            // Adiciona a injeção de dependencia
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            // Pega do appsettings.json as configurações do Token
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                _configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            // Adiciona a injeção de dependencia
            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
            // ---------------------------------------------------------------------------------

            // Adiciona ao código o versionamento de Api's
            services.AddApiVersioning(option => option.ReportApiVersions = true);

            // Configuração do Swagger para documentação da API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "RESTFull API with ASP.NET Core 2.1",
                        Version = "v1"
                    });
            });

            // Adiciona o Content Negotiation a API, para receber os dados em xml e json
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));


            })
            // Desativado por enquanto o suporte a xml
            //.AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Injeção de dependência das classes PersonBusiness
            services.AddScoped<IPersonBusiness, PersonBusiness>();

            // Injeção de dependência das classes LoginBusiness
            services.AddScoped<ILoginBusiness, LoginBusiness>();

            // Injeção de dependência das classes UserRepository
            services.AddScoped<IUserRepository, UserRepository>();

            // Injeção de de dependência das classes BookBusiness
            services.AddScoped<IBookBusiness, BookBusiness>();

            // Injeção de dependência das classes Repository genéricas
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            // Injeção de de dependência das classes PersonRepository
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Injeção de de dependência das classes FileBusiness
            services.AddScoped<IFileBusiness, FileBusiness>();
        }

        private void ExecuteMigrations(string connectionString)
        {
            if (_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed", ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Iniciando Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //Iniciando a API na página do swagger
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
