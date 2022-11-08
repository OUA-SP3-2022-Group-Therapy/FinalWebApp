using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// The structure for the Pet Events part of the database - Joshua Wagner
namespace GroupTherapyWebAppFinal.Models
{
    public class Event
    {
        [Required]
        public int ScheduleID { get; set; }
        [StringLength(30, ErrorMessage = "Event name cannot be longer than 30 characters.")]
        [Required]
        public string EventName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }
        [StringLength(20)]
        public string? EventStatus { get; set; }
        [StringLength(30)]
        [DisplayFormat(NullDisplayText = "NA")]
        public string? CompletedBy { get; set; }
        [ForeignKey("ScheduleID")]
        public Schedule Schedule { get; set; }
    }
}
