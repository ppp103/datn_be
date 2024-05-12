﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class User : BaseAuditEntity
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public int Role {  get; set; }

        public string? ImgLink { get; set; }

        public int IsActive { get; set; }
    }
}
