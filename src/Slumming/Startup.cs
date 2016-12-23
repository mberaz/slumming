using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Slumming.DAL.Interfaces;
using Slumming.DAL.Repositories;
using Slumming.Models;
using Slumming.Services;

namespace Slumming
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["Data:SlummingConnection:ConnectionString"];
            services.AddDbContext<SlummingContext>(options => options.UseSqlServer(connection));

            services.AddSingleton<IConfiguration>(Configuration);


            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IApartmentRepository, ApartmentRepository>();
            services.AddTransient<IDealRepository, DealRepository>();
            services.AddTransient<ISalesmanRepository, SalesmanRepository>();


            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<ISalesmanService, SalesmanService>();
            services.AddTransient<IDealService, DealService>();
            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }
}


