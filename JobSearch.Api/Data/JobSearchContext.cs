using JobSearch.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Api.Data
{
    public class JobSearchContext : DbContext
    {
        public JobSearchContext(DbContextOptions<JobSearchContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
