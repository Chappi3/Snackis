using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SnackisWebApp.Models;

namespace SnackisWebApp.Gateways
{
    public class ReportGateway
    {
        private readonly HttpClient _httpClient;

        public ReportGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Report>> GetAllReports()
        {
            return await _httpClient.GetFromJsonAsync<List<Report>>(_httpClient.BaseAddress + "/Reports");
        }

        public async Task<bool> CreateReport(string content, string byUser, string postId, string commentId)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/Reports", new {byUser, content, postId, commentId});
            return response.IsSuccessStatusCode;
        }

        public async Task<Report> GetReportById(string reportId)
        {
            return await _httpClient.GetFromJsonAsync<Report>(_httpClient.BaseAddress + "/Reports/" + reportId);
        }
    }
}
