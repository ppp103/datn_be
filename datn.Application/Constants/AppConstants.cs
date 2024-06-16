using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public static class AppConstants
    {
        public const string TEST_IMAGE_ROOT = "https://localhost:7253/images/tests/";
        public const string USER_IMAGE_ROOT = "https://localhost:7253/images/user/";
        public const string DEFAULT_EMAIL= "trandinhphuc1410@gmail.com";



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

    public enum DifficultyLevel
    {
        Easy = 1,
        Normal = 2,
        Hard = 3
    }
}
