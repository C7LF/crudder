using MediatR;

namespace Crudder.Application.Labels.Commands.DeleteLabel;

public record DeleteLabelCommand(int UserId, int LabelId)
    : IRequest<bool>;
