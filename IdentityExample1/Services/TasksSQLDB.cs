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
                Console.WriteLine(e);
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
    }
}
