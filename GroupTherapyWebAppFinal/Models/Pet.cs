using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// The structure for the Pets part of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class Pet
    {
        [Required]
        [Key]
        public int PetID { get; set; }
        [StringLength(100, ErrorMessage = "Pet name cannot be longer than 100 characters.")]
        [Required]
        public string Name { get; set; }
        [StringLength(50, ErrorMessage = "Species name cannot be longer than 50 characters.")]
        [DisplayFormat(NullDisplayText = "Species not selected")]
        public string? Species { get; set; }
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "Breed not selected")]
        public string? Breed { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        [StringLength(50, ErrorMessage = "Allergies statement cannot be longer than 300 characters.")]
        [DisplayFormat(NullDisplayText = "No allergies added")]
        public string? Allergies { get; set; }
        public int FamilyGroupID { get; set; }
        [ForeignKey("FamilyGroupID")]
        public FamilyGroup FamilyGroup { get; set; }

        public Pet()
        {

        }
    }
}
