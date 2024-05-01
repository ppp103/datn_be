using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class AppConstants
    {
        public const int MaxPageSize = 2147483646;
        public const int MinPageSize = 10;
    }

    public enum PracticeTestEnum
    {
        UserId = 0,
        TestId = 1,
    }

    public enum Role
    {
        User = 0,
        Admin = 1,
    }
}
