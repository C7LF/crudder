using Xunit;
using CrudderApi.Data;
using CrudderApi.Models;
using CrudderApi.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;

namespace CrudderApi.Tests.Services
{
    public class TodoServiceTests
    {
        private TodoContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new TodoContext(options);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddTodo()
        {
            // Arrange
            var context = GetDbContext();
            var mapper = new Mock<IMapper>().Object;
            var logger = NullLogger<TodoService>.Instance;
            var service = new TodoService(context, mapper, logger);

            // Act
            var todo = new TodoItem { Title = "Test todo", UserId = 1 };
            var created = await service.CreateAsync(todo);

            // Assert
            Assert.NotNull(created);
            Assert.Equal("Test todo", created.Title);
            Assert.Single(context.TodoItems);
        }

        [Fact]
        public async Task GetAllByUserAsync_ShouldReturnOnlyUserTodos()
        {
            // Given
            var context = GetDbContext();
            var mapper = new Mock<IMapper>().Object;
            var logger = NullLogger<TodoService>.Instance;
            var service = new TodoService(context, mapper, logger);

            // When
            context.AddRange(
                new TodoItem { Title = "User1 Todo", UserId = 1 },
                new TodoItem { Title = "User2 Todo", UserId = 2 }
            );

            await context.SaveChangesAsync();
            var todos = await service.GetAllByUserAsync(1);

            // Then
            Assert.Single(todos);
            Assert.Equal("User1 Todo", todos[0].Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTodo_WhenExists()
        {
            var context = GetDbContext();
            var mapperMock = new Mock<IMapper>();
            var logger = NullLogger<TodoService>.Instance;
            var service = new TodoService(context, mapperMock.Object, logger);

            var todo = new TodoItem { Id = 1, Title = "do the dishes", UserId = 1 };
            context.TodoItems.Add(todo);
            await context.SaveChangesAsync();

            var updateRequest = new DTOs.Todo.UpdateTodoRequest { Title = "updated title" };

            mapperMock.Setup(m => m.Map(updateRequest, todo)).Callback(() => todo.Title = updateRequest.Title);

            var result = await service.UpdateAsync(1, 1, updateRequest);

            Assert.True(result);
            Assert.Equal("updated title", todo.Title);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveTodo_WhenExists()
        {
            var context = GetDbContext();
            var mapperMock = new Mock<IMapper>();
            var logger = NullLogger<TodoService>.Instance;
            var service = new TodoService(context, mapperMock.Object, logger);

            var todo = new TodoItem { Id = 1, Title = "do the dishes", UserId = 1 };
            context.TodoItems.Add(todo);
            await context.SaveChangesAsync();

            var result = await service.DeleteAsync(1, 1);
            Assert.True(result);
            Assert.Empty(context.TodoItems);
        }
    }
}
