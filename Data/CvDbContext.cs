using Labb3Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Api.Data
{
    public class CvDbContext : DbContext
    {
        public CvDbContext(DbContextOptions<CvDbContext> options) : base(options) { }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }
    
    }
}
