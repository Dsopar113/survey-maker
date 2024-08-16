using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp1.Models;

namespace WebApp1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SurveyHeader> SurveyHeaders { get; set; }
        public DbSet<SurveySection> SurveySections { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Option> Options { get; set; }

    }
}
