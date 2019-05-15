using Library.Book.Service.Requests;
using Library.Book.Service.Responses;
using Library.Book.Service.ServiceModels;

namespace Library.Book.Service
{
    public interface IBookService
    {
        BookServiceModelResponse GetBooks(BookServiceModelRequest request);
        void InsertBook(InsertBookServiceModel input);
        void Delete(int userId, int id);
        BookServiceModelResponse GetBook(int userId, int id);
        void Update(int bookId, PutBookServiceModel putBookServiceModel);
    }
}