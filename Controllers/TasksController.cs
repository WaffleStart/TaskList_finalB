using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using TaskList.Data;
using TaskList.Models;
using TaskList.ViewModels;

namespace TaskList.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks
                .Include(t => t.User)
                .Include(t => t.TaskTags)
                .ThenInclude(tt => tt.Tag)
                .Select(t => new TaskViewModel
                {
                    Task_Id = t.Task_Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    User = t.User,
                    Tags = t.TaskTags.Select(tt => tt.Tag.Name).ToList()
                })
                .ToListAsync();

            return View(tasks);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.User)
                .Include(t => t.TaskTags)
                .ThenInclude(tt => tt.Tag)
                .Select(t => new TaskViewModel
                {
                    Task_Id = t.Task_Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    User = t.User,
                    Tags = t.TaskTags.Select(tt => tt.Tag.Name).ToList()
                })
                .FirstOrDefaultAsync(m => m.Task_Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        public IActionResult Create()
        {
            ViewData["User_Id"] = new SelectList(_context.Users, "User_Id", "Email");
            ViewData["Tags"] = new MultiSelectList(_context.Tags, "Tag_Id", "Name");
            return View(new TaskViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.User_Id);
                if (user == null)
                {
                    ModelState.AddModelError("User_Id", "The selected user does not exist.");
                    ViewData["User_Id"] = new SelectList(_context.Users, "User_Id", "Email", model.User_Id);
                    ViewData["Tags"] = new MultiSelectList(_context.Tags, "Tag_Id", "Name", model.Tags);
                    return View(model);
                }

                var task = new Models.Task
                {
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Status = model.Status,
                    User = user,
                    User_Id = model.User_Id,
                    TaskTags = model.Tags?.Select(tagId => new TaskTag { Tag_Id = int.Parse(tagId), Tag = _context.Tags.Find(int.Parse(tagId)) }).ToList()
                };
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["User_Id"] = new SelectList(_context.Users, "User_Id", "Email", model.User_Id);
            ViewData["Tags"] = new MultiSelectList(_context.Tags, "Tag_Id", "Name", model.Tags);
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
            .Include(t => t.TaskTags)
                .ThenInclude(tt => tt.Tag)
            .Include(t => t.User)
            .SingleOrDefaultAsync(t => t.Task_Id == id);

            if (task == null)
            {
                return NotFound();
            }

            var model = new TaskViewModel
            {
                Task_Id = task.Task_Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                User = task.User,
                Tags = task.TaskTags?.Select(tt => tt.Tag_Id.ToString()).ToList()
            };
            ViewData["User_Id"] = new SelectList(_context.Users, "User_Id", "Email", model.User_Id);
            ViewData["Tags"] = new MultiSelectList(_context.Tags, "Tag_Id", "Name", model.Tags);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = await _context.Tasks
                .Include(t => t.TaskTags)
                .ThenInclude(tt => tt.Tag)
                .Include(t => t.User)
                .SingleOrDefaultAsync(t => t.Task_Id == id);

                if (task == null)
                {
                    return NotFound();
                }

                var user = await _context.Users.FindAsync(model.User_Id);
                if (user == null)
                {
                    ModelState.AddModelError("User_Id", "The selected user does not exist.");
                    ViewData["User_Id"] = new SelectList(_context.Users, "User_Id", "Email", model.User_Id);
                    ViewData["Tags"] = new MultiSelectList(_context.Tags, "Tag_Id", "Name", model.Tags);
                    return View(model);
                }


                task.Title = model.Title;
                task.Description = model.Description;
                task.DueDate = model.DueDate;
                task.Status = model.Status;
                task.User = user;
                task.User_Id = model.User_Id;

                task.TaskTags.Clear();

                if (model.Tags != null && model.Tags.Any())
                {
                    foreach (var tagId in model.Tags.Select(int.Parse))
                    {
                        task.TaskTags.Add(new TaskTag
                        {
                            Task_Id = task.Task_Id,
                            Tag_Id = tagId
                        });
                    }
                }


                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tasks.Any(t => t.Task_Id == task.Task_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "User_Id", "Email", model.User_Id);
            ViewData["Tags"] = new MultiSelectList(_context.Tags, "Tag_Id", "Name", model.Tags);
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Task_Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Task_Id == id);
        }


    }
}
