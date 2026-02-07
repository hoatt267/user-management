using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using UserManagementApp.Domain.Constants;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is require")
                .MaximumLength(DataSchemaLength.LARGE).WithMessage($"Name must not exceed {DataSchemaLength.LARGE} characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Role).Must(r => r == Role.User.ToString() || r == Role.Admin.ToString())
            .WithMessage("Role must be 'Admin' or 'User'");
        }
    }
}