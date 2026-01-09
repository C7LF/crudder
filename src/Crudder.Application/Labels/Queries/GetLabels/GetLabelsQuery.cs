using Crudder.Application.Labels.Dtos;
using MediatR;

namespace Crudder.Application.Labels.Queries.GetLabels;

public record GetLabelsQuery(int UserId)
    : IRequest<List<LabelDto>>;
