using System.Net;

namespace UserManagementApp.Application.Exceptions
{
    public class ConflictException : Exception
    {
        protected int ErrorCode = (int)HttpStatusCode.Conflict;

        public ConflictException() : base()
        {
        }

        public ConflictException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}