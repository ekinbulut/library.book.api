using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Library.Book.Service.Data.Entities;
using Library.Book.Service.Data.Repositories;
using Library.Book.Service.Requests;
using Library.Book.Service.Responses;
using Library.Book.Service.ServiceModels;
using Library.Book.Service.Validations;
using Library.Common.Contracts.Events.BookEvents;
using MassTransit;

namespace Library.Book.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBusControl _busControl;

        public BookService(IBookRepository bookRepository, IBusControl busControl)
        {
            _bookRepository = bookRepository;
            _busControl = busControl;
        }

        public BookServiceModelResponse GetBooks(BookServiceModelRequest request)
        {
            var books = _bookRepository.GetByUserId(request.UserId);

            var eBooks = books.ToList();
            var listCount = eBooks.Count();
            var bookServiceModels = eBooks
                .Skip(request.Offset)
                .Take(request.Limit)
                .Select(t => new BookServiceModel
                             {
                                 Id = t.Id, Name = t.Name, Tag = t.Tag, AuthorId = t.AuthorId, No = t.No
                                 , SkinType = t.SkinType, LibraryId = t.LibraryId, PublishDate = t.PublishDate
                                 , PublisherId = t.PublisherId, ShelfId = t.ShelfId
                             });

            return new BookServiceModelResponse
                   {
                       Total = listCount, BookServiceModels = bookServiceModels
                   };
        }

        public BookServiceModelResponse GetBook(int userId, int id)
        {
            var eBook = _bookRepository.GetOne(id);

            return new BookServiceModelResponse
                   {
                       Total = 1, BookServiceModels = new List<BookServiceModel>
                                                      {
                                                          new BookServiceModel
                                                          {
                                                              Id = eBook.Id, Name = eBook.Name, Tag = eBook.Tag
                                                              , AuthorId = eBook.AuthorId, No = eBook.No
                                                              , SkinType = eBook.SkinType, LibraryId = eBook.LibraryId
                                                              , PublishDate = eBook.PublishDate
                                                              , PublisherId = eBook.PublisherId, ShelfId = eBook.ShelfId
                                                          }
                                                      }
                   };
        }


        public void InsertBook(InsertBookServiceModel input)
        {
            var validator = new InsertBookServiceModelValidator();
            validator.ValidateAndThrow(input);

            var record = _bookRepository.Insert(new EBook
                                                {
                                                    UserId = input.UserId, Name = input.Name, AuthorId = input.AuthorId
                                                    , PublisherId = input.PublisherId, PublishDate = input.PublisherDate
                                                    , No = input.No, SkinType = (int) input.SkinType, Tag = input.Tag
                                                    , LibraryId = input.LibraryId, ShelfId = input.ShelfId
                                                });

            _bookRepository.SaveChanges();

            _busControl.Publish(new BookCreated
                                {
                                    BookId = record.Id, AuthorId = input.AuthorId, PublisherId = input.PublisherId
                                    , LibraryId = input.LibraryId, ShelfId = input.ShelfId
                                }).GetAwaiter().GetResult();
        }

        public void Delete(int userId, int id)
        {
            var book = _bookRepository.GetOne(id);

            if (book != null) _bookRepository.Delete(book);


            _bookRepository.SaveChanges();
        }

        public void Update(int bookId, PutBookServiceModel putBookServiceModel)
        {
            _bookRepository.Update(new EBook
                                   {
                                       Id = bookId, No = putBookServiceModel.No, Tag = putBookServiceModel.Tag
                                       , Name = putBookServiceModel.Name, Updated = DateTime.Now
                                       , UserId = putBookServiceModel.UserId, AuthorId = putBookServiceModel.AuthorId
                                       , SkinType = (int) putBookServiceModel.SkinType
                                       , LibraryId = putBookServiceModel.LibraryId
                                       , PublishDate = putBookServiceModel.PublisherDate
                                       , PublisherId = putBookServiceModel.PublisherId
                                       , ShelfId = putBookServiceModel.ShelfId
                                   });

            _bookRepository.SaveChanges();
        }
    }
}