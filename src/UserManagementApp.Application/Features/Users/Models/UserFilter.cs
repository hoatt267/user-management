using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementApp.Application.Features.Users.Models
{
    public class UserFilter
    {
        public string? SearchKey { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}