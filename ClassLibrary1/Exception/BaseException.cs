using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class BaseException
    {
        #region Property
        /// <summary>
        /// Mã lỗi
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Message hiển thị cho developer
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public string? DevMessage { get; set; }

        /// <summary>
        /// Message hiển thị cho người dùng
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public string? UserMessage { get; set; }

        /// <summary>
        /// Id truy vết
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Lỗi
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public object? Errors { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Hàm trả về lỗi
        /// <return>Lỗi dưới dạng Json</return>>
        /// Created by: TDPhuc (15/08/2023)
        /// </summary>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion
    }
}
