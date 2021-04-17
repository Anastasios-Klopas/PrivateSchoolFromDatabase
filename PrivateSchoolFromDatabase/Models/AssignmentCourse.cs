using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Models
{
    class AssignmentCourse
    {
        public int AssignmentCourseId { get; set; }
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"AssignID: {AssignmentCourseId} \n");
            sb.Append($"AssignmentID: {AssignmentId} \n");
            sb.Append($"CourseID: {CourseId} \n");
            return sb.ToString();
        }
    }
}
