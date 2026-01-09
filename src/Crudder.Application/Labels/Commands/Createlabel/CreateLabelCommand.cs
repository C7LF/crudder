using Crudder.Application.Labels.Dtos;
using MediatR;

namespace Crudder.Application.Labels.Commands.CreateLabel;

public record CreateLabelCommand(int UserId, string Name, string Colour)
    : IRequest<LabelDto>;
