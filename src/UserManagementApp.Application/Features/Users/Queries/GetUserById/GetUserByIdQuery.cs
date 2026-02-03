using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using UserManagementApp.Application.Features.Users.DTOs;

namespace UserManagementApp.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid id) : IRequest<UserDto>;
}