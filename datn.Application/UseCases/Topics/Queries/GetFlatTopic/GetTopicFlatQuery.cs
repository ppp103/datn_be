using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetTopicFlatQuery : IRequest<List<TopicDto>>
    {
        public int ParentId;
    }
}
