using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public class Student
    {
        [Column("student_id")]
        public int StudentId { get; set; } // Unique identifier for the student

        [Column("learning_level")]
        public string Level { get; set; } // Learning level (beginner/intermediate/advanced)

        [Column("interests")]
        public string Interests { get; set; } // Interests
    }
}
