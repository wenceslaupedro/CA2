using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkoutTracker.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutTracker.Web.Controllers
{
    public class WorkoutController : Controller
    {
        private static List<Workout> _workouts = new List<Workout>();
        private static List<Exercise> _exercises = new List<Exercise>
        {
            new Exercise { Id = 1, Name = "Push-ups", Description = "Bodyweight exercise for upper body" },
            new Exercise { Id = 2, Name = "Squats", Description = "Lower body exercise" },
            new Exercise { Id = 3, Name = "Pull-ups", Description = "Upper body pulling exercise" }
        };

        // GET: Workout
        public IActionResult Index()
        {
            return View(_workouts);
        }

        // GET: Workout/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = _workouts.FirstOrDefault(m => m.Id == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workout/Create
        public IActionResult Create()
        {
            ViewBag.ExerciseId = new SelectList(_exercises, "Id", "Name");
            return View();
        }

        // POST: Workout/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ExerciseId,DatePerformed,Sets,Reps,Weight,DurationMinutes,Notes")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                workout.Id = _workouts.Count > 0 ? _workouts.Max(w => w.Id) + 1 : 1;
                _workouts.Add(workout);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ExerciseId = new SelectList(_exercises, "Id", "Name", workout.ExerciseId);
            return View(workout);
        }

        // GET: Workout/Edit/5
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
            ViewBag.ExerciseId = new SelectList(_exercises, "Id", "Name", workout.ExerciseId);
            return View(workout);
        }

        // POST: Workout/Edit/5
        [HttpPost]
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
                    _workouts[index] = workout;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ExerciseId = new SelectList(_exercises, "Id", "Name", workout.ExerciseId);
            return View(workout);
        }

        // GET: Workout/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = _workouts.FirstOrDefault(m => m.Id == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST: Workout/Delete/5
        [HttpPost, ActionName("Delete")]
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