using System.Net;

namespace UserManagementApp.Domain.Exceptions
{
    public class DataInvalidException : Exception
    {
        protected int ErrorCode = (int)HttpStatusCode.BadRequest;

        public DataInvalidException() : base()
        {
        }

        public DataInvalidException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public DataInvalidException(string message) : base(message)
        {
        }

        public DataInvalidException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}