namespace Library.Book.Service.Requests
{
    public class BookServiceModelRequest
    {
        public int UserId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}