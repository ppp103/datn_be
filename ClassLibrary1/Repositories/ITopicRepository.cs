using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public interface ITopicRepository
    {
        Task<List<TopicTreeDto>> GetTopicTreeAsync();
    }
}
