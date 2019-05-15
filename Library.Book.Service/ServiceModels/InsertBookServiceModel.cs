﻿using System;
using Library.Book.Service.Enums;

namespace Library.Book.Service.ServiceModels
{
    public class InsertBookServiceModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string AuthorId { get; set; }
        public int PublisherId { get; set; }
        public string Tag { get; set; } //genre
        public int No { get; set; }
        public DateTime PublisherDate { get; set; }
        public SkinType SkinType { get; set; }
        public string LibraryId { get; set; }
        public int ShelfId { get; set; }
    }
}