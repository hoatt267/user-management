using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using UserManagementApp.Application.Features.Users.DTOs;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Exceptions;
using UserManagementApp.Domain.Repositories;

namespace UserManagementApp.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(
                selector: u => _mapper.Map<UserDto>(u),
                predicate: u => u.Id == request.id,
                disableTracking: true
            ) ?? throw new EntityNotFoundException(nameof(User), request.id);

            return user;
        }
    }
}