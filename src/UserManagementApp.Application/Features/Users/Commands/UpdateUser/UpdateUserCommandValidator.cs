using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using UserManagementApp.Domain.Constants;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.FullName)
                .MaximumLength(DataSchemaLength.LARGE).WithMessage($"Name must not exceed {DataSchemaLength.LARGE} characters");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Role).Must(r => r == Role.User.ToString() || r == Role.Admin.ToString())
                .WithMessage("Role must be 'Admin' or 'User'");
        }
    }
}