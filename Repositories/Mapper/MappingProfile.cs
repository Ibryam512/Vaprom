using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DTO;
using ViewModels.Input;

namespace Repositories.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Role, EditRoleViewModel>().ReverseMap();
			CreateMap<Project, ProjectDTO>().ReverseMap();
			CreateMap<Role, RoleDTO>().ReverseMap();
			CreateMap<Team, TeamDTO>().ReverseMap();
			CreateMap<User, UserDTO>().ReverseMap();
			CreateMap<Vacation, VacationDTO>().ReverseMap();
		}	
	}
}
