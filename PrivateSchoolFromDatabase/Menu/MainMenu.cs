using PrivateSchoolFromDatabase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Menu
{
    class MainMenu
    {
        AssignmentService assignmentService = new AssignmentService();
        StudentService studentService = new StudentService();
        CourseService courseService = new CourseService();
        TrainerService trainerService = new TrainerService();
        StudentCourseService watchService = new StudentCourseService();
        TrainerCourseService teachService = new TrainerCourseService();
        AssignmentCourseService assignService = new AssignmentCourseService();
        public MainMenu()
        {
            Console.WriteLine("Welcome to the database of Private School \n");
            Menu();
        }
        public void Menu()
        {
            bool ContinueOrExit = true;
            do
            {
                Console.WriteLine("If u want to see the data from private school database write 1 in console");
                Console.WriteLine("If u want to insert data to private school database write 2 in console");
                Console.WriteLine("If u want to see the Bonus Menu write 10");
                Console.WriteLine("\nIf u want to exit the program write 0 in console\n");
                string userInput =Console.ReadLine();
                ContinueOrExit = CheckToContinueOrExit(userInput);
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        DisplayMenu();
                        break;
                    case "2":
                        Console.Clear();
                        InsertMenu();
                        break;
                    case "10":
                        Console.Clear();
                        BonusMenu();
                        break;
                    case "0":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Menu();
                        break;
                }
            } while (ContinueOrExit);
            Console.WriteLine("\nYou exit the program. Have a nice day\n");
        }
        public void BonusMenu()
        {
            bool ContinueOrExit = true;
            do
            {
                Console.WriteLine("If u want to delete data from private school database write 1 in console");
                Console.WriteLine("If u want to update data from private school database write 2 in console");
                Console.WriteLine("If u want to see data by ID from private school database write 3 in console");
                Console.WriteLine("\nIf u want to return to the Main Menu write 0 in console\n");
                string userInput = Console.ReadLine();
                ContinueOrExit = CheckToContinueOrExit(userInput);
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        DeleteMenu();
                        break;
                    case "2":
                        Console.Clear();
                        UpdateMenu();
                        break;
                    case "3":
                        Console.Clear();
                        GetByIdMenu();
                        break;
                    case "0":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        BonusMenu();
                        break;
                }
            } while (ContinueOrExit);
        }
        public bool CheckToContinueOrExit(string check)
        {
            return check == "0" ? false : true;
        }

        public void UpdateMenu()
        {
            bool ContinueOrExit = true;
            do
            {
                Console.WriteLine("If u want to update a student to the private school data base write 1 in console");
                Console.WriteLine("If u want to update a trainer to the private school data base write 2 in console");
                Console.WriteLine("If u want to update a course to the private school data base write 3 in console");
                Console.WriteLine("If u want to update a assignment to the private school data base write 4 in console");
                Console.WriteLine("If u want to update the relationship between student and course to the private school data base write 5 in console");
                Console.WriteLine("If u want to update the relationship between trainer and course to the private school data base write 6 in console");
                Console.WriteLine("If u want to update the relationship between assignment and course to the private school data base write 7 in console");
                Console.WriteLine("\nIf u want to return to Bonus Menu write 0 from keyboard\n");
                string userInput = Console.ReadLine();
                ContinueOrExit = CheckToContinueOrExit(userInput);
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        studentService.Update();
                        break;
                    case "2":
                        Console.Clear();
                        trainerService.Update();
                        break;
                    case "3":
                        Console.Clear();
                        courseService.Update();
                        break;
                    case "4":
                        Console.Clear();
                        assignmentService.Update();
                        break;
                    case "5":
                        Console.Clear();
                        watchService.Update();
                        break;
                    case "6":
                        Console.Clear();
                        teachService.Update();
                        break;
                    case "7":
                        Console.Clear();
                        assignService.Update();
                        break;
                    case "0":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        UpdateMenu();
                        break;
                }
            } while (ContinueOrExit);
        }
        public void GetByIdMenu()
        {
            bool ContinueOrExit = true;
            do
            {
                Console.WriteLine("If u want to get a student by ID from the private school data base write 1 in console");
                Console.WriteLine("If u want to get a trainer by ID from the private school data base write 2 in console");
                Console.WriteLine("If u want to get a course by ID from the private school data base write 3 in console");
                Console.WriteLine("If u want to get a assignment by ID from the private school data base write 4 in console");
                Console.WriteLine("If u want to get a relationship between student and course by ID from the to the private school data base write 5 in console");
                Console.WriteLine("If u want to get a relationship between trainer and course by ID from the private school data base write 6 in console");
                Console.WriteLine("If u want to get a relationship between assignment and course by ID from the private school data base write 7 in console");
                Console.WriteLine("\nIf u want to return to Bonus Menu write 0 from keyboard\n");
                string userInput = Console.ReadLine();
                ContinueOrExit = CheckToContinueOrExit(userInput);
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        studentService.GetById();
                        break;
                    case "2":
                        Console.Clear();
                        trainerService.GetById();
                        break;
                    case "3":
                        Console.Clear();
                        courseService.GetById();
                        break;
                    case "4":
                        Console.Clear();
                        assignmentService.GetById();
                        break;
                    case "5":
                        Console.Clear();
                        watchService.GetById();
                        break;
                    case "6":
                        Console.Clear();
                        teachService.GetById();
                        break;
                    case "7":
                        Console.Clear();
                        assignService.GetById();
                        break;
                    case "0":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        GetByIdMenu();
                        break;
                }
            } while (ContinueOrExit);
        }
        public void DeleteMenu()
        {
            bool ContinueOrExit = true;
            do
            {
                Console.WriteLine("If u want to delete a student to the private school data base write 1 in console");
                Console.WriteLine("If u want to delete a trainer to the private school data base write 2 in console");
                Console.WriteLine("If u want to delete a course to the private school data base write 3 in console");
                Console.WriteLine("If u want to delete a assignment to the private school data base write 4 in console");
                Console.WriteLine("If u want to delete the relationship between student and course to the private school data base write 5 in console");
                Console.WriteLine("If u want to delete the relationship between trainer and course to the private school data base write 6 in console");
                Console.WriteLine("If u want to delete the relationship between assignment and course to the private school data base write 7 in console");
                Console.WriteLine("\nIf u want to return to Bonus Menu write 0 from keyboard\n");
                string userInput = Console.ReadLine();
                ContinueOrExit = CheckToContinueOrExit(userInput);
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        studentService.Delete();
                        break;
                    case "2":
                        Console.Clear();
                        trainerService.Delete();
                        break;
                    case "3":
                        Console.Clear();
                        courseService.Delete();
                        break;
                    case "4":
                        Console.Clear();
                        assignmentService.Delete();
                        break;
                    case "5":
                        Console.Clear();
                        watchService.Delete();
                        break;
                    case "6":
                        Console.Clear();
                        teachService.Delete();
                        break;
                    case "7":
                        Console.Clear();
                        assignService.Delete();
                        break;
                    case "0":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        DeleteMenu();
                        break;
                }
            } while (ContinueOrExit);
        }
        public void InsertMenu()
        {
            bool ContinueOrExit = true;
            do
            {
                Console.WriteLine("If u want to insert a student to the private school data base write 1 in console");
                Console.WriteLine("If u want to insert a trainer to the private school data base write 2 in console");
                Console.WriteLine("If u want to insert a course to the private school data base write 3 in console");
                Console.WriteLine("If u want to insert a assignment to the private school data base write 4 in console");
                Console.WriteLine("If u want to relate student with course to the private school data base write 5 in console");
                Console.WriteLine("If u want to relate trainer with course to the private school data base write 6 in console");
                Console.WriteLine("If u want to relate assignment with course to the private school data base write 7 in console");
                Console.WriteLine("\nIf u want to return to Main Menu write 0 from keyboard\n");
                string userInput = Console.ReadLine();
                ContinueOrExit = CheckToContinueOrExit(userInput);
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        studentService.Create();
                        break;
                    case "2":
                        Console.Clear();
                        trainerService.Create();
                        break;
                    case "3":
                        Console.Clear();
                        courseService.Create();
                        break;
                    case "4":
                        Console.Clear();
                        assignmentService.Create();
                        break;
                    case "5":
                        Console.Clear();
                        watchService.Create();
                        break;
                    case "6":
                        Console.Clear();
                        teachService.Create();
                        break;
                    case "7":
                        Console.Clear();
                        assignService.Create();
                        break;
                    case "0":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        InsertMenu();
                        break;
                }
            } while (ContinueOrExit);
        }
        public void DisplayMenu()
        {
            bool ContinueOrExit = true;
            do
            {
                Console.WriteLine("If u want to see the students of the private school write 1 in console");
                Console.WriteLine("If u want to see the trainers of the private school write 2 in console");
                Console.WriteLine("If u want to see the courses of the private school write 3 in console");
                Console.WriteLine("If u want to see the assignments of the private school write 4 in console");
                Console.WriteLine("If u want to see the students for each course of the private school write 5 in console");
                Console.WriteLine("If u want to see the trainers for each course of the private school write 6 in console");
                Console.WriteLine("If u want to see the assignments for each course of the private school write 7 in console");
                Console.WriteLine("If u want to see the courses for each student of the private school write 8 in console");
                Console.WriteLine("If u want to see the assignments for each course for each student of the private school write 9 in console");
                Console.WriteLine("\nIf u want to return to Main Menu write 0 from keyboard\n");
                string userInput = Console.ReadLine();
                ContinueOrExit = CheckToContinueOrExit(userInput);
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        studentService.Display();
                        break;
                    case "2":
                        Console.Clear();
                        trainerService.Display();
                        break;
                    case "3":
                        Console.Clear();
                        courseService.Display();
                        break;
                    case "4":
                        Console.Clear();
                        assignmentService.Display();
                        break;
                    case "5":
                        Console.Clear();
                        courseService.DisplayStudentsPerCourse();
                        break;
                    case "6":
                        Console.Clear();
                        courseService.DisplayTrainersPerCourse();
                        break;
                    case "7":
                        Console.Clear();
                        courseService.DisplayAssignmentsPerCourse();
                        break;
                    case "8":
                        Console.Clear();
                        studentService.DisplayMultyCoursesPerStudent();
                        break;
                    case "9":
                        Console.Clear();
                        studentService.DisplayAssignmentsPerCoursePerStudent();
                        break;
                    case "0":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        DisplayMenu();
                        break;
                }
            } while (ContinueOrExit);
        }
    }
}
