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
using WebApi_Persons.Repository.Implementattions;

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
                catch(Exception ex)
                {
                    _logger.LogCritical("Database migration failed", ex);
                    throw;
                }
            }

            // Adiciona ao código o versionamento de Api's
            services.AddApiVersioning();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Injeção de dependência das classes Business
            services.AddScoped<IPersonBusiness, PersonBusiness>();
            // Injeção de dependência das classes Repository
            services.AddScoped<IPersonRepository, PersonRepository>();

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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
