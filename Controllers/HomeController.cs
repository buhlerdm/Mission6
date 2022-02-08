using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission6.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private TasksContext InfoContext { get; set; }

        public HomeController(ILogger<HomeController> logger, TasksContext someName)
        {
            _logger = logger;
            InfoContext = someName;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TaskForm()
        {
            ViewBag.Categories = InfoContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult TaskForm(TaskResponse ar)
        {
            if (ModelState.IsValid)
            {
                InfoContext.Add(ar);
                InfoContext.SaveChanges();
                return View("Confirmation", ar);
            }
            else
            {
                ViewBag.Categories = InfoContext.Categories.ToList();
                return View(ar);
            }
        }

        [HttpGet]
        public IActionResult TaskQuadrant()
        {
            var Tasks = InfoContext.responses
                .Include(x => x.Category)
                .Where(x => x.TaskName != null)
                .ToList();

            return View(Tasks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Edit entries for tasks
        [HttpGet]
        public IActionResult Edit(int taskid)
        {
            ViewBag.Categories = InfoContext.Categories.ToList();

            var submission = InfoContext.responses.Single(x => x.TaskId == taskid);

            return View("TaskForm", submission);
        }

        [HttpPost]
        public IActionResult Edit(TaskResponse editedTask)
        {
            if (ModelState.IsValid) //If valid
            {
                InfoContext.Update(editedTask);
                InfoContext.SaveChanges();

                return RedirectToAction("TaskQuadrant");
            }

            else //If Invalid
            {
                ViewBag.Categories = InfoContext.Categories.ToList();

                return View("TaskForm", editedTask);
            }
        }

        //Delete entries for a task
        [HttpGet]
        public IActionResult Delete(int taskid)
        {
            var submission = InfoContext.responses.Single(x => x.TaskId == taskid);

            return View(submission);
        }
        [HttpPost]
        public IActionResult Delete(TaskResponse deletedTask)
        {
            InfoContext.responses.Remove(deletedTask);
            InfoContext.SaveChanges();

            return RedirectToAction("TaskQuadrant");
        }
    }
}
