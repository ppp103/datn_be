﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Định nghĩa Exception xử lý khi không tìm thấy dữ liệu
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        /// <summary>
        /// Mã lỗi
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Constructor
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public NotFoundException() { }

        /// <summary>
        /// Exception có mã lỗi
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Exception có message
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// Exception có message và mã lỗi
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
