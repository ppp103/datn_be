using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetQuestionQuery : PagingRequest, IRequest<PagedList<Question>>
    {
        public int? ChuDeId { get; set; }
        public int? LoaiCauId { get; set; }
    }
}
