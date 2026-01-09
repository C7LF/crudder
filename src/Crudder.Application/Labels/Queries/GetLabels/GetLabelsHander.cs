using Crudder.Application.Labels.Dtos;
using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Labels.Queries.GetLabels;

public class GetLabelsHandler(ILabelRepository labelRepository)
    : IRequestHandler<GetLabelsQuery, List<LabelDto>>
{
    private readonly ILabelRepository _labelRepository = labelRepository;

    public async Task<List<LabelDto>> Handle(
        GetLabelsQuery request,
        CancellationToken cancellationToken)
    {
        var labels = await _labelRepository.GetAllByUserAsync(request.UserId);

        return [.. labels.Select(l => new LabelDto(l.Id, l.Text, l.Colour))];
    }
}
