using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// The structure for the Schedule part of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class Schedule
    {
        [Required]
        [Key]
        public int ScheduleID { get; set; }
        [StringLength(30, ErrorMessage = "Schedule name cannot be longer than 30 characters.")]
        public string? ScheduleName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }
        [StringLength(10)]
        public string? ScheduleType { get; set; }
        [StringLength(10)]
        public string? Frequency { get; set; }
        [StringLength(20, ErrorMessage = "Dosage cannot be longer than 20 characters.")]
        public string? Dose { get; set; }
        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters.")]
        [DisplayFormat(NullDisplayText = "No description")]
        public string? Description { get; set; }
        [Required]
        public int FamilyGroupID { get; set; }
        [ForeignKey("FamilyGroupID")]
        public FamilyGroup FamilyGroup { get; set; }

        public Schedule()
        { 

        } 
    }
}
