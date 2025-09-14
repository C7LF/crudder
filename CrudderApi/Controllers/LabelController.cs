using AutoMapper;
using CrudderApi.DTOs.Labels;
using CrudderApi.Extensions;
using CrudderApi.Models;
using CrudderApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudderApi.Controllers
{
    [Route("api/labels")]
    [ApiController]
    [Authorize]
    public class LabelController(LabelService labelService, IMapper mapper) : ControllerBase
    {
        private readonly LabelService _labelService = labelService;
        private readonly IMapper _mapper = mapper;

        protected int UserId => User.GetUserId();


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabelResponse>>> GetAll()
        {
            var labels = await _labelService.GetAllByUserAsync(UserId);
            var response = _mapper.Map<List<LabelResponse>>(labels);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<LabelResponse>> Create(CreateLabelRequest request)
        {
            var label = _mapper.Map<Label>(request);
            label.UserId = UserId;

            try
            {
                var created = await _labelService.CreateAsync(label);
                var response = _mapper.Map<LabelResponse>(created);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}