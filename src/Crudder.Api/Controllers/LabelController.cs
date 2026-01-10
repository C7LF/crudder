using Crudder.Application.Labels.Commands.CreateLabel;
using Crudder.Application.Labels.Commands.DeleteLabel;
using Crudder.Application.Labels.Queries.GetLabels;
using Crudder.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crudder.Api.Dtos.Labels;

namespace Crudder.Api.Controllers;

[ApiController]
[Route("api/labels")]
[Authorize]
public class LabelController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private int UserId => User.GetUserId();

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var labels = await _mediator.Send(
            new GetLabelsQuery(UserId));

        return Ok(labels);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLabelRequest request)
    {
        var label = await _mediator.Send(
            new CreateLabelCommand(UserId, request.Text, request.Colour));

        return Ok(label);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _mediator.Send(
            new DeleteLabelCommand(UserId, id));

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
