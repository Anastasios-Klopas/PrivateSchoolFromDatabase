using PrivateSchoolFromDatabase.Models;
using PrivateSchoolFromDatabase.ViewData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Services
{
    class CourseService : ICrable<Course>
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PrivateSchool;Integrated Security=True";
        public List<Course> GetAll()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    string querySql = "Select * from Courses";
                    using (SqlCommand cmdAllCourse = new SqlCommand(querySql, connect))
                    {
                        using (SqlDataReader courseReader = cmdAllCourse.ExecuteReader())
                        {
                            while (courseReader.Read())
                            {
                                Course course = new Course()
                                {
                                    CourseId = (int)courseReader["CourseID"],
                                    CourseTitle = (string)courseReader["CourseTitle"],
                                    Stream = (string)courseReader["Stream"],
                                    Type = (string)courseReader["Type"],
                                    StartDate = (DateTime)courseReader["StartDate"],
                                    EndDate = (DateTime)courseReader["EndDate"]
                                };
                                courses.Add(course);
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
            return courses;
        }
        public void Create()
        {
            Console.WriteLine("You can give the title of the course");
            string courseTitle = Console.ReadLine();
            Console.WriteLine("You can give the stream of the course");
            string stream= Console.ReadLine();
            Console.WriteLine("You can give the type of the course");
            string type = Console.ReadLine();
            Console.WriteLine("You can give the start date of the course in format (dd/mm/yyyy)");
            DateTime startDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("You can give the end date of the course in format (dd/mm/yyyy)");
            DateTime endDate = Convert.ToDateTime(Console.ReadLine());
            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();
                string querySql = "Insert Into Courses(CourseTitle,Stream,Type,StartDate,EndDate) Values (@courseTitle,@stream,@type,@startDate,@endDate)";
                using (SqlCommand cmdAddCourse = new SqlCommand(querySql, connect))
                {
                    cmdAddCourse.Parameters.Add(new SqlParameter("@courseTitle", courseTitle));
                    cmdAddCourse.Parameters.Add(new SqlParameter("@stream", stream));
                    cmdAddCourse.Parameters.Add(new SqlParameter("@type", type));
                    cmdAddCourse.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmdAddCourse.Parameters.Add(new SqlParameter("@endDate", endDate));
                    int successfulAdd = cmdAddCourse.ExecuteNonQuery();
                    if (successfulAdd > 0)
                    {
                        Console.WriteLine($"You have succefully add {successfulAdd} course in the database Private School");
                    }
                    else
                    {
                        Console.WriteLine("You did n't add any course");
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
        public List<StudentsPerCourse> AllStudentsPerAllCourses()
        {
            List<Course> courses = GetAll();
            List<Student> students = new StudentService().GetAll();
            List<StudentCourse> watches = new StudentCourseService().GetAll();

            var querySql = from s in students
                           join w in watches
                           on s.StudentId equals w.StudentId
                           join c in courses
                           on w.CourseId equals c.CourseId
                           select new 
                           { 
                               Student = s, 
                               Course = c 
                           };
            List<StudentsPerCourse> allStudentsPerAllCourses = new List<StudentsPerCourse>();
            var groupedByCourses = querySql.GroupBy(g => g.Course);
            foreach (var group in groupedByCourses)
            {
                StudentsPerCourse studentsPerCourse = new StudentsPerCourse();
                studentsPerCourse.Course = group.Key;
                foreach (var item in group)
                {
                    studentsPerCourse.Students.Add(item.Student);
                }
                allStudentsPerAllCourses.Add(studentsPerCourse);
            }
            return allStudentsPerAllCourses;
        }
        public List<TrainersPerCourse> AllTrainersPerAllCourses()
        {
            List<Course> courses = GetAll();
            List<Trainer> trainers = new TrainerService().GetAll();
            List<TrainerCourse> teaches = new TrainerCourseService().GetAll();
            var querySql = from tr in trainers
                           join te in teaches
                           on tr.TrainerId equals te.TrainerId
                           join c in courses
                           on te.CourseId equals c.CourseId
                           select new 
                           { 
                               Trainer = tr, 
                               Course = c 
                           };
            List<TrainersPerCourse> allTrainersPerAllCourses = new List<TrainersPerCourse>();
            var groupedByCourses = querySql.GroupBy(g => g.Course);
            foreach (var group in groupedByCourses)
            {
                TrainersPerCourse trainersPerCourse = new TrainersPerCourse();
                trainersPerCourse.Course = group.Key;
                foreach(var item in group)
                {
                    trainersPerCourse.Trainers.Add(item.Trainer);
                }
                allTrainersPerAllCourses.Add(trainersPerCourse);
            }
            return allTrainersPerAllCourses;
        }
        public List<AssignmentsPerCourse> AllAssignmentsPerAllCourses()
        {
            List<Course> courses = GetAll();
            List<Assignment> assignments = new AssignmentService().GetAll();
            List<AssignmentCourse> assigns = new AssignmentCourseService().GetAll();
            var querySql = from a in assignments
                           join ass in assigns
                           on a.AssignmentId equals ass.AssignmentId
                           join c in courses
                           on ass.CourseId equals c.CourseId
                           select new 
                           { 
                               Assignment = a, 
                               Course = c 
                           };
            List<AssignmentsPerCourse> allAssignmentsPerAllCourses = new List<AssignmentsPerCourse>();
            var groupedByCourses = querySql.GroupBy(g => g.Course);
            foreach (var group in groupedByCourses)
            {
                AssignmentsPerCourse assignmentsPerCourse = new AssignmentsPerCourse();
                assignmentsPerCourse.Course = group.Key;
                foreach (var item in group)
                {
                    assignmentsPerCourse.Assignments.Add(item.Assignment);
                }
                allAssignmentsPerAllCourses.Add(assignmentsPerCourse);
            }
            return allAssignmentsPerAllCourses;
        }
        
        public void Display()
        {
            var coursesList = GetAll();
            foreach(var course in coursesList)
                Console.WriteLine(course);
        }
        public void DisplayStudentsPerCourse()
        {
            var studentsPerCourse = AllStudentsPerAllCourses();
            foreach(var course in studentsPerCourse)
            {
                Console.WriteLine(course.Course.CourseTitle + " " + " " + course.Course.Stream + " " + course.Course.Type);
                foreach(var student in course.Students)
                {
                    Console.WriteLine("\t" + student.FirstName + " " + student.LastName);
                }
                Console.WriteLine("");
            }
        }
        public void DisplayTrainersPerCourse()
        {
            var trainersPerCourse = AllTrainersPerAllCourses();
            foreach (var course in trainersPerCourse)
            {
                Console.WriteLine(course.Course.CourseTitle + " " + " " + course.Course.Stream + " " + course.Course.Type);
                foreach (var trainer in course.Trainers)
                {
                    Console.WriteLine("\t" + trainer.FirstName + " " + trainer.LastName);
                }
                Console.WriteLine("");
            }
        }
        public void DisplayAssignmentsPerCourse()
        {
            var assignmentsPerCourse = AllAssignmentsPerAllCourses();
            foreach (var course in assignmentsPerCourse)
            {
                Console.WriteLine(course.Course.CourseTitle + " " + " " + course.Course.Stream + " " + course.Course.Type);
                foreach (var assignment in course.Assignments)
                {
                    Console.WriteLine("\t" + assignment.AssignmentTitle + " " + assignment.Description);
                }
                Console.WriteLine("");
            }
        }
        public void DisplayIdOnly()
        {
            var coursesList = GetAll();
            foreach (var course in coursesList)
                Console.WriteLine(course.CourseId);
        }
        public Course GetById()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the course from above");
            int courseId = Convert.ToInt32(Console.ReadLine());
            Course course = new Course();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand($"Select * From Courses Where CourseID = @courseID", connect))
                    {
                        cmd.Parameters.AddWithValue("@courseID", courseId);
                        using (SqlDataReader courseReader = cmd.ExecuteReader())
                        {
                            while (courseReader.Read())
                            {
                                course.CourseId = (int)courseReader["CourseID"];
                                course.CourseTitle = (string)courseReader["CourseTitle"];
                                course.Stream = (string)courseReader["Stream"];
                                course.Type = (string)courseReader["Type"];
                                course.StartDate = (DateTime)courseReader["StartDate"];
                                course.EndDate = (DateTime)courseReader["EndDate"];
                                //Console.WriteLine(course);
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
            Console.WriteLine(course);
            return course;
        }
        public void Update()
        {
            Display();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the course from above that u want to UPDATE");
            int courseId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the title of the course");
            string courseTitle = Console.ReadLine();
            Console.WriteLine("You can give the stream of the course");
            string stream = Console.ReadLine();
            Console.WriteLine("You can give the type of the course");
            string type = Console.ReadLine();
            Console.WriteLine("You can give the start date of the course in format (dd/mm/yyyy)");
            DateTime startDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("You can give the end date of the course in format (dd/mm/yyyy)");
            DateTime endDate = Convert.ToDateTime(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "Update Courses Set CourseTitle=@courseTitle, Stream=@stream, Type=@type, StartDate=@startDate, EndDate=@endDate Where CourseID=@courseID";

                    using (SqlCommand cmdUpdateCourse = new SqlCommand(querySql, connect))
                    {
                        cmdUpdateCourse.Parameters.Add(new SqlParameter("@courseTitle", courseTitle));
                        cmdUpdateCourse.Parameters.Add(new SqlParameter("@stream", stream));
                        cmdUpdateCourse.Parameters.Add(new SqlParameter("@type", type));
                        cmdUpdateCourse.Parameters.Add(new SqlParameter("@startDate", startDate));
                        cmdUpdateCourse.Parameters.Add(new SqlParameter("@endDate", endDate));
                        cmdUpdateCourse.Parameters.Add(new SqlParameter("@courseID", courseId));
                        int successfulUpdate = cmdUpdateCourse.ExecuteNonQuery();
                        if (successfulUpdate > 0)
                        {
                            Console.WriteLine($"You have succefully update {successfulUpdate} course in the database Private School");
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
            Console.WriteLine("You can choose an ID of the course from above that u want to DELETE");
            int courseId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "DELETE Courses WHERE CourseID = @courseID";

                    using (SqlCommand cmdDeleteCourse = new SqlCommand(querySql, connect))
                    {
                        cmdDeleteCourse.Parameters.Add(new SqlParameter("@courseID", courseId));
                        int successfulDelete = cmdDeleteCourse.ExecuteNonQuery();
                        if (successfulDelete > 0)
                        {
                            Console.WriteLine($"You have succefully delete {successfulDelete} course in the database Private School");
                        }
                        else
                        {
                            Console.WriteLine("You did n't delete any course");
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
