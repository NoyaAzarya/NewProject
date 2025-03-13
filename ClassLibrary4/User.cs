using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary5
{
    public class User
    {
     
        public int UserId { get; set; } // ממופה ל-user_id // 0

       
        public string FirstName { get; set; } // ממופה ל-first_name // 1

     
        public string LastName { get; set; } // ממופה ל-last_name // 2

       
        public string Email { get; set; } // ממופה ל-email // 3

        
        public string Role { get; set; } // ממופה ל-role // 5

        public string Password { get; set; } // 4

    }
}
