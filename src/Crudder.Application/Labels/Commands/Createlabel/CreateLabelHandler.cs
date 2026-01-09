using Crudder.Application.Labels.Dtos;
using Crudder.Domain.Entities;
using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Labels.Commands.CreateLabel;

public class CreateLabelHandler(ILabelRepository labelRepository)
    : IRequestHandler<CreateLabelCommand, LabelDto>
{
    private readonly ILabelRepository _labelRepository = labelRepository;

    private const int MaxLabels = 10;

    public async Task<LabelDto> Handle(
        CreateLabelCommand request,
        CancellationToken cancellationToken)
    {
        var count = await _labelRepository.CountByUserAsync(request.UserId);
        if (count >= MaxLabels)
            throw new InvalidOperationException($"Cannot have more than {MaxLabels} labels.");

        var label = new Label
        {
            Text = request.Name,
            UserId = request.UserId,
            Colour = request.Colour
        };

        await _labelRepository.AddAsync(label);

        return new LabelDto(label.Id, label.Text, label.Colour);
    }
}
