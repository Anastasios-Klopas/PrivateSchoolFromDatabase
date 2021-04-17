using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Models
{
    class TrainerCourse
    {
        public int TrainerCourseId { get; set; }
        public int TrainerId { get; set; }
        public int CourseId { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"TeachID: {TrainerCourseId} \n");
            sb.Append($"TrainerID: {TrainerId} \n");
            sb.Append($"CourseID: {CourseId} \n");
            return sb.ToString();
        }
    }
}
