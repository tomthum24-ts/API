namespace API
{
    public static class Response
    {
        public static Response<T> Fail<T>(string message, T data = default) =>
            new Response<T>(data, message, true);
        public static Response<T> Ok<T>(string message, T data = default) =>
            new Response<T>(data, message, false);
    }
    public class Response<T>
    {
        public Response(T data, string msg,bool error)
        {
            Data = data;
            msg = msg;
            Error=error;
        }
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
    }
}
