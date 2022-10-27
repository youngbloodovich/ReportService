using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<AccountingServiceClient> _logger;

        private readonly HttpClient _client = new HttpClient();

        public StaffServiceClient(
            IConfiguration configuration, 
            ILogger<AccountingServiceClient> logger)
        {
            _baseUrl = configuration.GetSection("StaffService:BaseUrl").Value;
            _endpointUrl = configuration.GetSection("StaffService:EndpointUrl").Value;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<string> GetCode(string inn)
        {
            var uri = new Uri($"{_baseUrl}/{_endpointUrl}/{inn}");

            string code;
            try
            {
                code = await _client.GetStringAsync(uri);
            }
            catch (Exception ex)
            {
                var message = "Request error to staff service";
                _logger.LogError(ex, message);
                throw new HttpRequestException(message, ex);
            }
            
            return code;
        }
    }
}
