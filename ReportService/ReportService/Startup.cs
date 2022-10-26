using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddTransient<IReportGetterService, ReportGetterService>();
            services.AddTransient<IReportBuilderService, ReportBuilderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
