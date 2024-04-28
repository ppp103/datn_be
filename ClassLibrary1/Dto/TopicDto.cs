using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class TopicDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }    
    }
}
