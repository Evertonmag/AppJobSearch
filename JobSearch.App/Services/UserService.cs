using JobSearch.App.Models;
using JobSearch.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public class UserService : Service
    {
        public async Task<ResponseService<User>> GetUser(string email, string password)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Users?email={email}&password={password}");

            ResponseService<User> responseService = new();
            responseService.Success = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<User>();
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<User>>(problemResponse);
                responseService.Errors = errors.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<User>> AddUser(User user)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Users", user);

            ResponseService<User> responseService = new();

            responseService.Success = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<User>();
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<User>>(problemResponse);
                responseService.Errors = errors.Errors;
            }
            return responseService;
        }
    }
}
