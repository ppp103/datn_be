using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdateTestCommand : CreateTestCommand
    {
        public int Id { get; set; }
    }
}
