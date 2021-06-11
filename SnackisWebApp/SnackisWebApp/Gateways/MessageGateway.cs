using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SnackisWebApp.Models;

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

        public async Task<bool> CreateMessage(string fromUserId, string toUserId, string content)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/Messages",
                new{ToUser = toUserId, FromUser = fromUserId, Content = content, SentAt = DateTime.Now, IsRead = false});
            return response.IsSuccessStatusCode;
        }

        public async Task EditMessage(string messageId, Message message)
        {
            await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + "/Messages/" + messageId, message);
        }
    }
}
