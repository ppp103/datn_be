using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class TestCategory : BaseAuditEntity
    {
        public int Id { get; set; }

        public string TestCategoryName { get; set; }
    }
}
