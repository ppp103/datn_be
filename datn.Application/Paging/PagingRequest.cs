using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class PagingRequest
    {
        public string Keyword { get; set; }
        [Required]
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
