using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Web.Models;

namespace WorkoutTracker.Web.Controllers
{
    [Route("Exercises")]
    public class ExerciseController : Controller
    {
        private static List<Exercise> _exercises = new List<Exercise>
        {
            new Exercise { Id = 1, Name = "Push-ups", MuscleGroup = "Chest", DifficultyLevel = "Beginner" },
            new Exercise { Id = 2, Name = "Squats", MuscleGroup = "Legs", DifficultyLevel = "Beginner" },
            new Exercise { Id = 3, Name = "Pull-ups", MuscleGroup = "Back", DifficultyLevel = "Intermediate" }
        };

        // GET: Exercise
        [HttpGet]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View(_exercises);
        }

        // GET: Exercise/Details/5
        [HttpGet("Details/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = _exercises.FirstOrDefault(e => e.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercise/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercise/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description,MuscleGroup,DifficultyLevel")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                exercise.Id = _exercises.Max(e => e.Id) + 1;
                _exercises.Add(exercise);
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: Exercise/Edit/5
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = _exercises.FirstOrDefault(e => e.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercise/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,MuscleGroup,DifficultyLevel")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var index = _exercises.FindIndex(e => e.Id == id);
                if (index != -1)
                {
                    _exercises[index] = exercise;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: Exercise/Delete/5
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = _exercises.FirstOrDefault(e => e.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercise/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var exercise = _exercises.FirstOrDefault(e => e.Id == id);
            if (exercise != null)
            {
                _exercises.Remove(exercise);
            }

            return RedirectToAction(nameof(Index));
        }
    }
} 