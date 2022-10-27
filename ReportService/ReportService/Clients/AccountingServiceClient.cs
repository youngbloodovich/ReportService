using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReportService.Clients.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Clients
{
    public class AccountingServiceClient : IAccountingServiceClient
    {
        private readonly string _baseUrl;
        private readonly string _endpointUrl;

        private readonly ILogger<AccountingServiceClient> _logger;

        private readonly HttpClient _client = new HttpClient();

        public AccountingServiceClient(
            IConfiguration configuration,
            ILogger<AccountingServiceClient> logger)
        {
            _baseUrl = configuration.GetSection("AccountingService:BaseUrl").Value;
            _endpointUrl = configuration.GetSection("AccountingService:EndpointUrl").Value;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<int> GetSalary(string code)
        {
            // Полагаю, тут надо попросить команду, которая разрабатывает сервис бухгалтрии, добавить параметры год и месяц.
            // Иначе как бухгалтерия понимает - за какой период мы запрашиваем зарплату сотрудника?
            // Так же смущает что использовался POST-запрос. Какой в этом смысл?
            // Так же ошибочно использовался ИНН вместо кода сотрудника из кадровой службы.
            var uri = new Uri($"{_baseUrl}/{_endpointUrl}/{code}");

            string salary;
            try
            {
                salary = await _client.GetStringAsync(uri);
            }
            catch (Exception ex)
            {
                var message = "Request error to accounting service";
                _logger.LogError(ex, message);
                throw new HttpRequestException(message, ex);
            }

            return int.Parse(salary);
        }

    }
}
