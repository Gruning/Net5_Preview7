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
    public class FluentBookDetailsConfig : IEntityTypeConfiguration<FluentBookDetail>
    {
        public void Configure(EntityTypeBuilder<FluentBookDetail> modelBuilder)
        {
            modelBuilder.HasKey(a => a.BookDetail_Id);
            modelBuilder.Property(a => a.NumberOfChapters).IsRequired();

        }
    }
}
