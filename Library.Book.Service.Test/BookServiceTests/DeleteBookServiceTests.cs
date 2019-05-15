using Library.Book.Service.Data.Entities;
using Library.Book.Service.Data.Repositories;
using Moq;
using Xunit;

namespace Library.Book.Service.Test.BookServiceTests
{
    public class DeleteBookServiceTests
    {
        public DeleteBookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();

            _bookRepositoryMock.Setup(t => t.Insert(It.IsAny<EBook>())).Verifiable();
            _bookRepositoryMock.Setup(t => t.SaveChanges()).Verifiable();
            _bookRepositoryMock.Setup(t => t.Delete(It.IsAny<EBook>())).Verifiable();
            _bookRepositoryMock.Setup(t => t.GetOne(It.IsAny<int>())).Returns(new EBook());
            _bookService = new BookService(_bookRepositoryMock.Object, null);
        }

        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly BookService _bookService;


        [Fact]
        public void WhenDeleteOperationCompleted_ReturnVoid()
        {
            _bookService.Delete(1, 1);

            _bookRepositoryMock.Verify(t => t.Delete(It.IsAny<EBook>()), Times.Once);
            _bookRepositoryMock.Verify(t => t.SaveChanges(), Times.Once);
        }
    }
}