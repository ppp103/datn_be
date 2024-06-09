using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, TestDto>
    {
        private readonly ITestRepository _testRepository;
        public UpdateTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;

        }
        public async Task<TestDto> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var imgLink = "";
            if (request.File != null)
            {
                string image = request.File.FileName;
                imgLink = await UploadFileHelper.UploadFile(request.File, @"tests", image.ToLower());
            }
            var testEntity = new TestDto()
            {
                Id = request.Id,
                TestName = request.TestName,
                Time = request.Time,
                TotalPoint = request.TotalPoint,
                NumberOfQuestions = request.NumberOfQuestion,
                ImgLink = imgLink,
                CreatedDate = DateTime.Now.ToString(),
                TestCategoryId = request.TestCategoryId,
                Ids = request.Ids
            };
            return await _testRepository.UpdateAsync(testEntity);
        }
    }
}
