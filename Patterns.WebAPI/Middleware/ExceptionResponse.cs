using System;

namespace Patterns.WebAPI.Middleware
{
    public class ExceptionResponse
    {
        public string Source { get; private set; }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }

        public ExceptionResponse(Exception exception)
        {
            Source = exception.Source;
            Message= exception.Message;
            StackTrace = exception.StackTrace;
        }
    }
}