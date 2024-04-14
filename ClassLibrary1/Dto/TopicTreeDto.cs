using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class TopicTreeDto
    {
            public int Id { get; set; }

            public string Name { get; set; }

            public int Level { get; set; }

            public int? ParentId { get; set; }

            public List<TopicTreeDto> Child { get; set; }
    }
}
