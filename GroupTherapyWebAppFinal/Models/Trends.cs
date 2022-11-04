using System.ComponentModel.DataAnnotations;

// The structure for the Pet Trends part of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class Trends
    {
        [Required]
        public int PetID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }

        public Trends()
        { 
            
        }
    }
}
