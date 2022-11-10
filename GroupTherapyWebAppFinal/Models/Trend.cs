using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// The structure for the Pet Trends part of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class Trend
    {
        [Required]
        public int PetID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        [ForeignKey("PetID")]
        public Pet Pet { get; set; }

        public Trend()
        { 
            
        }
    }
}
