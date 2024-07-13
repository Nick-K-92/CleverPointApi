using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleverPointApi.Models
{
    [Table("Status")]
    public class Status
    {
        public Status()
        {
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public bool IsFirstStatusOfTicket { get; set; }
    }
}
