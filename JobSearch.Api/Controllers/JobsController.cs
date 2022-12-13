using JobSearch.Api.Data;
using JobSearch.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private int numberOfRegistryByPage = 5;
        private readonly JobSearchContext _data;

        public JobsController(JobSearchContext data)
        {
            _data = data;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsAsync(string word = "", string cityState = "", int numberOfPage = 1)
        {
            word ??= string.Empty;
            cityState ??= string.Empty;

            var totalItems = _data.Jobs
                                .Where(lbda =>
                                    lbda.PublicationDate >= DateTime.Now.AddDays(-15) &&
                                    lbda.CityState.ToLower().Contains(cityState.ToLower()) &&
                                    (
                                        lbda.JobTitle.ToLower().Contains(word.ToLower()) ||
                                        lbda.TecnologyTools.ToLower().Contains(word.ToLower()) ||
                                        lbda.Company.ToLower().Contains(word.ToLower())
                                    )
                                ).Count();

            Response.Headers.Add("X-Total-Items", totalItems.ToString());

            var Jobs = await _data.Jobs
                              .Where(lbda =>
                                  lbda.PublicationDate >= DateTime.Now.AddDays(-15) &&
                                  lbda.CityState.ToLower().Contains(cityState.ToLower()) &&
                                  (
                                      lbda.JobTitle.ToLower().Contains(word.ToLower()) ||
                                      lbda.TecnologyTools.ToLower().Contains(word.ToLower()) ||
                                      lbda.Company.ToLower().Contains(word.ToLower())
                                  ))
                              .Skip(numberOfRegistryByPage * (numberOfPage - 1))
                              .Take(numberOfRegistryByPage)
                              .ToListAsync();

            return Ok(Jobs);
        }

        [HttpGet("{id:Int}")]
        public async Task<ActionResult<Job>> GetJobAsync(int id)
        {
            var jobDb = await _data.Jobs.FindAsync(id);

            if (jobDb == null) return NotFound();

            return Ok(jobDb);
        }

        [HttpPost]
        public async Task<ActionResult<Job>> AddJobAsync(Job job)
        {
            //TODO - Validações.

            var user = await GetUserById(job.UserId);

            if (user == null) return NotFound();

            job.User = user;
            job.PublicationDate = DateTime.Now;
            await _data.Jobs.AddAsync(job);
            await _data.SaveChangesAsync();

            return Ok(job);
        }

        private async Task<User> GetUserById(int id)
        {
            var user = await _data.Users.FirstOrDefaultAsync(lbda => lbda.Id == id);

            if (user != null)
                return user;

            return null;
        }

    }
}
