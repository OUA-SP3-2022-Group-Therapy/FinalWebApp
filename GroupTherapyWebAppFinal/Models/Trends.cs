using System.ComponentModel.DataAnnotations;

namespace GroupTherapyWebAppFinal.Models
{
    public class Trends
    {
        [Required]
        [StringLength(12)]
        [Key]
        public int PetID { get; set; }
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
