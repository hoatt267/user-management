using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using UserManagementApp.Application.Features.Users.DTOs;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Enums;
using UserManagementApp.Domain.Exceptions;
using UserManagementApp.Domain.Repositories;

namespace UserManagementApp.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsAsync(u => u.Email.ToLower().Equals(request.Email.ToLower())))
            {
                throw new ConflictException("Email already exists");
            }

            var userEntity = new User(request.FullName, request.Email, Enum.Parse<Role>(request.Role, true));

            var newUser = await _userRepository.AddAsync(userEntity);

            return _mapper.Map<UserDto>(newUser);
        }
    }
}