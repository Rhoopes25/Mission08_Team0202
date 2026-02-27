using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission8IS413.Models;
using System.Linq;

namespace Mission8IS413.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskContext _context;

        public HomeController(TaskContext context)
        {
            _context = context;
        }

        // GET: /Home/Quadrants
        public IActionResult Quadrants()
        {
            // include category navigation property so views can use TaskCategory?.CategoryName
            var tasks = _context.Tasks
                        .Include(t => t.TaskCategory)
                        .OrderBy(t => t.TaskDue)
                        .ToList();

            return View(tasks); // Views/Home/Quadrants.cshtml expects IEnumerable<TaskModel>
        }

        // ----- Add Task (GET) -----
        [HttpGet]
        public IActionResult AddTask()
        {
            // prepare SelectList for the category dropdown
            ViewBag.categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");

            // default model so the Quadrant select has a valid default value
            var model = new TaskModel
            {
                TaskQuadrant = 1,
                TaskCompleted = false
            };

            return View(model);
        }

        // ----- Add Task (POST) -----
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTask(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                // repopulate the SelectList and preserve selected category on validation error
                ViewBag.categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", model.CategoryId);
                return View(model);
            }

            _context.Tasks.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Quadrants));
        }

        // ----- Edit (GET) - supports both "Edit" and "EditTask" links -----
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return EditTask(id);
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            // provide SelectList with the current CategoryId selected
            ViewBag.categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", task.CategoryId);
            // reuse AddTask.cshtml as the editor
            return View("AddTask", task);
        }

        // ----- Edit (POST) -----
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTask(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                // repopulate SelectList preserving selection
                ViewBag.categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", model.CategoryId);
                return View("AddTask", model);
            }

            _context.Tasks.Update(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Quadrants));
        }

        // alias POST Edit -> EditTask (in case a form posts to Edit)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskModel model)
        {
            return EditTask(model);
        }

        // ----- Delete (GET) - your views use anchor tags, so support GET -----
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var t = _context.Tasks.Find(id);
            if (t != null)
            {
                _context.Tasks.Remove(t);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Quadrants));
        }

        [HttpGet]
        public IActionResult DeleteTask(int id)
        {
            return Delete(id);
        }

        // ----- Mark Complete (POST) -----
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkComplete(int id)
        {
            var t = _context.Tasks.Find(id);
            if (t != null)
            {
                t.TaskCompleted = true;
                _context.Tasks.Update(t);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Quadrants));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkTaskComplete(int id)
        {
            return MarkComplete(id);
        }
    }
}