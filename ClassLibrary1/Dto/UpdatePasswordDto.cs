using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class UpdatePasswordDto
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string Email { get; set; }   
    }
}
