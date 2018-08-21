using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDB.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Dept { get; set; }
    }
}