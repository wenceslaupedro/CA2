using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkoutTracker.Web.Models;

namespace WorkoutTracker.Web.Controllers
{
    [Route("Workouts")]
    public class WorkoutController : Controller
    {
        private static List<Exercise> _exercises = new List<Exercise>
        {
            new Exercise { Id = 1, Name = "Push-ups", MuscleGroup = "Chest", DifficultyLevel = "Beginner" },
            new Exercise { Id = 2, Name = "Squats", MuscleGroup = "Legs", DifficultyLevel = "Beginner" },
            new Exercise { Id = 3, Name = "Pull-ups", MuscleGroup = "Back", DifficultyLevel = "Intermediate" }
        };

        private static List<Workout> _workouts = new List<Workout>
        {
            new Workout 
            { 
                Id = 1, 
                ExerciseId = 1,
                Exercise = _exercises[0],
                DatePerformed = DateTime.Now.AddDays(-1),
                Sets = 3,
                Reps = 10,
                Weight = 0
            },
            new Workout 
            { 
                Id = 2, 
                ExerciseId = 2,
                Exercise = _exercises[1],
                DatePerformed = DateTime.Now.AddDays(-2),
                Sets = 4,
                Reps = 12,
                Weight = 20
            }
        };

        // GET: Workout
        [HttpGet]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View(_workouts);
        }

        // GET: Workout/Details/5
        [HttpGet("Details/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = _workouts.FirstOrDefault(w => w.Id == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workout/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["ExerciseId"] = new SelectList(_exercises, "Id", "Name");
            return View();
        }

        // POST: Workout/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ExerciseId,DatePerformed,Sets,Reps,Weight,DurationMinutes,Notes")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                workout.Id = _workouts.Max(w => w.Id) + 1;
                workout.Exercise = _exercises.FirstOrDefault(e => e.Id == workout.ExerciseId);
                _workouts.Add(workout);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseId"] = new SelectList(_exercises, "Id", "Name", workout.ExerciseId);
            return View(workout);
        }

        // GET: Workout/Edit/5
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = _workouts.FirstOrDefault(w => w.Id == id);
            if (workout == null)
            {
                return NotFound();
            }
            ViewData["ExerciseId"] = new SelectList(_exercises, "Id", "Name", workout.ExerciseId);
            return View(workout);
        }

        // POST: Workout/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ExerciseId,DatePerformed,Sets,Reps,Weight,DurationMinutes,Notes")] Workout workout)
        {
            if (id != workout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var index = _workouts.FindIndex(w => w.Id == id);
                if (index != -1)
                {
                    workout.Exercise = _exercises.FirstOrDefault(e => e.Id == workout.ExerciseId);
                    _workouts[index] = workout;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseId"] = new SelectList(_exercises, "Id", "Name", workout.ExerciseId);
            return View(workout);
        }

        // GET: Workout/Delete/5
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = _workouts.FirstOrDefault(w => w.Id == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST: Workout/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var workout = _workouts.FirstOrDefault(w => w.Id == id);
            if (workout != null)
            {
                _workouts.Remove(workout);
            }

            return RedirectToAction(nameof(Index));
        }
    }
} 