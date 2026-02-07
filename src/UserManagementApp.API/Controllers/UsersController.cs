using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Application.Features.Users.Commands.CreateUser;
using UserManagementApp.Application.Features.Users.Commands.ToggleUserStatus;
using UserManagementApp.Application.Features.Users.Commands.UpdateUser;
using UserManagementApp.Application.Features.Users.Models;
using UserManagementApp.Application.Features.Users.Queries.GetUserById;
using UserManagementApp.Application.Features.Users.Queries.GetUsers;
using UserManagementApp.Application.ViewModels;

namespace UserManagementApp.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _mediator.Send(
                new GetUserByIdQuery(id)
            );

            return Ok(new GeneralResponse
            {
                Data = user
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilter userFilter)
        {
            var users = await _mediator.Send(
                new GetUsersQuery(userFilter)
            );

            return Ok(new GeneralResponse
            {
                Data = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request)
        {
            var user = await _mediator.Send(request);

            return Ok(new GeneralResponse
            {
                Data = user
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand request)
        {
            if (id != request.Id)
                return BadRequest();

            var user = await _mediator.Send(request);

            return Ok(new GeneralResponse
            {
                Data = user
            });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleUserStatus(Guid id, [FromBody] ToggleUserStatusCommand request)
        {
            if (id != request.Id)
                return BadRequest();

            var IsActive = await _mediator.Send(request);

            return Ok(new GeneralResponse
            {
                Data = new
                {
                    IsActive
                }
            });
        }
    }
}