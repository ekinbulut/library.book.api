using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Library.Book.Service.Responses
{
    [ExcludeFromCodeCoverage]
    public class GetBookHttpResponse
    {
        public int Total { get; set; }
        public IEnumerable<BookHttpResponse> Books { get; set; }
    }
}