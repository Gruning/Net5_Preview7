using Microsoft.EntityFrameworkCore;
using Net5_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Net5_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        //public DbSet<FluentBookAuthor> FluentAuthors { get; set; }
        public DbSet<FluentBookDetail> FluentBookDetails { get; set; }
        public DbSet<FluentBook> FluentBooks { get; set; }
        public DbSet<FluentAuthor> FluentAuthors { get; set; }
        public DbSet<FluentPublisher> FluentPublisher { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //we configure fluent API

            //category table name and column name
            modelBuilder.Entity<Category>().ToTable("tbl_Category");
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("CategoryName");
            
            //composite key
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.AuthorId, ba.BookId });

            //BookDetails
            modelBuilder.Entity<FluentBookDetail>().HasKey(b => b.BookDetail_Id);
            modelBuilder.Entity<FluentBookDetail>().Property(b => b.NumberOfChapters).IsRequired();

            //Book
            modelBuilder.Entity<FluentBook>().HasKey(b => b.Book_Id);
            modelBuilder.Entity<FluentBook>().Property(b => b.ISBN).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<FluentBook>().Property(b => b.Title).IsRequired();
            modelBuilder.Entity<FluentBook>().Property(b => b.Price).IsRequired();
            //one to oone relation between book and bookdetail
            modelBuilder.Entity<FluentBook>()
                .HasOne(b => b.FluentBookDetail)
                .WithOne(b => b.FluentBook).HasForeignKey<FluentBook>("BookDetail_Id");
            //one to many relation publisher and book
            modelBuilder.Entity<FluentBook>()
                .HasOne(b => b.FluentPublisher)
                .WithMany(b => b.FluentBooks).HasForeignKey(b => b.Publisher_Id);
            //many to many relation books authors
            modelBuilder.Entity<FluentBookAuthor>().HasKey(ba => new { ba.AuthorId, ba.BookId });
            modelBuilder.Entity<FluentBookAuthor>()
                .HasOne(b => b.FluentBook)
                .WithMany(b => b.FluentBookAuthors).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<FluentBookAuthor>()
                .HasOne(b => b.FluentAuthor)
                .WithMany(b => b.FluentBookAuthors).HasForeignKey(b => b.AuthorId);

            //Author
            modelBuilder.Entity<FluentAuthor>().HasKey(a => a.Author_Id);
            modelBuilder.Entity<FluentAuthor>().Property(a => a.FirstName).IsRequired();
            modelBuilder.Entity<FluentAuthor>().Property(a => a.LastName).IsRequired();
            modelBuilder.Entity<FluentAuthor>().Ignore(a => a.FullName);




        }
    }
}
