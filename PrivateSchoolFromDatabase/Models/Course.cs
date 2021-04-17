using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Models
{
    class Course
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"CourseID: {CourseId} \n");
            sb.Append($"Title: {CourseTitle} \n");
            sb.Append($"Stream: {Stream} \n");
            sb.Append($"StartDate: {StartDate.ToShortDateString()} \n");
            sb.Append($"EndDate: {EndDate.ToShortDateString()} \n");
            return sb.ToString();
        }
    }
}
