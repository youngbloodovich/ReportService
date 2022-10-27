using Microsoft.Extensions.Configuration;
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

        private readonly HttpClient _client = new HttpClient();

        public AccountingServiceClient(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("AccountingService:BaseUrl").Value;
            _endpointUrl = configuration.GetSection("AccountingService:EndpointUrl").Value;
        }

        // Полагаю, тут надо попросить команду, которая разрабатывает сервис бухгалтрии, добавить параметры год и месяц.
        // Иначе как бухгалтерия понимает - за какой период мы запрашиваем зарплату сотрудника?
        // Так же смущает что использовался POST-запрос. Какой в этом смысл?
        // Так же ошибочно использовался ИНН вместо кода сотрудника из кадровой службы.
        public async Task<int> GetSalary(string code)
        {
            var uri = new Uri($"{_baseUrl}/{_endpointUrl}/{code}");

            var salary = await _client.GetStringAsync(uri);

            return int.Parse(salary);
        }

    }
}
