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

        public IEnumerable<IdentityExample1.Models.Task> GetAllTasksByUserID(int id)
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

        public IEnumerable<Task> GetCompletedTasksByUserID(int userID)
        {
            IEnumerable<IdentityExample1.Models.Task> CompletedTasks;
            SqlConnection conn = null;

            const string readQuery = "select * from Tasks where Complete = 'true' and UserID = @UserID";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    CompletedTasks = conn.Query<IdentityExample1.Models.Task>(readQuery, new { UserID = userID });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                CompletedTasks = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return CompletedTasks;
        }

        public IEnumerable<Task> GetUncompletedTasksByUserID(int userID)
        {
            IEnumerable<IdentityExample1.Models.Task> UncompletedTasks;
            SqlConnection conn = null;

            const string readQuery = "select * from Tasks where Complete = 'false' and UserID = @UserID";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    UncompletedTasks = conn.Query<IdentityExample1.Models.Task>(readQuery, new { UserID = userID });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                UncompletedTasks = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return UncompletedTasks;
        }

        public IEnumerable<Task> SearchTasksByUserID(int userID, string searchTerm)
        {
            IEnumerable<IdentityExample1.Models.Task> SearchResults;
            SqlConnection conn = null;

            const string searchQuery = "select * from Tasks " +
                "where UserID = @UserID and Description like @Description";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    SearchResults = conn.Query<IdentityExample1.Models.Task>(searchQuery, new { UserID = userID, Description = "%" + searchTerm + "%"});
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                SearchResults = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return SearchResults;
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
    }
}
