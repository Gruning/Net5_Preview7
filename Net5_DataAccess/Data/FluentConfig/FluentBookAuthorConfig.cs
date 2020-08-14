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
    public class FluentBookAuthorConfig : IEntityTypeConfiguration<FluentBookAuthor>
    {
        public void Configure(EntityTypeBuilder<FluentBookAuthor> modelBuilder)
        {
            ////many to many relation books authors
            modelBuilder.HasKey(ba => new { ba.AuthorId, ba.BookId });
            modelBuilder
                .HasOne(b => b.FluentBook)
                .WithMany(b => b.FluentBookAuthors).HasForeignKey(b => b.BookId);
            modelBuilder
                .HasOne(b => b.FluentAuthor)
                .WithMany(b => b.FluentBookAuthors).HasForeignKey(b => b.AuthorId);

        }
    }
}
