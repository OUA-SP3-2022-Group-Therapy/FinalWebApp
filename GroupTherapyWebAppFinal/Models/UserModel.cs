using System.ComponentModel.DataAnnotations;

namespace GroupTherapyWebAppFinal.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        [StringLength(20)]
        public string? UserType { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        public UserModel()
        {

        }
    }
}
