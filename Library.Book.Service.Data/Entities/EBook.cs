using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Library.Common.Data;

namespace Library.Book.Service.Data.Entities
{
    [Table("BOOKS")]
    [ExcludeFromCodeCoverage]
    public class EBook : BaseEntity
    {
        [Column("USER_ID")] public int UserId { get; set; }

        [Column("NAME")] public string Name { get; set; }

        [Column("PUBLISHER_ID")] public int PublisherId { get; set; }

        [Column("AUTHOR_ID")] public string AuthorId { get; set; }

        [Column("PUBLISH_DATE")] public DateTime PublishDate { get; set; }

        [Column("GENRE")] public string Tag { get; set; }

        [Column("NO")] public int No { get; set; }

        [Column("COVER_STATUS")] public int SkinType { get; set; }

        [Column("LIBRARY_ID")] public string LibraryId { get; set; }

        [Column("SHELF_NUMBER")] public int ShelfId { get; set; }
    }
}