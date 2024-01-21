using Healthy.Domain.Responses;
using HealthyBlazor.Model;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace HealthyBlazor.Services
{
    public class UserService : IUserService
    {
        HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<User>>("GetUsers");
            return response.Result.ToList();
        }

        public async Task<User> GetUserById(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Response<User>>("GetUserById?id=" + id);
            return response.Result.ToList().First();
        }

        public async Task<User> AddUser(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("CreateUser", user);

            var getResult = response.Content.ReadFromJsonAsync<Response<User>>().Result;

            return getResult.Result.First();
        }

        public async Task<User> EditUser(User user)
        {
            var response = await _httpClient.PutAsJsonAsync("EditUser", user);

            var getResult = response.Content.ReadFromJsonAsync<Response<User>>().Result;

            return getResult.Result.First();
        }

        public async Task<bool> DeleteUser(int idUser)
        {
            var jsonBody = new { id = idUser };

            // Convert the JSON object to a StringContent
            var content = new StringContent(JsonConvert.SerializeObject(jsonBody), Encoding.UTF8, "application/json");

            // Create a HttpRequestMessage with the DELETE method and content
            var request = new HttpRequestMessage(HttpMethod.Delete, "DeleteUser")
            {
                Content = content
            };

            // Make the request using HttpClient.SendAsync
            var response = await _httpClient.SendAsync(request);

            var getResult = response.Content.ReadFromJsonAsync<Response<User>>().Result;

            return true;
        }
    }
}
