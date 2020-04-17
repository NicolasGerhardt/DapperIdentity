﻿using System;
using System.Collections.Generic;
using System.Linq;
using IdentityExample1.Models;

namespace IdentityExample1.Services
{
    public interface ITasksDAL
    {

        public IEnumerable<Task> GetAllTasks();
        void AddTask(Task task);
        void ToggleTaskbyID(int id);
        IEnumerable<Task> GetAllTasksByUserID(int id);
        void DeleteTaskbyID(int id);
        void UpdateTask(Task task);
        object SearchTasksByUserID(int userID, string searchTerm);
    }
}
