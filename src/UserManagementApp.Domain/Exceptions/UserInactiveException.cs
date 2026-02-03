using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementApp.Domain.Exceptions
{
    public class UserInactiveException : Exception
    {
        public UserInactiveException() : base($"This user is inactive.")
        {
        }
    }
}