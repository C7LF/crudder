using AutoMapper;
using CrudderApi.Models;
using CrudderApi.DTOs.Todo;

namespace CrudderApi.Mappings
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            // Request → Entity
            CreateMap<CreateTodoRequest, TodoItem>();
            CreateMap<UpdateTodoRequest, TodoItem>();

            // Entity → Response
            CreateMap<TodoItem, TodoResponse>();
        }
    }
}