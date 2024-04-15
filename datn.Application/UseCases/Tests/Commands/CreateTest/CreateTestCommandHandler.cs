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
    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, Test>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public CreateTestCommandHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public Task<Test> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
