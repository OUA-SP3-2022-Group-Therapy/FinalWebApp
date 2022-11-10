using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// The structure for the Memberships within the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class Membership
    {
        [Required]
        public int UserModelID { get; set; }
        [Required]
        public int FamilyGroupID { get; set; }
        public int IsAdmin { get; set; }
        [ForeignKey("UserModelID")]
        public UserModel UserModel { get; set; }
        [ForeignKey("FamilyGroupID")]
        public FamilyGroup FamilyGroup { get; set; }
        public Membership()
        {

        }
    }
}
