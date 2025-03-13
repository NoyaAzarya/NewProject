using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public class Enrollment
    {
        [Column("enrollment_id")]
        public int EnrollmentId { get; set; } // מזהה ייחודי להרשמה

        [Column("student_id")]
        public int StudentId { get; set; } // מזהה התלמיד

        [Column("course_id")]
        public int CourseId { get; set; } // מזהה הקורס

        [Column("enrollment_date")]
        public DateTime EnrollmentDate { get; set; } // תאריך ההרשמה
    }
}
