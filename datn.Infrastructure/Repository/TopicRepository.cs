using Azure.Core;
using datn.Application;
using datn.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace datn.Infrastructure
{
    public class TopicRepository : ITopicRepository
    {

        private readonly QuestionDbContext _questionDbContext;

        public TopicRepository(QuestionDbContext questionDbContext)
        {

            _questionDbContext = questionDbContext;

        }

        public async Task<TopicDto> CreateTopicAsync(TopicDto topicRequest)
        {
            var topic = new Topic()
            {
                Name = topicRequest.Name,
                ParentId = topicRequest.ParentId,
            };

            var res = await _questionDbContext.Topics.AddAsync(topic);
            await _questionDbContext.SaveChangesAsync();

            return topicRequest;
        }

        public async Task<List<TopicDto>> GetTopicFlatAsync(int parentId)
        {
            var query = from topic in _questionDbContext.Topics
                        select new TopicDto()
                        {
                            Id = topic.Id,
                            Name = topic.Name,
                            ParentId = topic.ParentId
                        };

            if (parentId != null && parentId != 0)
            {
                query = query.Where(q => q.ParentId == parentId);
            }

            //var res = query.OrderByDescending(t => t.Id).ToList();

            return query.OrderByDescending(q => q.Id).ToList();
        }

        public async Task<PagedList<TopicTreeDto>> GetTopicTreeAsync(int page, int pageSize, string keyWord, int? parentId, int? level)
        {
            var queue = new Queue<TopicTreeDto>();
            var lstCheck = new List<TopicTreeDto>();
            var result = new List<TopicTreeDto>();

            //Expression<Func<CTermEntity, bool>> filter = t => t.DeletedAt == AppConstants.deleted_at && t.Type.ToLower() == request.Type.ToLower();

            //var entity = _ctermRepository.GetQueryable(filter);

            var entity = _questionDbContext.Topics;

            var data = from d in entity
                       select new TopicTreeDto
                       {
                           Id = d.Id,
                           Name = d.Name,
                           ParentId = d.ParentId,
                       };

            var parents = data.Where(x => x.ParentId == null);

            foreach (var item in parents)
            {
                item.Level = 1;
                queue.Enqueue(item);
                lstCheck.Add(item);
                result.Add(item);
            }

            while (queue.Count > 0)
            {
                var temp = queue.Dequeue();

                if (lstCheck.All(x => x.Id != temp.Id))
                {
                    result.Add(temp);
                }

                var childs = data.Where(x => x.ParentId != null);

                if (!childs.Any())
                    continue;

                var childLevel = temp.Level + 1;

                foreach (var item in childs)
                {
                    if (lstCheck.Any(x => x.Id == item.Id))
                        continue;

                    if (item.ParentId != temp.Id)
                        continue;

                    item.Level = childLevel;

                    queue.Enqueue(item);
                    lstCheck.Add(item);

                    temp.Child ??= new List<TopicTreeDto>();
                    temp.Child.Add(item);
                }
            }
            var resFinal = await PagedList<TopicTreeDto>.CreateAsync(result, page, pageSize);

            //return Task.FromResult(result);
            return await Task.FromResult(resFinal);
        }

        public async Task<TopicDto> UpdateTopicAsync(int id, TopicDto topicDto)
        {
            await _questionDbContext.Topics.Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters.
                    SetProperty(m => m.Id, topicDto.Id).
                    SetProperty(m => m.Name, topicDto.Name).
                    SetProperty(m => m.ParentId, topicDto.ParentId)
                );

            return topicDto;
        }
    }
}
