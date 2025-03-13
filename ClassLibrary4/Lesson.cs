using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public class Lesson
    {
        [Column("lesson_id")]
        public int LessonId { get; set; } // מזהה ייחודי לשיעור

        [Column("course_id")]
        public int CourseId { get; set; } // מזהה הקורס

        [Column("lesson_title")]
        public string LessonTitle { get; set; } // כותרת השיעור

        [Column("content")]
        public string Content { get; set; } // תוכן השיעור

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } // תאריך יצירת השיעור
    }
}
