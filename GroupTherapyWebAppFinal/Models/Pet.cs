using System.ComponentModel.DataAnnotations;

namespace GroupTherapyWebAppFinal.Models
{
    public class Pet
    {
        [Required]
        [StringLength(12)]
        [Key]
        public int PetID { get; set; }
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Breed { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        public string? Allergies { get; set; }
        [Required]
        [StringLength(12)]
        public int GroupID { get; set; }

        public Pet()
        {

        }
    }
}
