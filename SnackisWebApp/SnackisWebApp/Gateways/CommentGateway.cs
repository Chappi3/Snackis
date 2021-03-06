using SnackisWebApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SnackisWebApp.Gateways
{
    public class CommentGateway
    {
        private readonly HttpClient _httpClient;

        public CommentGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Comment>> GetCommentByPostId(string postId)
        {
            return await _httpClient.GetFromJsonAsync<List<Comment>>(_httpClient.BaseAddress + "/Comments/PostId/" + postId);
        }

        public async Task<bool> CreateComment(string content, string postId, string userId)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/Comments", new {content, userId, postId});
            return response.IsSuccessStatusCode;
        }

        public async Task<Comment> GetCommentById(string commentId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Comment>(_httpClient.BaseAddress + "/Comments/" + commentId);
            }
            catch (JsonException)
            {
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<bool> DeleteComment(string commentId)
        {
            var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Comments/" + commentId);
            return response.IsSuccessStatusCode;
        }
    }
}
