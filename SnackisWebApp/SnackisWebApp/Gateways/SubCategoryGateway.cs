using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SnackisWebApp.Models;

namespace SnackisWebApp.Gateways
{
    public class SubCategoryGateway
    {
        private readonly HttpClient _httpClient;

        public SubCategoryGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SubCategory>> GetAllSubCategories()
        {
            return await _httpClient.GetFromJsonAsync<List<SubCategory>>(_httpClient.BaseAddress + "/SubCategories");
        }

        public async Task<SubCategory> GetSubcategoryById(string subcategoryId)
        {
            return await _httpClient.GetFromJsonAsync<SubCategory>(_httpClient.BaseAddress + "/SubCategories/" + subcategoryId);
        }

        public async Task<SubCategory> CreateSubCategory(string name, string categoryId)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/SubCategories", new{Name = name, CategoryId = categoryId});
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<SubCategory>();
        }

        public async Task<bool> DeleteSubCategory(string subCategoryId)
        {
            var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + "/SubCategories/" + subCategoryId);
            return response.IsSuccessStatusCode;
        }
    }
}
