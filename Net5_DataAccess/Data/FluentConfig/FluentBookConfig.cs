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
            //name of table
            //Book
            builder.HasKey(b => b.Book_Id);
            builder.Property(b => b.ISBN).IsRequired().HasMaxLength(15);
            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Price).IsRequired();
            
            //relations
            //one to oone relation between book and bookdetail
            builder
                .HasOne(b => b.FluentBookDetail)
                .WithOne(b => b.FluentBook).HasForeignKey<FluentBook>("BookDetail_Id");
            //one to many relation publisher and book
            builder
                .HasOne(b => b.FluentPublisher)
                .WithMany(b => b.FluentBooks).HasForeignKey(b => b.Publisher_Id);                        
        }
    }
}