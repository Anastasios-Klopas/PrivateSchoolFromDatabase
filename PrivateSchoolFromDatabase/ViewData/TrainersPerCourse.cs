using PrivateSchoolFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.ViewData
{
    class TrainersPerCourse
    {
        public Course Course { get; set; }
        public List<Trainer> Trainers;
        public TrainersPerCourse()
        {
            Trainers = new List<Trainer>();
        }
    }
}
