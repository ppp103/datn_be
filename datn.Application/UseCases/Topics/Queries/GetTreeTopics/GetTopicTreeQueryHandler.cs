using AutoMapper;
using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetTopicTreeQueryHandler : IRequestHandler<GetTopicTreeQuery, List<TopicTreeDto>>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public GetTopicTreeQueryHandler(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }
        public async Task<List<TopicTreeDto>> Handle(GetTopicTreeQuery request, CancellationToken cancellationToken)
        {
            return await _topicRepository.GetTopicTreeAsync();
        }
    }
}
