using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Application.Features.Users.Models;
using UserManagementApp.Application.Features.Users.Queries.GetUserById;
using UserManagementApp.Application.Features.Users.Queries.GetUsers;
using UserManagementApp.Application.ViewModels;

namespace UserManagementApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}