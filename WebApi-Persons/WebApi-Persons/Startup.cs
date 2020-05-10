﻿using System;
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
        public Startup(IConfiguration configuration)
        {   //Inicializa a variável no construtor
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // É atribuida a string de conexão configurada no appsettings.json
            var connection = Configuration["MySqlConnection:MySqlConnectionString"];
            // Adiciona o contexto do MySQL
            services.AddDbContext<MySQLContext>(Options => Options.UseMySql(connection));

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
