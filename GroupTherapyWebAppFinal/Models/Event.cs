using System.ComponentModel.DataAnnotations;

// The structure for the Pet Events part of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class Event
    {
        [Required]
        public int ScheduleID { get; set; }
        [Required]
        [StringLength(30)]
        public string EventName { get; set; }
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
