using System.Diagnostics.CodeAnalysis;

namespace Library.Book.Service.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetBookHttpRequest
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}