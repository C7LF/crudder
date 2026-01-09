using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Labels.Commands.DeleteLabel;

public class DeleteLabelHandler(ILabelRepository labelRepository)
    : IRequestHandler<DeleteLabelCommand, bool>
{
    private readonly ILabelRepository _labelRepository = labelRepository;

    public async Task<bool> Handle(
        DeleteLabelCommand request,
        CancellationToken cancellationToken)
    {
        var label = await _labelRepository.GetByIdAsync(
            request.LabelId,
            request.UserId);

        if (label == null)
            return false;

        await _labelRepository.DeleteAsync(label);
        return true;
    }
}
