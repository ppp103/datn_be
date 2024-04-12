using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetQuestionByIdQuery : IRequest<QuestionDto>
    {
        public int QuestionId { get; set; }



    }
}
