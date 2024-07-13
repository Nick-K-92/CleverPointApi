namespace CleverPointApi.Models
{
    public class TicketFilters
    {
        public TicketFilters()
        {
        }

        public string? SearchQuery { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public int? StatusId { get; set; }
        public int? StoryPoints { get; set; }
    }
}
