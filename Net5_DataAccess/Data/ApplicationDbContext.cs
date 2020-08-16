using Microsoft.EntityFrameworkCore;
using Net5_DataAccess.Data.FluentConfig;
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

            modelBuilder.ApplyConfiguration(new FluentBookConfig());

        }
    }
}
