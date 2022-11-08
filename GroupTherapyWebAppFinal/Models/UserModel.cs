using System.ComponentModel.DataAnnotations;

// The structure for the users portion of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class UserModel
    {
        [Key]
        [Required]
        public int UserModelID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Password cannot be longer than 30 characters.")]
        [DataType(DataType.Password)]        
        public string Password { get; set; }
        [StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        [DisplayFormat(NullDisplayText = "Name not added")]
        public string? Name { get; set; }
        [StringLength(20)]
        [DisplayFormat(NullDisplayText = "User type not selected")]
        public string? UserType { get; set; }
        [StringLength(10)]
        [DisplayFormat(NullDisplayText = "Gender not selected")]
        public string? Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }

        public UserModel()
        {

        }
    }
}
