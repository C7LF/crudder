using AutoMapper;
using CrudderApi.Models;
using CrudderApi.DTOs.Labels;
using CrudderApi.DTOs.Todo;

namespace CrudderApi.Mappings
{
    public class LabelProfile : Profile
    {
        public LabelProfile()
        {
            // Request → Entity
            CreateMap<CreateLabelRequest, Label>();

            // Entity → Response
            CreateMap<Label, LabelResponse>();

            // Todo mappings
            CreateMap<TodoItem, TodoResponse>()
                .ForMember(dest => dest.Labels,
                    opt => opt.MapFrom(src => src.Labels));
        }
    }
}