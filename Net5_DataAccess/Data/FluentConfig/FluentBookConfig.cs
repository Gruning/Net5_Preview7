using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net5_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5_DataAccess.Data.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<FluentBook>
    {

        public void Configure(EntityTypeBuilder<FluentBook> builder)
        {

            throw new NotImplementedException();
            //Book
            //modelBuilder.Entity<FluentBook>().HasKey(b => b.Book_Id);
            //modelBuilder.Entity<FluentBook>().Property(b => b.ISBN).IsRequired().HasMaxLength(15);
            //modelBuilder.Entity<FluentBook>().Property(b => b.Title).IsRequired();
            //modelBuilder.Entity<FluentBook>().Property(b => b.Price).IsRequired();
            ////one to oone relation between book and bookdetail
            //modelBuilder.Entity<FluentBook>()
            //    .HasOne(b => b.FluentBookDetail)
            //    .WithOne(b => b.FluentBook).HasForeignKey<FluentBook>("BookDetail_Id");
            ////one to many relation publisher and book
            //modelBuilder.Entity<FluentBook>()
            //    .HasOne(b => b.FluentPublisher)
            //    .WithMany(b => b.FluentBooks).HasForeignKey(b => b.Publisher_Id);
            ////many to many relation books authors
            //modelBuilder.Entity<FluentBookAuthor>().HasKey(ba => new { ba.AuthorId, ba.BookId });
            //modelBuilder.Entity<FluentBookAuthor>()
            //    .HasOne(b => b.FluentBook)
            //    .WithMany(b => b.FluentBookAuthors).HasForeignKey(b => b.BookId);
            //modelBuilder.Entity<FluentBookAuthor>()
            //    .HasOne(b => b.FluentAuthor)
            //    .WithMany(b => b.FluentBookAuthors).HasForeignKey(b => b.AuthorId);
        }
    }
}