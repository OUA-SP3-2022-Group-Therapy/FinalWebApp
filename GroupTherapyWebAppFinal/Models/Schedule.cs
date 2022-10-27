using System.ComponentModel.DataAnnotations;

namespace GroupTherapyWebAppFinal.Models
{
    public class Schedule
    {
        [Required]
        [StringLength(12)]
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
        public string? Type { get; set; }
        [StringLength(10)]
        public string? Frequency { get; set; }
        public int? Dose { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        [Required]
        [StringLength(12)]
        public int GroupID { get; set; }

        public Schedule()
        { 

        } 
    }
}
