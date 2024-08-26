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
        public DbSet<SurveyHeader> SurveyHeaders { get; set; }
        public DbSet<SurveySection> SurveySections { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<SurveyHeader_Participant> SurveyHeaders_Participants { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cascade delete from SurveyHeader to SurveySection
            modelBuilder.Entity<SurveySection>()
                .HasOne<SurveyHeader>()
                .WithMany(sh => sh.SurveySections)
                .HasForeignKey(ss => ss.SurveyHeaderId)
                .OnDelete(DeleteBehavior.Cascade);  // Enable cascade delete

            // Cascade delete from SurveySection to Question
            modelBuilder.Entity<Question>()
                .HasOne<SurveySection>()
                .WithMany(ss => ss.Questions)
                .HasForeignKey(q => q.SurveySectionId)
                .OnDelete(DeleteBehavior.Cascade);  // Enable cascade delete

            // Cascade delete from Participant to SurveyHeader_Participant (already implemented)
            modelBuilder.Entity<SurveyHeader_Participant>()
                .HasOne<Participant>()
                .WithMany(p => p.SurveyHeader_Participants)
                .HasForeignKey(shp => shp.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
