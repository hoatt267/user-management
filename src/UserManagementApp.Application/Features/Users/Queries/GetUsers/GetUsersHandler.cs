using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using UserManagementApp.Application.Features.Users.DTOs;
using UserManagementApp.Application.Features.Users.Models;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Repositories;
using UserManagementApp.Domain.Values;

namespace UserManagementApp.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserDto>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUsersHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetPaginatedListAsync(
                pageNumber: request.userFilter.PageNumber ?? 1,
                pageSize: request.userFilter.PageSize ?? 10,
                selector: u => _mapper.Map<UserDto>(u),
                predicate: u => string.IsNullOrEmpty(request.userFilter.SearchKey)
                                || u.FullName.Contains(request.userFilter.SearchKey)
                                || u.Email.Contains(request.userFilter.SearchKey),
                disableTracking: true
            );

            return users;
        }
    }
}