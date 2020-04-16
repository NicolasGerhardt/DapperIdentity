using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using IdentityExample1.Models;

namespace IdentityExample1.Services
{
    public class TasksSQLDB : ITasksDAL
    {

        private string connectionString;

        public TasksSQLDB(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public void AddTask(Task task)
        {
            SqlConnection conn = null;

            const string addQuery = "insert into Tasks (UserID, Description, DueDate, Complete) " +
                "values " +
                "(@UserID, @Description, @DueDate, 0)";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    conn.Execute(addQuery, task);
                    Console.WriteLine("Task added");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void DeleteTaskbyID(int id)
        {
            SqlConnection conn = null;

            const string deleteQuery = "Delete from Tasks " +
                "where Id = @TaskID";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    conn.Execute(deleteQuery, new { TaskID = id });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<IdentityExample1.Models.Task> GetAllTasks()
        {
            IEnumerable<IdentityExample1.Models.Task> AllTasks;
            SqlConnection conn = null;

            const string readQuery = "select * from Tasks";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    AllTasks = conn.Query<IdentityExample1.Models.Task>(readQuery);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                AllTasks = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return AllTasks;
        }

        public object GetAllTasksByUserID(int id)
        {
            IEnumerable<IdentityExample1.Models.Task> AllTasks;
            SqlConnection conn = null;

            const string readQuery = "select * from Tasks " +
                "where UserID = @UserID";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    AllTasks = conn.Query<IdentityExample1.Models.Task>(readQuery, new { UserID = id });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                AllTasks = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return AllTasks;
        }

        public void ToggleTaskbyID(int id)
        {
            SqlConnection conn = null;

            const string toggleCompleteQuery = "update Tasks " +
                "set Complete = Complete^1 " +
                "where Id = @ID";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    conn.Execute(toggleCompleteQuery, new { ID = id });
                    Console.WriteLine("Task complete toggled");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void UpdateTask(Task task)
        {
            Console.WriteLine("#####################################################");
            SqlConnection conn = null;

            const string updateTaskQuery = "update Tasks " +
                "set  Description = @Description, " +
                "DueDate = @DueDate " +
                "where Id = @ID";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    conn.Execute(updateTaskQuery, task);
                    Console.WriteLine("Task complete toggled");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            Console.WriteLine("#####################################################");
        }
    }
}
