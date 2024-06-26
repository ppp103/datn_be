﻿using AutoMapper;
using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionDto>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionByIdQueryHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<QuestionDto> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);

            return _mapper.Map<QuestionDto>(question);
        }
    }
}
