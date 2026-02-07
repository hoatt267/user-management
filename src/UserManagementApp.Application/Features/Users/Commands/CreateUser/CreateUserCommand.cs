using MediatR;
using UserManagementApp.Application.Features.Users.DTOs;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}