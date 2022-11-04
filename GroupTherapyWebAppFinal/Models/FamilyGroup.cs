using System.ComponentModel.DataAnnotations;

// The structure for the Family Groups of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class FamilyGroup
    {
        [Required]
        [Key]
        public int GroupID { get; set; }
        [StringLength(30)]
        public string? FamilyName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        [StringLength(10)]
        public string? MemberStatus { get; set; }

        public FamilyGroup()
        { 
            
        }
    }
}
