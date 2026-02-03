using System.Net;

namespace UserManagementApp.Domain.Exceptions
{
    public class EntityNotLoadedException : Exception
    {
        protected int ErrorCode = (int)HttpStatusCode.BadRequest;

        public EntityNotLoadedException() : base()
        {
        }

        public EntityNotLoadedException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public EntityNotLoadedException(string message) : base(message)
        {
        }

        public EntityNotLoadedException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}