using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SnackisWebApp.Models;

namespace SnackisWebApp.Gateways
{
    public class PostGateway
    {
        private readonly HttpClient _httpClient;

        public PostGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetAllPostsBySubcategoryId(string subcategoryId)
        {
            return await _httpClient.GetFromJsonAsync<List<Post>>(_httpClient.BaseAddress + "/Posts/subcategoryId/" + subcategoryId);
        }

        public async Task<bool> CreatePost(string title, string content, string userId, string subcategoryId)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/posts", new {Title = title, Content = content, SubCategoryId = subcategoryId, UserId = userId});
            return response.IsSuccessStatusCode;
        }
    }
}
