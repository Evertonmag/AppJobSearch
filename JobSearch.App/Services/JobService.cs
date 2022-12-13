using JobSearch.App.Models;
using JobSearch.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public class JobService : Service
    {
        public async Task<ResponseService<List<Job>>> GetJobs(string word = "", string cityState = "", int numberOfPage = 1)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Jobs?word={word}&cityState={cityState}&numberOfPage={numberOfPage}");

            ResponseService<List<Job>> responseService = new();
            responseService.Success = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<List<Job>>();

                var pagination = new Pagination()
                {
                    IsPagination = true,
                    TotalItems = int.Parse(response.Headers.GetValues("X-Total-Items").FirstOrDefault())
                };

                responseService.Pagination = pagination;
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<List<Job>>>(problemResponse);
                if (errors != null)
                {
                    responseService.Errors = errors.Errors;
                }
            }
            return responseService;
        }

        public async Task<ResponseService<Job>> GetJob(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Jobs/{id}");

            ResponseService<Job> responseService = new();
            responseService.Success = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Job>();
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<Job>>(problemResponse);
                responseService.Errors = errors.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<Job>> AddJob(Job job)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Jobs", job);

            ResponseService<Job> responseService = new();
            responseService.Success = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Job>();
            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<Job>>(problemResponse);
                responseService.Errors = errors.Errors;
            }
            return responseService;
        }
    }
}
