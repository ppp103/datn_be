using datn.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public class QuestionDbContext : DbContext
    {
        public QuestionDbContext(DbContextOptions<QuestionDbContext> dbContextOption) 
            : base(dbContextOption) 
        {
            
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<QuestionTest> QuestionTests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AnswerSheetConfiguration());
            builder.ApplyConfiguration(new PracticeTestConfiguration());
            builder.ApplyConfiguration(new QuestionCategoryConfiguration());
            builder.ApplyConfiguration(new QuestionConfiguration());
            builder.ApplyConfiguration(new QuestionTestConfiguration());
            builder.ApplyConfiguration(new TestCategoryConfiguration());
            builder.ApplyConfiguration(new TestConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());


        }
    }
}
