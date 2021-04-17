using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Models
{
    class StudentCourse
    {
        public int StudentCourseId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"WatchID: {StudentCourseId} \n");
            sb.Append($"StudentID: {StudentId} \n");
            sb.Append($"CourseID: {CourseId} \n");
            return sb.ToString();
        }
    }
}
