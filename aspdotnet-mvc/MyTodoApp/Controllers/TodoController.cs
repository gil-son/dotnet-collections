using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTodoApp.Data;
using MyTodoApp.Models;

namespace MyTodoApp.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

       private readonly ILogger<TodoController> _logger;

        public TodoController(ApplicationDbContext context, ILogger<TodoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
              return _context.Todos != null ? 
                          View(await _context.Todos
                          .AsNoTracking()
                          //Just the logged user can view yourself tasks
                          .Where( x => x.User == User.Identity.Name)
                          .ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Todos'  is null.");
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            // Take the logged user. So only the logged user can manage yourself task
            if (todo.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Done,CreatedAt,LastUpdateDate,User")] Todo todo)
        {
            // Take the logged user to attribute the name
            todo.User = User.Identity.Name;

            if (ModelState.IsValid)
            {

                _context.Add(todo);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Redirect to Index");

                return RedirectToAction(nameof(Index));
            
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError($"Model error: {error.ErrorMessage}");
                }
            }


            return View(todo);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            // Take the logged user. So only the logged user can manage yourself task
            if (todo.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Done,User")] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    todo.User = User.Identity.Name;
                    todo.LastUpdateDate = DateTime.Now;
                    

                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError($"Model error: {error.ErrorMessage}");
                }
            }
            return View(todo);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            // Take the logged user. So only the logged user can manage yourself task
            if (todo.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Todos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Todos'  is null.");
            }
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
          return (_context.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
