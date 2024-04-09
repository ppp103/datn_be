using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class Question : BaseAuditEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public string CorrectOption {  get; set; }

        public string Explaination { get;set; }

        public string ImageUrl { get; set; }

        public int ChuDeId { get; set; }

        public int LoaiCauDeId { get; set; }

        //public int TestId { get; set; }

        //public Test Test { get; set; }
    }
}
