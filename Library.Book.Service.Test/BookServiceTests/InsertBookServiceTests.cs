using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Library.Book.Service.Data.Entities;
using Library.Book.Service.Data.Repositories;
using Library.Book.Service.Enums;
using Library.Book.Service.ServiceModels;
using Library.Common.Contracts.Events.BookEvents;
using MassTransit;
using Moq;
using Xunit;

namespace Library.Book.Service.Test.BookServiceTests
{
    public class InsertBookServiceTests
    {
        public InsertBookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            var busControlMock = new Mock<IBusControl>();

            busControlMock.Setup(s => s.Publish(new BookCreated(), It.IsAny<CancellationToken>()))
                          .Returns(() => Task.CompletedTask);

            _bookRepositoryMock.Setup(t => t.Insert(It.IsAny<EBook>()))
                               .Returns(() => new EBook
                                              {
                                                  Id = 1, No = 1
                                                  , Tag = "tag"
                                                  , Name = "name"
                                                  , Created = DateTime.Now
                                                  , UserId = 1
                                                  , ShelfId = 1
                                                  , AuthorId = "id"
                                                  , SkinType = 1
                                                  , LibraryId = "lib"
                                                  , PublishDate = DateTime.Now
                                                  , PublisherId = 1
                                                  , Updated = DateTime.Now
                                                  , CreatedBy = "me"
                                                  , UpdatedBy = "me"
                                              })
                               .Verifiable();
            _bookRepositoryMock.Setup(t => t.SaveChanges()).Verifiable();
            _bookRepositoryMock.Setup(t => t.Delete(It.IsAny<EBook>())).Verifiable();
            _bookRepositoryMock.Setup(t => t.GetOne(It.IsAny<int>())).Returns(new EBook()
                                                                              {
                                                                                  Id = 1, No = 1
                                                                                  , Tag = "tag"
                                                                                  , Name = "name"
                                                                                  , Created = DateTime.Now
                                                                                  , UserId = 1
                                                                                  , ShelfId = 1
                                                                                  , AuthorId = "id"
                                                                                  , SkinType = 1
                                                                                  , LibraryId = "lib"
                                                                                  , PublishDate =
                                                                                      DateTime.Now
                                                                                  , PublisherId = 1
                                                                                  , Updated = DateTime.Now
                                                                                  , CreatedBy = "me"
                                                                                  , UpdatedBy = "me"
                                                                              });

            _bookService = new BookService(_bookRepositoryMock.Object, busControlMock.Object);
        }

        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly BookService           _bookService;

        [Theory]
        [MemberData(nameof(InvalidParameters))]
        public void WhenInputIsNotValidated_ThrowValidationException(InsertBookServiceModel input)
        {
            Assert.Throws<ValidationException>(() => _bookService.InsertBook(input));
        }

        public static IEnumerable<object[]> InvalidParameters = new List<object[]>
                                                                {
                                                                    new object[]
                                                                    {
                                                                        new InsertBookServiceModel
                                                                        {
                                                                            Name = null
                                                                        }
                                                                    }
                                                                    , new object[]
                                                                      {
                                                                          new InsertBookServiceModel
                                                                          {
                                                                              Name = "name"
                                                                          }
                                                                      }
                                                                };

        [Fact]
        public void WhenInsertIsCompleted_VerifyRepositoryIsCompleted()
        {
            _bookService.InsertBook(new InsertBookServiceModel
                                    {
                                        Name = "name", UserId = 1, No = 1, Tag = "", ShelfId = 1, AuthorId = ""
                                        , SkinType = SkinType.Cover, LibraryId = "", PublisherId = 1
                                        , PublisherDate = DateTime.Now
                                    });

            _bookRepositoryMock.Verify(t => t.Insert(It.IsAny<EBook>()), Times.Once);
        }

        [Fact]
        public void WhenInsertIsCompleted_VerifySaveChangesIsCompleted()
        {
            _bookService.InsertBook(new InsertBookServiceModel
                                    {
                                        Name = "name", UserId = 1, No = 1, Tag = "", ShelfId = 1, AuthorId = ""
                                        , SkinType = SkinType.Cover, LibraryId = "", PublisherId = 1
                                        , PublisherDate = DateTime.Now
                                    });

            _bookRepositoryMock.Verify(t => t.SaveChanges(), Times.Once);
        }
    }
}