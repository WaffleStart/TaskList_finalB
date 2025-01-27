using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskList.Data;
using TaskList.Models;
using TaskList.ViewModels;

namespace TaskList.Controllers
{
    public class TaskTagsController : Controller
    {
        private readonly AppDbContext _context;

        public TaskTagsController(AppDbContext context)
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



    }
}
