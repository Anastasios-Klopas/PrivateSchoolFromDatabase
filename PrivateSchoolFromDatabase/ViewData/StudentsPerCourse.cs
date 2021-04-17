using PrivateSchoolFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.ViewData
{
    class StudentsPerCourse
    {
        public Course Course { get; set; }
        public List<Student> Students;
        public StudentsPerCourse()
        {
            Students = new List<Student>();
        }
    }
}
