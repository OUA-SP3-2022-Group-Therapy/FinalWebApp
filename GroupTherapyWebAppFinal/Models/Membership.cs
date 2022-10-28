using System.ComponentModel.DataAnnotations;

namespace GroupTherapyWebAppFinal.Models
{
    public class Membership
    {
        [Required]
        [Key]
        public int UserID { get; set; }
        [Required]
        [Key]
        public int GroupID { get; set; }
        

        public Membership()
        {

        }
    }
}
