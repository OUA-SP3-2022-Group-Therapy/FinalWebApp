using System.ComponentModel.DataAnnotations;

namespace GroupTherapyWebAppFinal.Models
{
    public class Event
    {
        [Required]
        [Key]
        public int ScheduleID { get; set; }
        [StringLength(30)]
        public string? EventName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }
        [StringLength(10)]
        public string? EventStatus { get; set; }
        public string? CompletedBy { get; set; }
    }
}
