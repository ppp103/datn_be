using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateTopicCommand : IRequest<TopicDto>
    {
        public string Name { get; set; }

        public int? ParentId { get; set; }
    }
}
