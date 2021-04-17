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
    class StudentService : ICrable<Student>
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PrivateSchool;Integrated Security=True";
        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    string querySql = "Select * from Students";
                    using (SqlCommand cmdAllStudents = new SqlCommand(querySql, connect))
                    {
                        using (SqlDataReader studentReader = cmdAllStudents.ExecuteReader())
                        {
                            while (studentReader.Read())
                            {
                                Student student = new Student()
                                {
                                    StudentId = (int)studentReader["StudentID"],
                                    FirstName = (string)studentReader["FirstName"],
                                    LastName = (string)studentReader["LastName"],
                                    DateOfBirth = (DateTime)studentReader["DateOfBirth"],
                                    TuitionFee = (double)studentReader["TuitionFee"]
                                };
                                students.Add(student);
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
            return students;
        }
        public void Create()
        {
            Console.WriteLine("You can give the firstname of the student");
            string firstName = Console.ReadLine();
            Console.WriteLine("You can give the lastname of the student");
            string lastName = Console.ReadLine();
            Console.WriteLine("You can give the date of birth of the student in format (dd/mm/yyyy)");
            DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("You can give the tuition fee of the student");
            double tuitionFee = Convert.ToDouble(Console.ReadLine());
            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();
                string querySql = "Insert Into Students(FirstName,LastName,DateOfBirth,TuitionFee) Values (@firstName,@lastName,@dateOfBirth,@tuitionFee)";
                using (SqlCommand cmdAddStudent = new SqlCommand(querySql, connect))
                {
                    cmdAddStudent.Parameters.Add(new SqlParameter("@firstName", firstName));
                    cmdAddStudent.Parameters.Add(new SqlParameter("@lastName", lastName));
                    cmdAddStudent.Parameters.Add(new SqlParameter("@dateOfBirth", dateOfBirth));
                    cmdAddStudent.Parameters.Add(new SqlParameter("@tuitionFee", tuitionFee));
                    int successfulAdd = cmdAddStudent.ExecuteNonQuery();
                    if (successfulAdd > 0)
                    {
                        Console.WriteLine($"You have succefully add {successfulAdd} student in the database Private School");
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
                Console.WriteLine("EXCEPTION", e.Message);
            }
        }
        public List<CoursesPerStudent> MultyCoursesPerSrudent()
        {
            List<Student> students = GetAll();
            List<Course> courses = new CourseService().GetAll();
            List<StudentCourse> watches = new StudentCourseService().GetAll();

            var querySql = from s in students
                           join w in watches
                           on s.StudentId equals w.StudentId
                           join w1 in watches
                           on w.StudentId equals w1.StudentId
                           join c in courses
                           on w.CourseId equals c.CourseId
                           where w.CourseId != w1.CourseId
                           select new
                           {
                               Student = s,
                               Course = c
                           };

            List<CoursesPerStudent> MultyCoursesPerSrudent = new List<CoursesPerStudent>();
            var groupedByStudents = querySql.GroupBy(s => s.Student);
            
                foreach (var group in groupedByStudents)
                {
                    CoursesPerStudent coursesPerStudent = new CoursesPerStudent();
                    coursesPerStudent.Student = group.Key;
                    foreach (var item in group.Distinct())
                    {
                        //count++;
                        coursesPerStudent.Courses.Add(item.Course);
                    }
                    
                    MultyCoursesPerSrudent.Add(coursesPerStudent);
                }
            return MultyCoursesPerSrudent;
        }
        public void Display()
        {
            var studentsList = GetAll();
            foreach (var students in studentsList)
                Console.WriteLine(students);
        }
        public void DisplayMultyCoursesPerStudent()
        {
            var coursesPerStudent = MultyCoursesPerSrudent();
            foreach (var student in coursesPerStudent)
            {
                Console.WriteLine(student.Student.FirstName + " " + student.Student.LastName);
                
                foreach (var course in student.Courses) //.Distinct()
                {
                    Console.WriteLine("\t" + course.CourseTitle + " " + " " + course.Stream + " " + course.Type);
                }
                Console.WriteLine("");
            }
        }
        public void DisplayIdOnly()
        {
            var studentsList = GetAll();
            foreach (var student in studentsList)
                Console.WriteLine(student.StudentId);
        }
        public Student GetById()
        {
            DisplayIdOnly();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the student from above");
            int studentId = Convert.ToInt32(Console.ReadLine());
            Student student = new Student();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand($"Select * From Students Where StudentID = @studentID", connect))
                    {
                        cmd.Parameters.AddWithValue("@studentID", studentId);
                        using (SqlDataReader studentReader = cmd.ExecuteReader())
                        {
                            while (studentReader.Read())
                            {
                                student.StudentId = (int)studentReader["StudentID"];
                                student.FirstName = (string)studentReader["Firstname"];
                                student.LastName = (string)studentReader["LastName"];
                                student.DateOfBirth = (DateTime)studentReader["DateOfBirth"];
                                student.TuitionFee = (double)studentReader["TuitionFee"];
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
            Console.WriteLine(student);
            return student;
        }
        public void Update()
        {
            Display();
            Console.WriteLine("");
            Console.WriteLine("You can choose an ID of the student from above that u want to UPDATE");
            int studentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You can give the firstname of the student");
            string firstName = Console.ReadLine();
            Console.WriteLine("You can give the lastname of the student");
            string lastName = Console.ReadLine();
            Console.WriteLine("You can give the date of birth of the student in format (dd/mm/yyyy)");
            DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("You can give the tuition fee of the student");
            double tuitionFee = Convert.ToDouble(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "Update Students Set FirstName=@firstName, LastName=@lastName, DateOfBirth=@dateOfBirth, TuitionFee=@tuitionFee Where StudentID=@studentID";

                    using (SqlCommand cmdUpdateStudent = new SqlCommand(querySql, connect))
                    {
                        cmdUpdateStudent.Parameters.Add(new SqlParameter("@firstName", firstName));
                        cmdUpdateStudent.Parameters.Add(new SqlParameter("@lastName", lastName));
                        cmdUpdateStudent.Parameters.Add(new SqlParameter("@dateOfBirth", dateOfBirth));
                        cmdUpdateStudent.Parameters.Add(new SqlParameter("@tuitionFee", tuitionFee));
                        cmdUpdateStudent.Parameters.Add(new SqlParameter("@studentID", studentId));
                        int successfulUpdate = cmdUpdateStudent.ExecuteNonQuery();
                        if (successfulUpdate > 0)
                        {
                            Console.WriteLine($"You have succefully update {successfulUpdate} student in the database Private School");
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
            Console.WriteLine("You can choose an ID of the student from above that u want to DELETE");
            int studentId = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                try
                {
                    string querySql = "DELETE Students WHERE StudentID = @studentID";

                    using (SqlCommand cmdDeleteStudent = new SqlCommand(querySql, connect))
                    {
                        cmdDeleteStudent.Parameters.Add(new SqlParameter("@studentID", studentId));
                        int successfulDelete = cmdDeleteStudent.ExecuteNonQuery();
                        if (successfulDelete > 0)
                        {
                            Console.WriteLine($"You have succefully delete {successfulDelete} student in the database Private School");
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
        public List<AssignmentsPerCoursePerStudent> AllAssignmentsPerAllCoursePerAllStudent()
        {
            List<Student> students = GetAll();
            List<StudentCourse> watches = new StudentCourseService().GetAll();
            List<AssignmentsPerCourse> assignmentsPerCourses = new CourseService().AllAssignmentsPerAllCourses();
            var querySql = from s in students
                           join w in watches
                           on s.StudentId equals w.StudentId
                           join aPc in assignmentsPerCourses
                           on w.CourseId equals aPc.Course.CourseId
                           select new 
                           {
                                Students = s,
                                AssignmentsPerCourse= aPc
                           };
            var groupedByStudents = querySql.GroupBy(s => s.Students);
            List < AssignmentsPerCoursePerStudent > AllassignmentsPerAllStudentsPerAllCourses = new List<AssignmentsPerCoursePerStudent>();
            foreach (var group in groupedByStudents)
            {
                AssignmentsPerCoursePerStudent assignmentsPerStudentsPerCourse = new AssignmentsPerCoursePerStudent();
                assignmentsPerStudentsPerCourse.Student = group.Key;
                //assignmentsPerStudentsPerCourse.Student = group.Key;
                foreach (var item in group)
                {
                    //assignmentsPerStudentsPerCourse.Students.Add(item.Student);
                    assignmentsPerStudentsPerCourse.AssignmentsPerCourses.Add(item.AssignmentsPerCourse);
                }
                AllassignmentsPerAllStudentsPerAllCourses.Add(assignmentsPerStudentsPerCourse);
            }
            return AllassignmentsPerAllStudentsPerAllCourses;
        }
        public void DisplayAssignmentsPerCoursePerStudent()
        {
            var assignmentPerCoursePerStudent = AllAssignmentsPerAllCoursePerAllStudent();
            foreach(var student in assignmentPerCoursePerStudent)
            {
                Console.WriteLine(student.Student.FirstName + " " + student.Student.LastName);
                foreach(var assignmentsPerCourse in student.AssignmentsPerCourses)
                {
                    Console.WriteLine("\t" + assignmentsPerCourse.Course.CourseTitle + " " + assignmentsPerCourse.Course.Stream + " " + assignmentsPerCourse.Course.Type);
                    foreach(var assignmnet in assignmentsPerCourse.Assignments)
                    {
                        Console.WriteLine("\t\t" + assignmnet.AssignmentTitle + " " + assignmnet.Description);
                    }
                }
            }
        }
    }
}
