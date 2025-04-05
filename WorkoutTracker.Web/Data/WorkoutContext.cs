using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Web.Models;

namespace WorkoutTracker.Web.Data
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext(DbContextOptions<WorkoutContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.Workouts)
                .WithOne(w => w.Exercise)
                .HasForeignKey(w => w.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 