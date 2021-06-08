using SnackisWebApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SnackisWebApp.Gateways
{
    public class CategoryGateway
    {
        private readonly HttpClient _httpClient;

        public CategoryGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>(_httpClient.BaseAddress + "/Categories");
        }

        public async Task<Category> CreateCategory(string name)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/Categories", new { Name = name });
            
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            // Todo: return if success instead?
            return await response.Content.ReadFromJsonAsync<Category>();
        }

        public async Task<bool> DeleteCategory(string categoryId)
        {
            var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Categories/" + categoryId);
            return response.IsSuccessStatusCode;
        }

        public async Task<Category> GetCategoryById(string categoryId)
        {
            return await _httpClient.GetFromJsonAsync<Category>(_httpClient.BaseAddress + "/Categories/" + categoryId);
        }
    }
}
