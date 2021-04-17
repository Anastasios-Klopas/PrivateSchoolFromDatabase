using PrivateSchoolFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.ViewData
{
    class AssignmentsPerCoursePerStudent
    {
        public Student Student { get; set; }
        public List<AssignmentsPerCourse> AssignmentsPerCourses;
        public AssignmentsPerCoursePerStudent()
        {
            AssignmentsPerCourses = new List<AssignmentsPerCourse>();
        }
    }
}
