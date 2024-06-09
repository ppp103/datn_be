using datn.Domain;
using MediatR;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetQuestionExcelFileQueryHandler : IRequestHandler<GetQuestionExcelFileQuery, ExcelPackage>
    {
        private readonly IQuestionRepository _questionRepository;
        public GetQuestionExcelFileQueryHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;

        }

        public async Task<ExcelPackage> Handle(GetQuestionExcelFileQuery request, CancellationToken cancellationToken)
        {
            var totalColumn = 12;
            var filteredQuestion = await _questionRepository.GetAllQuestionPaggingAsync(1, -1, request.Keyword, request.ChuDeId, request.LoaiCauId, request.DifficultyLevel);

            if (filteredQuestion is null)
            {
                throw new NotFoundException("Không lấy được data");
            }

            var questions = filteredQuestion.Items.ToList();

            string fileName = "Danh_sach_cau_hoi.xlsx";
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            string filePath = Path.Combine(downloadsPath, fileName);

            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Danh sách câu hỏi");

            // Style excel sheet
            worksheet.DefaultColWidth = 15;
            worksheet.DefaultRowHeight = 30;
            worksheet.OutLineApplyStyle = true;
            // Tiêu đề
            string title = "Danh sách câu hỏi";
            var titleRange = worksheet.Cells["A1"];
            worksheet.Cells["A1:L1"].Merge = true;
            worksheet.Cells["A2:L2"].Merge = true;

            titleRange.Value = title;
            titleRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.Size = 16;


            // Set độ rộng cột
            worksheet.Column(1).Width = 5;
            worksheet.Column(2).Width = 95;
            worksheet.Column(11).Width = 32;

            // Center text ở tất cả các cột, trừ câu hỏi và giải thích <cột câu hỏi chỉ align column>
            for (int i = 1; i <= totalColumn; i++)
            {
                if(i == 2 || i == 11)
                {
                    continue;
                }
                worksheet.Column(i).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            worksheet.Column(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Column(2).Style.WrapText = true;

            worksheet.Column(11).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Column(11).Style.WrapText = true;

            // Header của bảng
            var headerRange = worksheet.Cells["A3:L3"];
            headerRange.Style.Font.Bold = true;
            worksheet.Cells["A3:L3"].Merge = false;

            // Căn giữa header
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            var headerRow = 3;

            // Tiêu đề cột
            worksheet.Cells[headerRow, 1].Value = "STT";
            worksheet.Cells[headerRow, 2].Value = "Nội dung câu hỏi";
            worksheet.Cells[headerRow, 3].Value = "Độ khó";
            worksheet.Cells[headerRow, 4].Value = "Chủ đề";
            worksheet.Cells[headerRow, 5].Value = "Loại câu hỏi";
            worksheet.Cells[headerRow, 6].Value = "Đáp án 1";
            worksheet.Cells[headerRow, 7].Value = "Đáp án 2";
            worksheet.Cells[headerRow, 8].Value = "Đáp án 3";
            worksheet.Cells[headerRow, 9].Value = "Đáp án 4";
            worksheet.Cells[headerRow, 10].Value = "Đáp án đúng";
            worksheet.Cells[headerRow, 11].Value = "Giải thích";
            worksheet.Cells[headerRow, 12].Value = "Thời gian";

            // Dữ liệu
            int row = 4;
            var stt = 1;

            // Ghi dữ liệu
            foreach (var question in questions)
            {
                worksheet.Cells[row, 1].Value = stt;
                worksheet.Cells[row, 2].Value = question.Content;

                worksheet.Cells[row, 3].Value = question.DifficultyLevel;

                worksheet.Cells[row, 4].Value = question.ChuDe;
                worksheet.Cells[row, 5].Value = question.LoaiCau;
                worksheet.Cells[row, 6].Value = question.Option1;
                worksheet.Cells[row, 7].Value = question.Option2;
                worksheet.Cells[row, 8].Value = question.Option3;
                worksheet.Cells[row, 9].Value = question.Option4;
                worksheet.Cells[row, 10].Value = question.CorrectOption;
                worksheet.Cells[row, 11].Value = question.Explaination;
                worksheet.Cells[row, 12].Value = question.Time;

                if (question.DifficultyLevel == (int)DifficultyLevel.Easy)
                {
                    //worksheet.Cells[row, 3].Value = question.DifficultyLevel;

                    worksheet.Cells[row, 3].Value = "Dễ";
                }
                else if(question.DifficultyLevel == (int)DifficultyLevel.Normal)
                {
                    //worksheet.Cells[row, 3].Value = question.DifficultyLevel;

                    worksheet.Cells[row, 3].Value = "Trung bình";
                }
                else
                {
                    //worksheet.Cells[row, 3].Value = question.DifficultyLevel;

                    worksheet.Cells[row, 3].Value = "Khó";
                }

                stt++;
                row++;
            }

            // Tạo file
            FileInfo excelFile = new FileInfo(filePath);
            excelPackage.SaveAs(excelFile);
            return excelPackage;
            //throw new NotImplementedException();
        }
    }
}
