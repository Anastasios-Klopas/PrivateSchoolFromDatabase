using PrivateSchoolFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Services
{
    class TrainerService : ICrable<Trainer>
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PrivateSchool;Integrated Security=True";
        public List<Trainer> GetAll()
        {
            List<Trainer> trainers = new List<Trainer>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    string querySql = "Select * from Trainers";
                    using (SqlCommand cmdAllTrainers = new SqlCommand(querySql, connect))
                    {
                        using (SqlDataReader trainerReader = cmdAllTrainers.ExecuteReader())
                        {
                            while (trainerReader.Read())
                            {
                                Trainer trainer =new Trainer()
                                {
                                    TrainerId = (int)trainerReader["TrainerID"],
                                    FirstName = (string)trainerReader["FirstName"],
                                    LastName = (string)trainerReader["LastName"],
                                    Subject = (string)trainerReader["Subject"]
                                };
                                trainers.Add(trainer);
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
            return trainers;
        }
        public void Create()
        {
            Console.WriteLine("You can give the firstname of the trainer");
            string firstName = Console.ReadLine();
            Console.WriteLine("You can give the lastname of the trainer");
            string lastName = Console.ReadLine();
            Console.WriteLine("You can give the subject that trainer teach");
            string subject = Console.ReadLine();
            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();
                string querySql = "Insert Into Trainers(FirstName,LastName,Subject) Values (@firstName,@lastName,@subject)";
                using (SqlCommand cmdAddTrainer = new SqlCommand(querySql, connect))
                {
                    cmdAddTrainer.Parameters.Add(new SqlParameter("@firstName", firstName));
                    cmdAddTrainer.Parameters.Add(new SqlParameter("@lastName", lastName));
                    cmdAddTrainer.Parameters.Add(new SqlParameter("@subject", subject));
                    int successfulAdd = cmdAddTrainer.ExecuteNonQuery();
                    if (successfulAdd > 0)
                    {
                        Console.WriteLine($"You have succefully add {successfulAdd} trainer in the database Private School");
                    }
                    else
                    {
                        Console.WriteLine("You did n't add any trainer");
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("SQL EXCEPTION", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION", e.Message);
            }
        }
        public void Display()
        {
            var trainersList = GetAll();
            foreach (var trainer in trainersList)
                Console.WriteLine(trainer);
        }
        public void DisplayIdOnly()
        {
            var trainersList = GetAll();
            foreach (var trainer in trainersList)
                Console.WriteLine(trainer.TrainerId);
        }
        public Trainer GetById()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the trainer from above");
            int trainerId = Convert.ToInt32(Console.ReadLine());
            Trainer trainer = new Trainer();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand($"Select * From Trainers Where TrainerID = @trainerID", connect))
                    {
                        cmd.Parameters.AddWithValue("@trainerID", trainerId);
                        using (SqlDataReader trainerReader = cmd.ExecuteReader())
                        {
                            while (trainerReader.Read())
                            {
                                trainer.TrainerId = (int)trainerReader["TrainerID"];
                                trainer.FirstName = (string)trainerReader["FirstName"];
                                trainer.LastName = (string)trainerReader["LastName"];
                                trainer.Subject = (string)trainerReader["Subject"];
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
            Console.WriteLine(trainer);
            return trainer;
        }
        public void Update()
        {
            Display();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the trainer from above that u want to UPDATE");
            int trainerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the firstname of the trainer");
            string firstName = Console.ReadLine();
            Console.WriteLine("You can give the lastname of the trainer");
            string lastName = Console.ReadLine();
            Console.WriteLine("You can give the subject that trainer teach");
            string subject = Console.ReadLine();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "Update Trainers Set FirstName=@firstName, LastName=@lastName, Subject=@subject Where TrainerID=@trainerID";

                    using (SqlCommand cmdUpdateTrainer = new SqlCommand(querySql, connect))
                    {
                        cmdUpdateTrainer.Parameters.Add(new SqlParameter("@firstName", firstName));
                        cmdUpdateTrainer.Parameters.Add(new SqlParameter("@lastName", lastName));
                        cmdUpdateTrainer.Parameters.Add(new SqlParameter("@subject", subject));
                        cmdUpdateTrainer.Parameters.Add(new SqlParameter("@trainerID", trainerId));
                        int successfulUpdate = cmdUpdateTrainer.ExecuteNonQuery();
                        if (successfulUpdate > 0)
                        {
                            Console.WriteLine($"You have succefully update {successfulUpdate} trainer in the database Private School");
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
            Console.WriteLine("You can choose an ID of the trainer from above that u want to DELETE");
            int trainerId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "DELETE Trainers WHERE TrainerID = @trainerID";

                    using (SqlCommand cmdDeleteTrainer = new SqlCommand(querySql, connect))
                    {
                        cmdDeleteTrainer.Parameters.Add(new SqlParameter("@trainerID", trainerId));
                        int successfulDelete = cmdDeleteTrainer.ExecuteNonQuery();
                        if (successfulDelete > 0)
                        {
                            Console.WriteLine($"You have succefully delete {successfulDelete} trainer in the database Private School");
                        }
                        else
                        {
                            Console.WriteLine("You did n't delete any student");
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
