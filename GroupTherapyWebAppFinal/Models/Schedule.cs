using System.ComponentModel.DataAnnotations;

// The structure for the Schedule part of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class Schedule
    {
        [Required]
        [Key]
        public int ScheduleID { get; set; }
        [StringLength(30)]
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
        [StringLength(20)]
        public string? Dose { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        [Required]
        public int GroupID { get; set; }

        public Schedule()
        { 

        } 
    }
}
