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

        public async Task<int> GetSalary(string code, int year, int month)
        {
            var uri = new Uri($"{_baseUrl}/{_endpointUrl}/{code}");

            var json = JsonConvert.SerializeObject(new { year, month });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);
            var salary = await response.Content.ReadAsStringAsync();

            return int.Parse(salary);
        }

    }
}
