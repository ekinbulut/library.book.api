using System.Collections.Generic;
using Library.Book.Service.Data.Entities;
using Library.Common.Data;

namespace Library.Book.Service.Data.Repositories
{
    public interface IBookRepository : IRepository<EBook>
    {
        IEnumerable<EBook> GetByUserId(int userId);
    }
}