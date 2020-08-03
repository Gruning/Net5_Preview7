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
    public class FluentPublisherConfig : IEntityTypeConfiguration<FluentPublisher>
    {
        public void Configure(EntityTypeBuilder<FluentPublisher> bulder)
        {
            //Publisher
            modelBuilder.Entity<FluentPublisher>().HasKey(p => p.Publisher_Id);
            modelBuilder.Entity<FluentPublisher>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<FluentPublisher>().Property(p => p.Location).IsRequired();
        }
    }
}
