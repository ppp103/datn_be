using datn.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();

            //// Define one-to-many relationship with Question
            //builder.HasMany(t => t.Questions)
            //       .WithOne(q => q.Test)
            //       .HasForeignKey(q => q.TestId);
        }
    }
}
