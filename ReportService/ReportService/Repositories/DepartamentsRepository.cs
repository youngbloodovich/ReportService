using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using ReportService.Clients;
using ReportService.Repositories.Entities;
using ReportService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ReportService.Repositories
{
    public class DepartamentsRepository : IDepartamentsRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<DepartamentsRepository> _logger;

        public DepartamentsRepository(
            IDbConnection dbConnection,
            ILogger<DepartamentsRepository> logger)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Departament>> ReadAll()
        {
            IEnumerable<Departament> departaments = null;
            try
            {
                _dbConnection.Open();

                var sqlQuery = "SELECT id as DepartmentId, name as Name, active as IsActive from deps";
                departaments = await _dbConnection.QueryAsync<Departament>(sqlQuery);

                _dbConnection.Close();
            }
            catch (Exception ex)
            {
                var message = "Error reading collection of departments";
                _logger.LogError(ex, message);
                throw new NpgsqlException(message, ex);
            }

            return departaments;
        }
    }
}
