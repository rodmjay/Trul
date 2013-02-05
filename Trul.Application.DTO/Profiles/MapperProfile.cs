using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Trul.Domain.Entities;
using System.Linq.Expressions;

namespace Trul.Application.DTO.Profiles
{
    public class MapperProfile
    {
        public static void Configure()
        {
            Mapper.CreateMap<Menu, MenuDTO>();
            Mapper.CreateMap<MenuDTO, Menu>();
            Mapper.CreateMap<Country, CountryDTO>();
            Mapper.CreateMap<CountryDTO, Country>();
            Mapper.CreateMap<Person, PersonDTO>();
            Mapper.CreateMap<PersonDTO, Person>();
            Mapper.CreateMap<UserDTO, User>();
            Mapper.CreateMap<List<UserDTO>, List<User>>();
            Mapper.CreateMap<List<User>, List<UserDTO>>();
            Mapper.CreateMap<User, UserDTO>();
            Mapper.CreateMap<RoleDTO, Role>();
            Mapper.CreateMap<Role, RoleDTO>();
            Mapper.CreateMap<List<RoleDTO>, List<Role>>();
            Mapper.CreateMap<List<Role>, List<RoleDTO>>();
        }
    }
}
