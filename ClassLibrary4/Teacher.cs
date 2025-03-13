using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public class Teacher
    {
        [Column("teacher_id")]
        public int TeacherId { get; set; } // Unique identifier for the teacher

        [Column("specialization")]
        public string Specialization { get; set; } // Specialization

        [Column("experience_years")]
        public int ExperienceYears { get; set; } // Years of experience

        [Column("bio")]
        public string Bio { get; set; } // Biography
    }
}
