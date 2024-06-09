using datn.Domain;
using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetQuestionExcelFileQuery : PagingRequest, IRequest<ExcelPackage>
    {
        public int? ChuDeId { get; set; }
        public int? LoaiCauId { get; set; }
        public int? DifficultyLevel { get; set; }
    }
}
