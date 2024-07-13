using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleverPointApi.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        public Ticket()
        {
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }

        [Required]
        public string ShipmentID { get; set; }
        [Required]
        public string ShipmentTrackingNumber { get; set; }

        [Required]
        public double EstimatedStoryPoints { get; set; }
        public Nullable<double> SpentStoryPoints { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public Nullable<DateTime> DateClosed { get; set; }

        [Required]
        public int StatusId { get; set; }
        public Status? Status { get; set; }

        public int CreatorUserId { get; set; }
        public User? CreatorUser { get; set; }

        public Nullable<int> AssigneeUserId { get; set; }
        public User? AssigneeUser { get; set; }
    }
}
