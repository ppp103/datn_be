﻿using datn.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public class QuestionTestConfiguration : IEntityTypeConfiguration<QuestionTest>
    {
        public void Configure(EntityTypeBuilder<QuestionTest> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();

        }
    }
}
