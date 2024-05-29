using datn.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateTestCommand : IRequest<TestDto>
    {
        public string? TestName { get; set; }

        public int Time { get; set; }

        public int TotalPoint {  get; set; }

        public int NumberOfQuestion { get; set; }

        public string? CreatedBy { get; set; }

        //public string? ImgLink {  get; set; }
        public IFormFile? File { get; set; }

        public int TestCategoryId {  get; set; }

        public List<int>? Ids { get; set; }
    }
}
