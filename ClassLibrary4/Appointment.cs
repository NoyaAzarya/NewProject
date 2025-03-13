using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public class Appointment
    {
        [Column("appointment_id")]
        public int AppointmentId { get; set; } // מזהה ייחודי לפגישה

        [Column("student_id")]
        public int StudentId { get; set; } // מזהה התלמיד

        [Column("teacher_id")]
        public int TeacherId { get; set; } // מזהה המורה

        [Column("date")]
        public DateTime Date { get; set; } // תאריך ושעה של הפגישה

        [Column("status")]
        public string Status { get; set; } // סטטוס הפגישה (פעילה/מבוטלת)
    }
}
