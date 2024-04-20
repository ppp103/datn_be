using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class AnswerSheet : BaseAuditEntity
    {
        public int Id { get; set; }

        public int QuesitonId { get; set; }

        public int PracticeTestId { get; set; }

        public string? ChosenOption {  get; set; }

        public int Point { get; set; }  

        public bool IsCorrect { get; set; }

    }
}
