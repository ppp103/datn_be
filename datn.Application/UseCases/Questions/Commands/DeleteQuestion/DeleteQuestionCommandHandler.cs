using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, int>
    {
        private readonly IQuestionRepository _questionRepository;

        public DeleteQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public async Task<int> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            return await _questionRepository.DeleteAsync(request.Id);
        }
    }
}
