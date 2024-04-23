using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class TestDto
    {
        public int Id { get; set; }

        public string TestName { get; set; }

        public int Time { get; set; }

        public int NumberOfQuestions { get; set; }

        public int TotalPoint {  get; set; }

        public string? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }  

        public string? ImgLink { get; set; }

        public List<int> Ids { get; set; }

        public string TestCategoryName { get; set; }

        public int TestCategoryId { get; set; } 
    }
}
