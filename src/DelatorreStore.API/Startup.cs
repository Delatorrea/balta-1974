using System;
using DelatorreStore.Domain.StoreContext.Handlers;
using DelatorreStore.Domain.StoreContext.Repositories;
using DelatorreStore.Domain.StoreContext.Services;
using DelatorreStore.Infra.StoreContext.DataContexts;
using DelatorreStore.Infra.StoreContext.Repositories;
using DelatorreStore.Infra.StoreContext.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using DelatorreStore.Shared;

namespace DelatorreStore.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set;}
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc(mvc => mvc.EnableEndpointRouting = false);

            services.AddResponseCompression();

            services.AddScoped<DelatorreDataContext, DelatorreDataContext>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<CustomerHandler, CustomerHandler>();

            services.AddSwaggerGen();

            services.AddElmahIo(o =>
            {
                o.ApiKey = "5ca58aaecaaa43138b6ab566fe2b90c4";
                o.LogId = new Guid("fd379b3f-b8c2-47d3-8238-b6b8f71a9132");
            });

            Settings.ConnectionString = $"{Configuration["connectionString"]}";
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseElmahIo();

            app.UseMvc();
            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Delatorre Store - V1");
            });

        }
    }
}
