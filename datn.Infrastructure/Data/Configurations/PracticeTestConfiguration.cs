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
    public class PracticeTestConfiguration : IEntityTypeConfiguration<PracticeTest>
    {
        public void Configure(EntityTypeBuilder<PracticeTest> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
        }
    }
}
