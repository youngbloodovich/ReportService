using Dapper;
using ReportService.Domain;
using ReportService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ReportService.Repositories
{
    public class DepartamentsRepository : IDepartamentsRepository
    {
        private readonly IDbConnection _dbConnection;

        public DepartamentsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<IEnumerable<Departament>> ReadAll()
        {
            _dbConnection.Open();

            var sqlQuery = "SELECT id, name, active from deps";
            var departaments = await _dbConnection.QueryAsync<Departament>(sqlQuery);

            _dbConnection.Close();

            return departaments;
        }
    }
}
