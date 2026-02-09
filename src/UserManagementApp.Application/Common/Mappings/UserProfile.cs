using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserManagementApp.Application.Features.Users.Commands.CreateUser;
using UserManagementApp.Application.Features.Users.DTOs;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        }
    }
}