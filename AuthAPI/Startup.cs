﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using AuthAPI.UsersRepository;
using AuthAPI.Services;
using AuthAPI.Validators;
using Microsoft.Extensions.Configuration;
using System.IO;
using DatabaseAccess.Repository;
using DatabaseAccess.SpExecuters;

namespace AuthAPI
{
    /// <summary>
    /// Class containing start configuration
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

        /// <summary>
        /// Configures services
        /// This method gets called by the runtime which uses this method to add services to the container.
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // adding custom repository
            services.AddScoped<IUserRepository, UserRepository>();

            // adding mvc
            services.AddMvc();

            // adding custom configurations of Identity Server
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients())
                    .AddProfileService<ProfileService>();

            // adding transients
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();

            // adding singletons
            services.AddSingleton(new Repo<User>(
                new MapInfo(this.Configuration["Mappers:Users"]),
                new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));
        }

        /// <summary>
        /// Configures app and environment.
        /// This method gets called by the runtime which uses this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">App</param>
        /// <param name="env">Environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}
