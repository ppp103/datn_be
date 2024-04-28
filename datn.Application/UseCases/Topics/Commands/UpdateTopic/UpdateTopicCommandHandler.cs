using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdateTopicCommandHandler : IRequestHandler<UpdateTopicCommand, TopicDto>
    {
        private readonly ITopicRepository _topicRepository;

        public UpdateTopicCommandHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }
        public async Task<TopicDto> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var UpdateTopicEntity = new TopicDto()
            {
                Id = request.Id,
                Name = request.Name,
                ParentId = request.ParentId,
            };
            return await _topicRepository.UpdateTopicAsync(request.Id, UpdateTopicEntity);
        }
    }
}
