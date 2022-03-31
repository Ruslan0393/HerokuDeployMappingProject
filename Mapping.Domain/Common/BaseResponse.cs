namespace Mapping.Domain.Common
{
    public class BaseResponse<T> where T : class
    {
        public BaseResponse(T data = null, ErrorModel error = null)
        {
            Data = data;
            Error = error;
        }
        public T Data { get; set; }
        public ErrorModel Error { get; set; }
    }
}
