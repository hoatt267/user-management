using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementApp.Domain.Exceptions
{
    public class InvalidInformationLoginException : Exception
    {
        public InvalidInformationLoginException() : base($"Invalid information login, please check your username or password.")
        {
        }
    }
}