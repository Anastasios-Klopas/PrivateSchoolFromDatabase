using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Models
{
    class Assignment
    {
        public int AssignmentId { get; set; }
        public string AssignmentTitle { get; set; }
        public string Description { get; set; }
        public DateTime SubDateTime { get; set; }
        public double OralMark { get; set; }
        public double TotalMark { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"AssignmentID: {AssignmentId} \n");
            sb.Append($"AssignmentTitle: {AssignmentTitle} \n");
            sb.Append($"Description: {Description} \n");
            sb.Append($"SubDateTime: {SubDateTime.ToShortDateString()} \n");
            sb.Append($"OralMark: {OralMark} \n");
            sb.Append($"TotalMark: {TotalMark} \n");
            return sb.ToString();
        }
    }
}
