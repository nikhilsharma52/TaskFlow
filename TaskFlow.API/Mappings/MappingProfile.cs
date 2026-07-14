using AutoMapper;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;

namespace TaskFlow.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItem, TaskResponseDto>();

            CreateMap<CreateTaskDto, TaskItem>();

            CreateMap<UpdateTaskDto, TaskItem>();

            CreateMap<Project, ProjectResponseDto>();

            CreateMap<CreateProjectDto, Project>();

            CreateMap<UpdateProjectDto, Project>();
        }
    }
}
