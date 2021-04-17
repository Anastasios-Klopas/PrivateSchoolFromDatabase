using PrivateSchoolFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Services
{
    class AssignmentService : ICrable<Assignment>
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PrivateSchool;Integrated Security=True";
        public List<Assignment> GetAll()
        {
            List<Assignment> assignments = new List<Assignment>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try 
                {
                    connect.Open();
                    string querySql = "Select * From Assignments";
                    using (SqlCommand cmdAllAssignments = new SqlCommand(querySql,connect))
                    {
                        using (SqlDataReader assignmentReader = cmdAllAssignments.ExecuteReader())
                        {
                            while (assignmentReader.Read())
                            {
                                Assignment assignment = new Assignment()
                                {
                                    AssignmentId = (int)assignmentReader["AssignmentID"],
                                    AssignmentTitle=(string)assignmentReader["AssignmentTitle"],
                                    Description=(string)assignmentReader["Description"],
                                    SubDateTime=(DateTime)assignmentReader["SubDateTime"],
                                    OralMark=(double)assignmentReader["OralMark"],
                                    TotalMark= (double)assignmentReader["TotalMark"]
                                };
                                assignments.Add(assignment);
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL EXCEPTION {0}", e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception {0}", e.Message);
                }
            }
            return assignments;
        }
        public void Create()
        {
            Console.WriteLine("You can give the title of the assignment");
            string assignmentTitle = Console.ReadLine();
            Console.WriteLine("You can give the description of the assignment");
            string description = Console.ReadLine();
            Console.WriteLine("You can give the submission date of the assignment in format (dd/mm/yyyy)");
            DateTime subDateTime = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("You can give the oral mark of the assignment");
            double oralMark = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("You can give the total mark of the assignment");
            double totalMark = Convert.ToDouble(Console.ReadLine());
            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();
                string querySql = "Insert into Assignments(AssignmentTitle,Description,SubDateTime,OralMark,TotalMark) Values(@assignmentTitle,@description,@subDateTime,@oralMark,@totalMark)";
                using (SqlCommand cmdAddAssignment = new SqlCommand(querySql, connect))
                {
                    cmdAddAssignment.Parameters.Add(new SqlParameter("@assignmentTitle", assignmentTitle));
                    cmdAddAssignment.Parameters.Add(new SqlParameter("@description", description));
                    cmdAddAssignment.Parameters.Add(new SqlParameter("@subDateTime", subDateTime));
                    cmdAddAssignment.Parameters.Add(new SqlParameter("@oralMark", oralMark));
                    cmdAddAssignment.Parameters.Add(new SqlParameter("@totalMark", totalMark));
                    int successfulAdd = cmdAddAssignment.ExecuteNonQuery();
                    if (successfulAdd > 0)
                    {
                        Console.WriteLine($"You have succefully add {successfulAdd} assignment in the database Private School");
                    }
                    else
                    {
                        Console.WriteLine("You did n't add any student");
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
        public void Display()
        {
            var assignmentsList = GetAll();
            foreach (var assignment in assignmentsList)
                Console.WriteLine(assignment);
        }
        public void DisplayIdOnly()
        {
            var assignmentsList = GetAll();
            foreach (var assignment in assignmentsList)
                Console.WriteLine(assignment.AssignmentId);
        }
        public Assignment GetById()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the assignment from above");
            int assignmentId = Convert.ToInt32(Console.ReadLine());
            Assignment assignment = new Assignment();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand cmdGetById = new SqlCommand($"Select * From Assignments Where AssignmentID = @assignmentID", connect))
                    {
                        cmdGetById.Parameters.AddWithValue("@assignmentID", assignmentId);
                        using (SqlDataReader assignmentReader = cmdGetById.ExecuteReader())
                        {
                            while (assignmentReader.Read())
                            {
                                assignment.AssignmentId = (int)assignmentReader["AssignmentID"];
                                assignment.AssignmentTitle = (string)assignmentReader["AssignmentTitle"];
                                assignment.Description= (string)assignmentReader["Description"];
                                assignment.SubDateTime = (DateTime)assignmentReader["SubDateTime"];
                                assignment.OralMark = (double)assignmentReader["OralMark"];
                                assignment.TotalMark = (double)assignmentReader["TotalMark"];
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
            Console.WriteLine(assignment);
            return assignment;
        }
        public void Update()
        {
            Display();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the assignment from above that u want to UPDATE");
            int assignmentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the title of the assignment");
            string assignmentTitle = Console.ReadLine();
            Console.WriteLine("You can give the description of the assignment");
            string description = Console.ReadLine();
            Console.WriteLine("You can give the submission date of the assignment in format (dd/mm/yyyy)");
            DateTime subDateTime = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("You can give the oral mark of the assignment");
            double oralMark = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("You can give the total mark of the assignment");
            double totalMark = Convert.ToDouble(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "Update Assignments Set AssignmentTitle=@assignmentTitle, Decsription=@description, SubDateTime=@subDateTime, OralMark=@oralMark, TotalMark=@totalMark Where AssignmentID=@assignmentID";

                    using (SqlCommand cmdUpdateAssignment = new SqlCommand(querySql, connect))
                    {
                        cmdUpdateAssignment.Parameters.Add(new SqlParameter("@assignmentTitle", assignmentTitle));
                        cmdUpdateAssignment.Parameters.Add(new SqlParameter("@description", description));
                        cmdUpdateAssignment.Parameters.Add(new SqlParameter("@subDateTime", subDateTime));
                        cmdUpdateAssignment.Parameters.Add(new SqlParameter("@oralMark", oralMark));
                        cmdUpdateAssignment.Parameters.Add(new SqlParameter("@totalMark", totalMark));
                        cmdUpdateAssignment.Parameters.Add(new SqlParameter("@assignmentID", assignmentId));
                        int successfulUpdate = cmdUpdateAssignment.ExecuteNonQuery();
                        if (successfulUpdate > 0)
                        {
                            Console.WriteLine($"You have succefully update {successfulUpdate} assignment in the database Private School");
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
            Display();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the assignment from above that u want to DELETE");
            int assignmentId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "DELETE Assignments WHERE AssignmentID = @assignmentID";

                    using (SqlCommand cmdDeleteAssignment = new SqlCommand(querySql, connect))
                    {
                        cmdDeleteAssignment.Parameters.Add(new SqlParameter("@assignmentID", assignmentId));
                        int successfulDelete = cmdDeleteAssignment.ExecuteNonQuery();
                        if (successfulDelete > 0)
                        {
                            Console.WriteLine($"You have succefully delete {successfulDelete} assignment in the database Private School");
                        }
                        else
                        {
                            Console.WriteLine("You did n't delete any assignment");
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"SQL EXCEPTION, u should delete the relationship 1st, ERROR: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"EXCEPTION {e.Message}");
                }
            }
        }
    }
}
