using Dapper;
using ReportService.Domain;
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

        public EmployeesRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<IEnumerable<Employee>> ReadAll()
        {
            _dbConnection.Open();

            var sqlQuery = "SELECT name, inn, departmentid from emps";
            var employees = await _dbConnection.QueryAsync<Employee>(sqlQuery);

            _dbConnection.Close();

            return employees;
        }
    }
}
