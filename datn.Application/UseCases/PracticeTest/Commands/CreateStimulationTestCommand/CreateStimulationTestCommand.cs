using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateStimulationTestCommand : IRequest<PracticeTestDto>
    {

        public int TestId { get; set; }

        public List<AnswerSheetDto> AnswerSheets { get; set; }
    }
}
