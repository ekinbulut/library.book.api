using System;
using System.Collections.Generic;
using System.Linq;
using Library.Book.Service.Data.Entities;
using Library.Book.Service.Data.Repositories;
using Library.Book.Service.Requests;
using Library.Book.Service.ServiceModels;
using Moq;
using Xunit;

namespace Library.Book.Service.Test.BookServiceTests
{
    public class GetBookServiceTests
    {
        public GetBookServiceTests()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(t => t.GetByUserId(It.IsAny<int>()))
                              .Returns(() => new List<EBook>
                                             {
                                                 new EBook
                                                 {
                                                     Id = 1, No = 1, Tag = "tag", Name = "name"
                                                     , Created = Convert.ToDateTime("01.01.2019"), UserId = 1, ShelfId = 1
                                                     , AuthorId = "id", SkinType = 1, LibraryId = "lib"
                                                     , PublishDate = Convert.ToDateTime("01.01.2019"), PublisherId = 1
                                                     , Updated =Convert.ToDateTime("01.01.2019"), CreatedBy = "me"
                                                     , UpdatedBy = "me"
                                                 }
                                             });

            bookRepositoryMock.Setup(t => t.GetOne(It.IsAny<int>())).Returns(() => new EBook
                                                                                   {
                                                                                       Id = 1, No = 1
                                                                                       , Tag = "tag"
                                                                                       , Name = "name"
                                                                                       , Created = Convert.ToDateTime("01.01.2019")
                                                                                       , UserId = 1
                                                                                       , ShelfId = 1
                                                                                       , AuthorId = "id"
                                                                                       , SkinType = 1
                                                                                       , LibraryId = "lib"
                                                                                       , PublishDate = Convert.ToDateTime("01.01.2019")
                                                                                           
                                                                                       , PublisherId = 1
                                                                                       , Updated = Convert.ToDateTime("01.01.2019")
                                                                                       , CreatedBy = "me"
                                                                                       , UpdatedBy = "me"
                                                                                   });

            _bookService = new BookService(bookRepositoryMock.Object, null);
        }

        private readonly BookService _bookService;

        [Fact]
        public void WhenBookRepositoryGetByUserId_ReturnsBooks()
        {
            var actual = _bookService.GetBooks(new BookServiceModelRequest
                                               {
                                                   UserId = It.IsAny<int>(), Limit = 10, Offset = 0
                                               });

            var bookServiceModel = actual.BookServiceModels.FirstOrDefault();

            Assert.Equal(1, actual.Total);
            Assert.IsType<BookServiceModel>(bookServiceModel);
            Assert.Equal(1, bookServiceModel.Id);
            Assert.Equal("name", bookServiceModel.Name);
            Assert.Equal(1, bookServiceModel.PublisherId);
            Assert.Equal(Convert.ToDateTime("01.01.2019"), bookServiceModel.PublishDate);
            Assert.Equal("id", bookServiceModel.AuthorId);
            Assert.Equal("tag", bookServiceModel.Tag);
            Assert.Equal(1, bookServiceModel.No);
            Assert.Equal(1, bookServiceModel.SkinType);
            Assert.Equal("lib", bookServiceModel.LibraryId);
            Assert.Equal(1, bookServiceModel.ShelfId);

        }

        [Fact]
        public void WhenBookRepositoryGetOne_ReturnsBooks()
        {
            var actual = _bookService.GetBook(It.IsAny<int>(), It.IsAny<int>());
            Assert.Equal(1, actual.Total);
        }
    }
}