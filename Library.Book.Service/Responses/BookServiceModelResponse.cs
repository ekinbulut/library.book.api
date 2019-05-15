using System.Collections.Generic;
using Library.Book.Service.ServiceModels;

namespace Library.Book.Service.Responses
{
    public class BookServiceModelResponse
    {
        public int Total { get; set; }

        public IEnumerable<BookServiceModel> BookServiceModels { get; set; }
    }
}