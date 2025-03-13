using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public class StudentHours
    {
        [Column("hours_id")]
        public int HoursId { get; set; } // מזהה ייחודי לשעות

        [Column("teacher_id")]
        public int TeacherId { get; set; } // מזהה המורה

        [Column("date")]
        public DateTime Date { get; set; } // תאריך הפעילות

        [Column("hours")]
        public double Hours { get; set; } // מספר השעות

        [Column("description")]
        public string Description { get; set; } // תיאור הפעילות

        [Column("status")]
        public string Status { get; set; } // סטטוס (מאושר/לא מאושר)
    }
}
