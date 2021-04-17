using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Models
{
    class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double TuitionFee { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"StudentID: {StudentId} \n");
            sb.Append($"FirstName: {FirstName} \n");
            sb.Append($"LastName: {LastName} \n");
            sb.Append($"DateOfBirth: {DateOfBirth.ToShortDateString()} \n");
            sb.Append($"TuitionFee: {TuitionFee} \n");
            return sb.ToString();
        }
    }
}
