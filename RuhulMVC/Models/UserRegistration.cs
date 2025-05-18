using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RuhulMVC.Models
{
    public class UserRegistration
    {

        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        [EmailAddress]
        [Length(10, 40)]
        public required string Email { get; set; }

        [PasswordPropertyText]
        [Length(6, 12)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }






    }
}
