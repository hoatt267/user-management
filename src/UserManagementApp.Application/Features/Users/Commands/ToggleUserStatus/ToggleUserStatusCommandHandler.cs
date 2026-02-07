using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Exceptions;
using UserManagementApp.Domain.Repositories;

namespace UserManagementApp.Application.Features.Users.Commands.ToggleUserStatus
{
    public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleUserStatusCommand, bool>
    {
        private readonly IRepository<User> _userRepository;

        public ToggleUserStatusCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(
                selector: u => u,
                predicate: u => u.Id == request.Id
            );

            if (user == null)
                throw new EntityNotFoundException(nameof(User), request.Id);

            if (request.IsActive)
                user.Activate();
            else
                user.Deactivate();

            await _userRepository.SaveChangesAsync();

            return request.IsActive;
        }
    }
}