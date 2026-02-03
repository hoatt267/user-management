using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementApp.Domain.Exceptions
{
    public class InvalidOtpException : Exception
    {
        public InvalidOtpException() : base($"Invalid OTP or expired, please check your email.")
        {
        }
    }
}