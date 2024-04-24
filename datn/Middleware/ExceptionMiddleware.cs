using datn.Domain;

namespace datn.API
{
    public class ExceptionMiddleware
    {
        /// <summary>
		/// Trường lưu trữ delegate của middleware tiếp theo
		/// </summary>
		/// Created by: TDPhuc (15/08/2023)
		private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// Created by: TDPhuc (15/08/2023)
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Hàm gọi đến Middleware tiếp theo nếu không lỗi
        /// </summary>
        /// <param name="context"></param>
        /// Created by: TDPhuc (15/08/2023)
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Hàm xử lý Exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns>Trả về lỗi 404, 409 hoặc 500</returns>
        /// Created by: TDPhuc (19/08/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            Console.WriteLine(ex);
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;

                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = 404,
                            UserMessage = "Không tìm thấy tài nguyên",
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                        );
                    break;
                case Domain.SystemException systemException:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = 500,
                            UserMessage = "Lỗi hệ thống! Vui lòng liên hệ với quản trị viên để được hỗ trợ",
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                        );
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = 500,
                            UserMessage = "Lỗi hệ thống",
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? "");
                    break;
            }
        }
    }
}
