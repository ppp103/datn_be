using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetTopicFlatQueryHandler : IRequestHandler<GetTopicFlatQuery, List<TopicDto>>
    {
        private readonly ITopicRepository _topicRepository;

        public GetTopicFlatQueryHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;

        }
        public async Task<List<TopicDto>> Handle(GetTopicFlatQuery request, CancellationToken cancellationToken)
        {
            return await _topicRepository.GetTopicFlatAsync(request.ParentId);
        }
    }
}
