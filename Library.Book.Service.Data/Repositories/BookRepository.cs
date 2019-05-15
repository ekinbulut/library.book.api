using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Library.Book.Service.Data.Entities;
using Library.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Book.Service.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BookRepository : BaseRepository<EBook>, IBookRepository
    {
        public BookRepository(DbContext dbcontext) : base(dbcontext)
        {
        }

        public IEnumerable<EBook> GetByUserId(int userId)
        {
            return Dbcontext.Set<EBook>().Where(t => t.UserId == userId).Select(t => t);
        }
    }
}