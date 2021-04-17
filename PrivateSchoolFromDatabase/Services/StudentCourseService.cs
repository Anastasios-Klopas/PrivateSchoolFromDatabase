using PrivateSchoolFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Services
{
    class StudentCourseService : ICrable<StudentCourse>
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PrivateSchool;Integrated Security=True";
        StudentService studentService = new StudentService();
        CourseService courseService = new CourseService();
        public List<StudentCourse> GetAll()
        {
            List<StudentCourse> watches = new List<StudentCourse>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    string querySql = "Select * from Watch";
                    using (SqlCommand cmdAllWatches = new SqlCommand(querySql, connect))
                    {
                        using (SqlDataReader watchReader = cmdAllWatches.ExecuteReader())
                        {
                            while (watchReader.Read())
                            {
                                StudentCourse watch = new StudentCourse()
                                {
                                    StudentCourseId= (int)watchReader["WatchID"],
                                    StudentId = (int)watchReader["StudentID"],
                                    CourseId = (int)watchReader["CourseID"]
                                };
                                watches.Add(watch);
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL EXCEPTION", e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception", e.Message);
                }
            }
            return watches;
        }
        public void Create()
        {
            studentService.Display();
            Console.WriteLine("You can choose an ID of the student above to relate it to a course\n");
            int studentId = Convert.ToInt32(Console.ReadLine());
            courseService.Display();
            Console.WriteLine("You can choose an ID of the course above to relate, the student u picked\n");
            int courseId = Convert.ToInt32(Console.ReadLine());
            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();
                string querySql = "Insert Into Watch(StudentID,CourseID) Values (@studentID,@courseID)";
                using (SqlCommand cmdAddStudentWithCourse = new SqlCommand(querySql, connect))
                {
                    cmdAddStudentWithCourse.Parameters.Add(new SqlParameter("@studentID", studentId));
                    cmdAddStudentWithCourse.Parameters.Add(new SqlParameter("@courseID", courseId));
                    int successfulAdd = cmdAddStudentWithCourse.ExecuteNonQuery();
                    if (successfulAdd > 0)
                    {
                        Console.WriteLine($"You have succefully add {successfulAdd} relationship between student and course in the database Private School\n");
                    }
                    else
                    {
                        Console.WriteLine("You did n't add any ralationship\n");
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("SQL EXCEPTION, u did n't add any ralationship\n", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION", e.Message);
            }
        }
        public void Display()
        {
            var watchList = GetAll();
            foreach(var watch in watchList)
            {
                Console.WriteLine(watch);
            }
        }
        public void DisplayIdOnly()
        {
            var watchList = GetAll();
            foreach (var watch in watchList)
                Console.WriteLine(watch.StudentCourseId);
        }
        public StudentCourse GetById()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the watch from above");
            int watchId = Convert.ToInt32(Console.ReadLine());
            StudentCourse watch = new StudentCourse();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand($"Select * From Watch Where WatchID = @watchID", connect))
                    {
                        cmd.Parameters.AddWithValue("@watchID", watchId);
                        using (SqlDataReader watchReader = cmd.ExecuteReader())
                        {
                            while (watchReader.Read())
                            {
                                watch.StudentCourseId = (int)watchReader["WatchID"];
                                watch.CourseId = (int)watchReader["CourseID"];
                                watch.StudentId = (int)watchReader["StudentID"];
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"SQL EXCEPTION {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"EXCEPTION {e.Message}");
                }
            }
            Console.WriteLine(watch);
            return watch;
        }
        public void Update()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the watch from above that u want to UPDATE");
            int watchId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the ID of the student");
            int studentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the ID of the course");
            int courseId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "Update Watch Set StudentID=@studentID, CourseID=@cousreID Where WatchID=@watchID";

                    using (SqlCommand cmdUpdateWatch = new SqlCommand(querySql, connect))
                    {
                        cmdUpdateWatch.Parameters.Add(new SqlParameter("@studentID", studentId));
                        cmdUpdateWatch.Parameters.Add(new SqlParameter("@courseID", courseId));
                        cmdUpdateWatch.Parameters.Add(new SqlParameter("@watchID", watchId));
                        int successfulUpdate = cmdUpdateWatch.ExecuteNonQuery();
                        if (successfulUpdate > 0)
                        {
                            Console.WriteLine($"You have succefully update {successfulUpdate} relationship between student and course in the database Private School");
                        }
                        else
                        {
                            Console.WriteLine("You did n't update any assignment");
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"SQL EXCEPTION {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"EXCEPTION {e.Message}");
                }
            }
        }
        public void Delete()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the watch from above that u want to DELETE");
            int watchId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "DELETE Watch WHERE WatchID = @watchID";

                    using (SqlCommand cmdDeleteWatch = new SqlCommand(querySql, connect))
                    {
                        cmdDeleteWatch.Parameters.Add(new SqlParameter("@watchID", watchId));
                        int successfulDelete = cmdDeleteWatch.ExecuteNonQuery();
                        if (successfulDelete > 0)
                        {
                            Console.WriteLine($"You have succefully delete {successfulDelete} relationship between student and course in the database Private School\n");
                        }
                        else
                        {
                            Console.WriteLine("You did n't delete any ralationship");
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"SQL EXCEPTION u should delete the relationship 1st, ERROR: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"EXCEPTION {e.Message}");
                }
            }
        }
    }
}
