using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using Library.Book.Service;
using Library.Book.Service.Requests;
using Library.Book.Service.Responses;
using Library.Book.Service.ServiceModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Book.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/{userId}/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;


        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(GetBookHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromRoute] int userId, [FromQuery] GetBookHttpRequest request)
        {
            var bookServiceModelResponse = _bookService.GetBooks(new BookServiceModelRequest
                                                                 {
                                                                     UserId = userId, Offset = request.Offset
                                                                     , Limit = request.Limit
                                                                 });

            IEnumerable<BookHttpResponse> books = new List<BookHttpResponse>();
            if (bookServiceModelResponse.Total > 0)
                books = bookServiceModelResponse.BookServiceModels.Select(t => new BookHttpResponse
                                                                               {
                                                                                   Id = t.Id, Name = t.Name
                                                                                   , AuthorId = t.AuthorId
                                                                                   , PublishDate = t.PublishDate
                                                                                   , PublisherId = t.PublisherId
                                                                                   , No = t.No, Tag = t.Tag
                                                                                   , SkinType = t.SkinType
                                                                                   , LibraryId = t.LibraryId
                                                                                   , ShelfId = t.ShelfId
                                                                               });

            var httpResponse = new GetBookHttpResponse
                               {
                                   Total = bookServiceModelResponse.Total, Books = books
                               };

            return StatusCode((int) HttpStatusCode.OK, httpResponse);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(GetBookHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromRoute] int userId, [FromRoute] int id)
        {
            var bookServiceModelResponse = _bookService.GetBook(userId, id);

            IEnumerable<BookHttpResponse> books = new List<BookHttpResponse>();

            if (bookServiceModelResponse.Total > 0)
                books = bookServiceModelResponse.BookServiceModels.Select(t => new BookHttpResponse
                                                                               {
                                                                                   Name = t.Name, AuthorId = t.AuthorId
                                                                                   , PublishDate = t.PublishDate
                                                                                   , PublisherId = t.PublisherId
                                                                                   , No = t.No, Tag = t.Tag
                                                                                   , SkinType = t.SkinType
                                                                                   , LibraryId = t.LibraryId
                                                                                   , ShelfId = t.ShelfId
                                                                               });

            var httpResponse = new GetBookHttpResponse
                               {
                                   Total = bookServiceModelResponse.Total, Books = books
                               };

            return StatusCode((int) HttpStatusCode.OK, httpResponse);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Put([FromRoute] int userId, [FromRoute] int id, [FromBody] PutBookHttpRequest request)
        {
            //TODO : update model


            var serviceModel = new PutBookServiceModel
                               {
                                   UserId = userId, Name = request.Name, AuthorId = request.AuthorId
                                   , PublisherId = request.PublisherId, PublisherDate = request.PublisherDate
                                   , No = request.No, SkinType = request.SkinType, Tag = request.Tag
                                   , LibraryId = request.LibraryId, ShelfId = request.ShelfId
                               };

            _bookService.Update(id, serviceModel);


            return StatusCode((int) HttpStatusCode.Accepted);
        }


        [Route("")]
        [HttpPost]
        public IActionResult Post([FromRoute] int userId, [FromBody] PostBookHttpRequest request)
        {
            var bookServiceModel = new InsertBookServiceModel
                                   {
                                       UserId = userId, Name = request.Name, AuthorId = request.AuthorId
                                       , PublisherId = request.PublisherId, PublisherDate = request.PublisherDate
                                       , No = request.No, SkinType = request.SkinType, Tag = request.Tag
                                       , LibraryId = request.LibraryId, ShelfId = request.ShelfId
                                   };

            _bookService.InsertBook(bookServiceModel);

            return StatusCode((int) HttpStatusCode.Created);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete([FromRoute] int userId, [FromRoute] int id)
        {
            _bookService.Delete(userId, id);

            return StatusCode((int) HttpStatusCode.OK);
        }
    }
}