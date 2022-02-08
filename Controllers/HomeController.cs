using Microsoft.AspNetCore.Mvc;
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


        public IActionResult TaskQuadrant()
        {

            return View();
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
