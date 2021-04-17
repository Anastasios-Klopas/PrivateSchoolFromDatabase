using PrivateSchoolFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Services
{
    class TrainerCourseService:ICrable<TrainerCourse>
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PrivateSchool;Integrated Security=True";
        CourseService courseService = new CourseService();
        TrainerService trainerService = new TrainerService();
        public List<TrainerCourse> GetAll()
        {
            List<TrainerCourse> teaches = new List<TrainerCourse>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    string querySql = "Select * from Teach";
                    using (SqlCommand cmdAllTeach = new SqlCommand(querySql, connect))
                    {
                        using (SqlDataReader teachReader = cmdAllTeach.ExecuteReader())
                        {
                            while (teachReader.Read())
                            {
                                TrainerCourse teach =new TrainerCourse()
                                {
                                    TrainerCourseId = (int)teachReader["TeachID"],
                                    TrainerId = (int)teachReader["TrainerID"],
                                    CourseId = (int)teachReader["CourseID"]
                                };
                                teaches.Add(teach);
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
            return teaches;
        }
        public void Create()
        {
            trainerService.Display();
            Console.WriteLine("You can choose an ID of the trainer from above to relate it to a course\n");
            int trainerId = Convert.ToInt32(Console.ReadLine());
            courseService.Display();
            Console.WriteLine("You can choose an ID of the course from above to relate, the trainer u picked\n");
            int courseId = Convert.ToInt32(Console.ReadLine());
            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();
                string querySql = "Insert Into Teach(TrainerID,CourseID) Values (@trainerID,@courseID)";
                using (SqlCommand cmdAddTeach = new SqlCommand(querySql, connect))
                {
                    cmdAddTeach.Parameters.Add(new SqlParameter("@trainerID", trainerId));
                    cmdAddTeach.Parameters.Add(new SqlParameter("@courseID", courseId));
                    int successfulAdd = cmdAddTeach.ExecuteNonQuery();
                    if (successfulAdd > 0)
                    {
                        Console.WriteLine($"You have succefully add {successfulAdd} relationship between trainer and course in the database Private School\n");
                    }
                    else
                    {
                        Console.WriteLine("You did n't add any course\n");
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
            var teachList = GetAll();
            foreach(var teach in teachList)
            {
                Console.WriteLine(teach);
            }
        }
        public void DisplayIdOnly()
        {
            var teachList = GetAll();
            foreach (var teach in teachList)
                Console.WriteLine(teach.TrainerCourseId);
        }
        public TrainerCourse GetById()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the teach from above");
            int teachId = Convert.ToInt32(Console.ReadLine());
            TrainerCourse teach = new TrainerCourse();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand($"Select * From Teach Where TeachID = @teachID", connect))
                    {
                        cmd.Parameters.AddWithValue("@teachID", teachId);
                        using (SqlDataReader teachReader = cmd.ExecuteReader())
                        {
                            while (teachReader.Read())
                            {
                                teach.TrainerCourseId = (int)teachReader["TeachID"];
                                teach.CourseId = (int)teachReader["CourseID"];
                                teach.TrainerId = (int)teachReader["TrainerID"];
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
            Console.WriteLine(teach);
            return teach;
        }
        public void Update()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the teach from above that u want to UPDATE");
            int teachId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the ID of the trainer");
            int trainerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the ID of the course");
            int courseId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "Update Watch Set TrainerID=@trainerID, CourseID=@cousreID Where TeachID=@teachID";

                    using (SqlCommand cmdUpdateTeach = new SqlCommand(querySql, connect))
                    {
                        cmdUpdateTeach.Parameters.Add(new SqlParameter("@trainerID", trainerId));
                        cmdUpdateTeach.Parameters.Add(new SqlParameter("@courseID", courseId));
                        cmdUpdateTeach.Parameters.Add(new SqlParameter("@teachID", teachId));
                        int successfulUpdate = cmdUpdateTeach.ExecuteNonQuery();
                        if (successfulUpdate > 0)
                        {
                            Console.WriteLine($"You have succefully update {successfulUpdate} relationship between trainer and course in the database Private School");
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
            Console.WriteLine("You can choose an ID of the teach from above that u want to DELETE");
            int teachId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "DELETE Teach WHERE TeachID = @TeachID";

                    using (SqlCommand cmdDeleteTeach = new SqlCommand(querySql, connect))
                    {
                        cmdDeleteTeach.Parameters.Add(new SqlParameter("@teachID", teachId));
                        int successfulDelete = cmdDeleteTeach.ExecuteNonQuery();
                        if (successfulDelete > 0)
                        {
                            Console.WriteLine($"You have succefully delete {successfulDelete} relationship between trainer and course in the database Private School\n");
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
