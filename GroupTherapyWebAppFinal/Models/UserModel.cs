using System.ComponentModel.DataAnnotations;

// The structure for the users portion of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class UserModel
    {
        [Key]
        [Required]
        public int UserID { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(20)]
        public string? UserType { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? DateCreated { get; set; }

        public UserModel()
        {

        }
    }
}
