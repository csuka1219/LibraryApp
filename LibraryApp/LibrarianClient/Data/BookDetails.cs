namespace LibrarianClient.Data
{
    public class BookDetails
    {
        public BookDetails(string status)
        {
            Status = status;
        }

        public BookDetails(string status, string renter, DateTime deadline)
        {
            Status = status;
            Renter = renter;
            DeadLine = deadline;
        }

        public string Status { get; set; }

        public string? Renter { get; set; }

        public DateTime? DeadLine { get; set; }
    }
}
