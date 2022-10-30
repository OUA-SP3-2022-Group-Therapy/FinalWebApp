using System.ComponentModel.DataAnnotations;

namespace GroupTherapyWebAppFinal.Models
{
    public class Membership
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int GroupID { get; set; }
        

        public Membership()
        {

        }
    }
}
