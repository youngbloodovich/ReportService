using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using ReportService.Repositories.Entities;
using ReportService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ReportService.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<EmployeesRepository> _logger;

        public EmployeesRepository(
            IDbConnection dbConnection,
            ILogger<EmployeesRepository> logger)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Employee>> ReadAll()
        {
            IEnumerable<Employee> employees = null;
            try
            {
                _dbConnection.Open();

                var sqlQuery = "SELECT name as Name, inn as Inn, departmentid as DepartmentId from emps";
                employees = await _dbConnection.QueryAsync<Employee>(sqlQuery);

                _dbConnection.Close();
            }
            catch (Exception ex)
            {
                var message = "Error reading collection of employees";
                _logger.LogError(ex, message);
                throw new NpgsqlException(message, ex);
            }

            return employees;
        }
    }
}
