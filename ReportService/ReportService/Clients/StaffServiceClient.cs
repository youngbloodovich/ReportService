using Microsoft.Extensions.Configuration;
using ReportService.Clients.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReportService.Clients
{
    public class StaffServiceClient : IStaffServiceClient
    {
        private readonly string _baseUrl;
        private readonly string _endpointUrl;

        private readonly HttpClient _client = new HttpClient();

        public StaffServiceClient(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("StaffService:BaseUrl").Value;
            _endpointUrl = configuration.GetSection("StaffService:EndpointUrl").Value;
        }

        public async Task<string> GetCode(string inn)
        {
            var uri = new Uri($"{_baseUrl}/{_endpointUrl}/{inn}");

            return await _client.GetStringAsync(uri);
        }
    }
}
