namespace MdbApi.Application.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void LogError(string message, params object[] args);
    }
}