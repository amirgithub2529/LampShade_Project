using A_Test.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Test.Infrastructure.EFCore
{
    public class InfoMapping : IEntityTypeConfiguration<Info>
    {
        public void Configure(EntityTypeBuilder<Info> builder)
        {
            //builder.HasKey(x => x.Id);
            builder.ToTable("Info");

            //builder.Property(x => x.Name).HasMaxLength(50);
            //builder.Property(x => x.Family).HasMaxLength(50);


        }
    }
}
