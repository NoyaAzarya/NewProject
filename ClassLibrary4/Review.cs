using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public class Review
    {
        [Column("review_id")]
        public int ReviewId { get; set; } // מזהה ייחודי לביקורת

        [Column("appointment_id")]
        public int AppointmentId { get; set; } // מזהה הפגישה

        [Column("reviewer_id")]
        public int ReviewerId { get; set; } // מזהה כותב הביקורת

        [Column("receiver_id")]
        public int ReceiverId { get; set; } // מזהה מקבל הביקורת

        [Column("rating")]
        public double Rating { get; set; } // דירוג (מספרי)

        [Column("comments")]
        public string Comments { get; set; } // הערות
    }
}
