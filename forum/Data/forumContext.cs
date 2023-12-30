using forum.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace forum.Data
{
    public class forumContext : DbContext
    {
        public forumContext(DbContextOptions<forumContext> options) : base(options)
        {
        }

        public DbSet<Theme> Themes { get; set; }
    }
}