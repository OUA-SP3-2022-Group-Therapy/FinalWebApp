using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? UserType { get; set; }
        public string? Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        public Users()
        {

        }
    }
}
