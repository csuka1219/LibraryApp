namespace ReaderClient.Data
{
    public class LoanData
    {
        public int ReaderId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime LoanDate { get; set; }

        public DateTime ReturnDeadline { get; set; }
    }
}
