using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
 
namespace DotNetBelt.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options): base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Participant> Participants { get; set; }

    }
}   