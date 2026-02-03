using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using UserManagementApp.Application.Features.Users.DTOs;
using UserManagementApp.Application.Features.Users.Models;
using UserManagementApp.Domain.Values;

namespace UserManagementApp.Application.Features.Users.Queries.GetUsers
{
    public record GetUsersQuery(UserFilter userFilter) : IRequest<PaginatedList<UserDto>>;
}