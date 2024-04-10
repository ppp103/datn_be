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
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
            builder.Property(x => x.Content).HasColumnName("Content").IsRequired(false);
            builder.Property(x => x.Option1).HasColumnName("Option1").IsRequired(false);
            builder.Property(x => x.Option2).HasColumnName("Option2").IsRequired(false);
            builder.Property(x => x.Option3).HasColumnName("Option3").IsRequired(false);
            builder.Property(x => x.Option4).HasColumnName("Option4").IsRequired(false);
            builder.Property(x => x.CorrectOption).HasColumnName("CorrectOption").IsRequired(false);
            builder.Property(x => x.Explaination).HasColumnName("Explaination").IsRequired(false);
            builder.Property(x => x.ImageUrl).HasColumnName("ImageUrl").IsRequired(false);

        }
    }
}
