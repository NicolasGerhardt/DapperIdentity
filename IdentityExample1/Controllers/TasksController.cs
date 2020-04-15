using System;
using System.Collections.Generic;
using System.Linq;
using IdentityExample1.Models;
using IdentityExample1.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityExample1.Controllers
{
    public class TasksController : Controller
    {
        private ITasksDAL tasksDAL;

        public TasksController(ITasksDAL tasksDAL)
        {
            this.tasksDAL = tasksDAL;
        }

        public IActionResult Index()
        {
            ViewData["allTasks"] = tasksDAL.GetAllTasks();

            return View();
        }
    }
}