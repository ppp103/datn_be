using MediatR;
using datn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class QuestionTestQueryHandler : IRequestHandler<QuestionTestQuery, List<QuestionTestDto>>
    {
        public Task<List<QuestionTestDto>> Handle(QuestionTestQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
