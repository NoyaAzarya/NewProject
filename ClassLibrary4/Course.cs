using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public class Course
    {
        [Column("course_id")]
        public int CourseId { get; set; } // מזהה ייחודי לקורס

        [Column("course_name")]
        public string CourseName { get; set; } // שם הקורס

        [Column("description")]
        public string Description { get; set; } // תיאור הקורס

        [Column("teacher_id")]
        public int TeacherId { get; set; } // מזהה המורה שמעביר את הקורס
    }
}
