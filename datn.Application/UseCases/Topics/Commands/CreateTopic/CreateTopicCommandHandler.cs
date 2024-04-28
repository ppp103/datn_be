using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, TopicDto>
    {
        private readonly ITopicRepository _topicRepository;

        public CreateTopicCommandHandler(ITopicRepository topicRepository)
        {

            _topicRepository = topicRepository;

        }
        public async Task<TopicDto> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topicDto = new TopicDto() 
            { 
                Name = request.Name,
                ParentId = request.ParentId,
            };
            var res = await _topicRepository.CreateTopicAsync(topicDto);
            return res;
        }
    }
}
