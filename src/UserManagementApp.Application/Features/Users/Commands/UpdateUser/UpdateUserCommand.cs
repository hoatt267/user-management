using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using UserManagementApp.Application.Features.Users.DTOs;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}