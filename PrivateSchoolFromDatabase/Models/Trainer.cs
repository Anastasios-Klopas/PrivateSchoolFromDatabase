using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Models
{
    class Trainer
    {
        public int TrainerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"TrainerID: {TrainerId} \n");
            sb.Append($"FirstName: {FirstName} \n");
            sb.Append($"LastName: {LastName} \n");
            sb.Append($"Subject: {Subject} \n");
            return sb.ToString();
        }
    }
}
