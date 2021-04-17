using PrivateSchoolFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.ViewData
{
    class AssignmentsPerCourse
    {
        public Course Course { get; set; }
        public List<Assignment> Assignments;
        public AssignmentsPerCourse()
        {
            Assignments = new List<Assignment>();
        }
    }
}
