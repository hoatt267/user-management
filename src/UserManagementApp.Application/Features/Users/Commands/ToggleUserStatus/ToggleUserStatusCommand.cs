using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace UserManagementApp.Application.Features.Users.Commands.ToggleUserStatus
{
    public class ToggleUserStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}