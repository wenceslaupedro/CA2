using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Web.Data;

namespace WorkoutTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalysisController : ControllerBase
    {
        private readonly WorkoutContext _context;

        public AnalysisController(WorkoutContext context)
        {
            _context = context;
        }

        [HttpGet("workout-stats")]
        public async Task<ActionResult<WorkoutStats>> GetWorkoutStats()
        {
            try
            {
                var workouts = await _context.Workouts
                    .Include(w => w.Exercise)
                    .ToListAsync();

                if (!workouts.Any())
                {
                    return Ok(new WorkoutStats
                    {
                        TotalWorkouts = 0,
                        TotalExercises = 0,
                        AverageSets = 0,
                        AverageReps = 0,
                        TotalVolume = 0,
                        MostPerformedExercise = null
                    });
                }

                var stats = new WorkoutStats
                {
                    TotalWorkouts = workouts.Count,
                    TotalExercises = workouts.Select(w => w.ExerciseId).Distinct().Count(),
                    AverageSets = workouts.Average(w => w.Sets),
                    AverageReps = workouts.Average(w => w.Reps),
                    TotalVolume = workouts.Sum(w => (decimal)(w.Sets * w.Reps * (w.Weight ?? 0))),
                    MostPerformedExercise = workouts
                        .GroupBy(w => w.Exercise.Name)
                        .OrderByDescending(g => g.Count())
                        .FirstOrDefault()?.Key
                };

                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class WorkoutStats
    {
        public int TotalWorkouts { get; set; }
        public int TotalExercises { get; set; }
        public double AverageSets { get; set; }
        public double AverageReps { get; set; }
        public decimal TotalVolume { get; set; }
        public string? MostPerformedExercise { get; set; }
    }
} 