using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetPracticeTestByTypeIdQuery : PagingRequest, IRequest<PagedList<PracticeTestDto>>
    {
        public int TypeId { get; set; }
        public int Type {  get; set; }
    }
}
