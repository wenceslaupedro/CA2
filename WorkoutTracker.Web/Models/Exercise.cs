using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Web.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public string MuscleGroup { get; set; } = string.Empty;

        [Required]
        public string DifficultyLevel { get; set; } = string.Empty;

        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    }
} 