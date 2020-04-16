using System;
using System.Collections.Generic;
using System.Linq;
using IdentityExample1.Models;
using IdentityExample1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Identity.Dapper.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample1.Controllers
{
    public class TasksController : Controller
    {
        private ITasksDAL tasksDAL;
        private readonly UserManager<DapperIdentityUser> _userManager;
        private readonly SignInManager<DapperIdentityUser> _signInManager;
        private readonly ILogger _logger;


        public TasksController(ITasksDAL tasksDAL, 
            UserManager<DapperIdentityUser> userManager, 
            SignInManager<DapperIdentityUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            this.tasksDAL = tasksDAL;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index()
        {
            if (int.TryParse(_userManager.GetUserId(User), out int UserID))
            {
                ViewData["allTasks"] = tasksDAL.GetAllTasksByUserID(UserID);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            

            return View();
        }

        [HttpPost]
        // TODO: solve [ValidateAntiForgeryToken] bug
        public IActionResult AddTask(IdentityExample1.Models.Task task)
        {
            task.UserID = int.Parse(_userManager.GetUserId(User));

            tasksDAL.AddTask(task);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Toggle(int id)
        {
            tasksDAL.ToggleTaskbyID(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            tasksDAL.DeleteTaskbyID(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Edit(int id)
        {
            TempData["TaskToEditID"] = id;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id, IdentityExample1.Models.Task task)
        {
            task.ID = id;
            Console.WriteLine("Task Desc: " + task.Description);
            Console.WriteLine("Task DueDate: " + task.DueDate.ToString("MM/dd/yyyy"));
            tasksDAL.UpdateTask(task);
            return RedirectToAction("Index");
        }
    }
}