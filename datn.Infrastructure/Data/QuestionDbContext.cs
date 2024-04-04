using datn.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
