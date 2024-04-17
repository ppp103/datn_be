using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class PracticeTestDto
    {
        public int Id { get; set; }

        // Lần thi
        public int TakeTimes { get; set; }

        // Thời gian làm
        public DateTime Time { get; set; }

        public int Result { get; set; }

        public int UserId { get; set; }

        public int TestId { get; set; }
    }
}
