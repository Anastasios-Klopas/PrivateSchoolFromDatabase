using PrivateSchoolFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Services
{
    class AssignmentCourseService : ICrable<AssignmentCourse>
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PrivateSchool;Integrated Security=True";
        AssignmentService assignmentService = new AssignmentService();
        CourseService courseService = new CourseService();
        public List<AssignmentCourse> GetAll()
        {
            List<AssignmentCourse> assigns = new List<AssignmentCourse>();
            using (SqlConnection connect= new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    string querySql = "Select * from Assign";
                    using (SqlCommand cmdAllAssign = new SqlCommand(querySql, connect))
                    {
                        using (SqlDataReader assignReader = cmdAllAssign.ExecuteReader())
                        {
                            while (assignReader.Read())
                            {
                                AssignmentCourse assign = new AssignmentCourse()
                                {
                                    AssignmentCourseId=(int)assignReader["AssignID"],
                                    AssignmentId = (int)assignReader["AssignmentID"],
                                    CourseId = (int)assignReader["CourseID"]
                                };
                                assigns.Add(assign);
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
            return assigns;
        }
        public void Create()
        {
            assignmentService.Display();
            Console.WriteLine("You can choose an ID of the assignment above to relate it to a course\n");
            int assignmentId = Convert.ToInt32(Console.ReadLine());
            courseService.Display();
            Console.WriteLine("You can choose an ID of the course above to relate, the assignment u picked\n");
            int courseId = Convert.ToInt32(Console.ReadLine());
            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();
                string querySql = "Insert Into Assign(AssignmentID,CourseID) Values (@assignmentID,@courseID)";
                using (SqlCommand cmdAddAssignWithCourse = new SqlCommand(querySql, connect))
                {
                    cmdAddAssignWithCourse.Parameters.Add(new SqlParameter("@assignID", assignmentId));
                    cmdAddAssignWithCourse.Parameters.Add(new SqlParameter("@courseID", courseId));
                    int successfulAdd = cmdAddAssignWithCourse.ExecuteNonQuery();
                    if (successfulAdd > 0)
                    {
                        Console.WriteLine($"You have succefully add {successfulAdd} relationship between assignment and course in the database Private School\n");
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
            var assignList = GetAll();
            foreach (var a in assignList)
            {
                Console.WriteLine(a);
            }
        }
        public void DisplayIdOnly()
        {
            var assignList = GetAll();
            foreach (var assign in assignList)
                Console.WriteLine(assign.AssignmentCourseId);
        }
        public AssignmentCourse GetById()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the assign froma above");
            int assignId = Convert.ToInt32(Console.ReadLine());
            AssignmentCourse assign = new AssignmentCourse();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand cmdGetById = new SqlCommand($"Select * From Assign Where AssignID = @assignID", connect))
                    {
                        cmdGetById.Parameters.AddWithValue("@assignID", assignId);
                        using (SqlDataReader assignReader = cmdGetById.ExecuteReader())
                        {
                            while (assignReader.Read())
                            {
                                assign.AssignmentCourseId = (int)assignReader["AssignID"];
                                assign.CourseId = (int)assignReader["CourseID"];
                                assign.AssignmentId = (int)assignReader["AssignmentID"];
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
            Console.WriteLine(assign);
            return assign;
        }
        public void Update()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the assign from above that u want to UPDATE");
            int assignId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the ID of the assignment");
            int assignmentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the ID of the course");
            int courseId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "Update Assign Set AssignmentID=@assignmentID, CourseID=@cousreID Where AssignID=@assignID";

                    using (SqlCommand cmdUpdateAssign = new SqlCommand(querySql, connect))
                    {
                        cmdUpdateAssign.Parameters.Add(new SqlParameter("@assignmentID", assignmentId));
                        cmdUpdateAssign.Parameters.Add(new SqlParameter("@courseID", courseId));
                        cmdUpdateAssign.Parameters.Add(new SqlParameter("@assignID", assignId));
                        int successfulUpdate = cmdUpdateAssign.ExecuteNonQuery();
                        if (successfulUpdate > 0)
                        {
                            Console.WriteLine($"You have succefully update {successfulUpdate} relationship between assignment and course in the database Private School");
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
            Console.WriteLine("You can choose an ID of the assign from above that u want to DELETE");
            int assignId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "DELETE Assign WHERE AssignID = @assignID";

                    using (SqlCommand cmdDeleteAssign = new SqlCommand(querySql, connect))
                    {
                        cmdDeleteAssign.Parameters.Add(new SqlParameter("@assignID", assignId));
                        int successfulDelete = cmdDeleteAssign.ExecuteNonQuery();
                        if (successfulDelete > 0)
                        {
                            Console.WriteLine($"You have succefully delete {successfulDelete} relationship between assignment and course in the database Private School\n");
                        }
                        else
                        {
                            Console.WriteLine("You did n't delete any ralationship");
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
    }
}
