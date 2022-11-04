using System.ComponentModel.DataAnnotations;

// The structure for the Memberships within the database - Joshua Wagner
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
