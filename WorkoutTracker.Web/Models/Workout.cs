using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.Web.Models
{
    public class Workout
    {
        public int Id { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; } = null!;

        [Required]
        public DateTime DatePerformed { get; set; }

        [Required]
        [Range(1, 100)]
        public int Sets { get; set; }

        [Required]
        [Range(1, 100)]
        public int Reps { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Weight { get; set; }

        [Range(0, 300)]
        public int? DurationMinutes { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
} 