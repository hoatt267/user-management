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

namespace UserManagementApp.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(
                selector: u => u,
                predicate: u => u.Id == request.Id
            );

            if (user == null)
                throw new EntityNotFoundException(nameof(User), request.Id);

            user.UpdateUser(request.FullName ?? user.FullName, request.Email ?? user.Email, Enum.Parse<Role>(request.Role ?? user.Role.ToString()));

            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}