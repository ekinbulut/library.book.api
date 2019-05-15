using System;
using Library.Book.Service.Data.Entities;
using Library.Book.Service.Data.Repositories;
using Library.Book.Service.Enums;
using Library.Book.Service.ServiceModels;
using Moq;
using Xunit;

namespace Library.Book.Service.Test.BookServiceTests
{
    public class UpdateBookServiceTest
    {
        public UpdateBookServiceTest()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookRepositoryMock.Setup(t => t.Update(It.IsAny<EBook>())).Verifiable();
            _bookRepositoryMock.Setup(t => t.SaveChanges()).Verifiable();

            _bookService = new BookService(_bookRepositoryMock.Object, null);
        }

        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly BookService           _bookService;

        [Fact]
        public void WhenBookRepositoryUpdateMethod_VerifySaveChanges()
        {
            _bookService.Update(It.IsAny<int>(), new PutBookServiceModel()
                                                 {
                                                     No = 1, Tag = "", ShelfId = 1, AuthorId = ""
                                                     , SkinType = SkinType.Cover, LibraryId = "", PublisherId = 1
                                                     , PublisherDate = DateTime.Now, Name = "name", UserId = 1
                                                 });

            _bookRepositoryMock.Verify(t => t.SaveChanges());
        }

        [Fact]
        public void WhenBookRepositoryUpdateMethod_VerifyUpdate()
        {
            _bookService.Update(It.IsAny<int>(), new PutBookServiceModel()
                                                 {
                                                     No = 1, Tag = "", ShelfId = 1
                                                     , AuthorId = ""
                                                     , SkinType = SkinType.Cover
                                                     , LibraryId = "", PublisherId = 1
                                                     , PublisherDate = DateTime.Now
                                                     , Name = "name", UserId = 1
                                                 });

            _bookRepositoryMock.Verify(t => t.Update(It.IsAny<EBook>()));
        }
    }
}