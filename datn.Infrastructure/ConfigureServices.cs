using datn.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QuestionDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("QuestionDbContext") ?? 
                    throw new InvalidOperationException("connection string 'QuestionDbContext not found'"));
            });
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuestionCategoryRepository, QuestionCategoryRepository>();
            services.AddTransient<ITestRepository, TestRepository>();
            services.AddTransient<ITopicRepository, TopicRepository>();

            return services;
        }
    }
}
