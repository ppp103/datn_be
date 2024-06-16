using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public interface ITopicRepository
    {
        Task<PagedList<TopicTreeDto>> GetTopicTreeAsync(int page, int pageSize, string keyWord, int? parentId, int? level);
        Task<List<TopicDto>> GetTopicFlatAsync(int parentId);
        Task<TopicDto> CreateTopicAsync(TopicDto topicDto);
        Task<TopicDto> UpdateTopicAsync(int id, TopicDto topicDto);

    }
}
