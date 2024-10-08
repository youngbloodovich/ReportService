﻿using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Npgsql;
using ReportService.Clients;
using ReportService.Clients.Interfaces;
using ReportService.Repositories;
using ReportService.Repositories.Interfaces;
using ReportService.Services;
using ReportService.Services.Interfaces;

namespace ReportService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTransient<IDbConnection, NpgsqlConnection>((sp) => {
                var connectionString = Configuration.GetConnectionString("Default");
                return new NpgsqlConnection(connectionString);
            });

            services.AddTransient<IStaffServiceClient, StaffServiceClient>();
            services.AddTransient<IAccountingServiceClient, AccountingServiceClient>();

            services.AddTransient<IDepartamentsRepository, DepartamentsRepository>();
            services.AddTransient<IEmployeesRepository, EmployeesRepository>();

            services.AddTransient<IReportBuilderService, ReportBuilderService>();
            services.AddTransient<IReportFormatterService, ReportFormatterService>();
            services.AddTransient<IFileCacheService, FileCacheService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            app.UseMvc();
        }
    }
}
