using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDB.Models;

namespace MvcDB.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AddOrEdit(int studentId = 0)
        {
            if (studentId == 0) return View(new Student());

            var connectionString = ConfigurationManager.ConnectionStrings["SampleDatabase"].ConnectionString;
            var data = new Student() { };
            using (var connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string query = "Select * from Employee where Id = @Id";
                SqlCommand myCommand = new SqlCommand(query, connection);
                myCommand.Parameters.AddWithValue("@Id", studentId);

                var reader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    data.City = (string)reader["City"];
                    data.Dept = (string)reader["Dept"];
                    data.Name = (string)reader["Name"];
                    data.Id = (int)reader["Id"];
                }

            }
            return View(data);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Student student)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SampleDatabase"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (student.Id == 0)
                {
                    string query = "INSERT INTO Employee ([id],[Name],[City],[Dept])";
                    query += " VALUES (@id, @Name, @City, @Dept)";

                    SqlCommand myCommand = new SqlCommand(query, connection);
                    myCommand.Parameters.AddWithValue("@Name", student.Name);
                    myCommand.Parameters.AddWithValue("@City", student.City);
                    myCommand.Parameters.AddWithValue("@Dept", student.Dept);
                    // ... other parameters
                    var id = myCommand.ExecuteNonQuery();
                    Console.WriteLine("Inserting Sucessfully");
                }
                else
                {
                    string query = "UPDATE Employee SET Name= @Name,City= @City, Dept =@Dept WHERE ID=@Id";

                    SqlCommand myCommand = new SqlCommand(query, connection);
                    myCommand.Parameters.AddWithValue("@Name", student.Name);
                    myCommand.Parameters.AddWithValue("@City", student.City);
                    myCommand.Parameters.AddWithValue("@Dept", student.Dept);
                    myCommand.Parameters.AddWithValue("@Id", student.Id);

                    var id = myCommand.ExecuteNonQuery();
                }
                connection.Close();

                return View(student);
            }
        }






    }
}