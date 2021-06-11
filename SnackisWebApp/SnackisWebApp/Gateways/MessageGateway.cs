using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace SnackisWebApp.Gateways
{
    public class MessageGateway
    {
        private readonly HttpClient _httpClient;

        public MessageGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetNumOfUnreadMessagesByUserId(string userId)
        {
            return await _httpClient.GetFromJsonAsync<int>(_httpClient.BaseAddress + "/Messages/Unread/" + userId);
        }

        public async Task<List<Message>> GetAllMessagesByUserId(string userId)
        {
            return await _httpClient.GetFromJsonAsync<List<Message>>(_httpClient.BaseAddress + "/Messages/UserId/" + userId);
        }

        public async Task EditMessage(string messageId, Message message)
        {
            await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + "/Messages/" + messageId, message);
        }
    }
}
