using System.Diagnostics.CodeAnalysis;
using Library.Book.Service.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Book.Service.Data
{
    [ExcludeFromCodeCoverage]
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BookDbContext()
        {
        }

        public DbSet<EBook> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
        }
    }

    [ExcludeFromCodeCoverage]
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<EBook>
    {
        public void Configure(EntityTypeBuilder<EBook> builder)
        {
            builder.Property(t => t.CreatedBy).IsRequired(false);
            builder.Property(t => t.UpdatedBy).IsRequired(false);
        }
    }
}