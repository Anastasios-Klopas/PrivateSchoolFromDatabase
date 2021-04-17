using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolFromDatabase.Services
{
    interface ICrable<T> where T:class
    {
        List<T> GetAll();
        T GetById();
        void Create();
        void Display();
        void Update();
        void Delete();
        void DisplayIdOnly();
    }
}
