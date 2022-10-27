using System.ComponentModel.DataAnnotations;

namespace GroupTherapyWebAppFinal.Models
{
    public class Membership
    {
        [Required]
        [StringLength(12)]
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(12)]
        [Key]
        public int GroupID { get; set; }
        

        public Membership()
        {

        }
    }
}
