using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Core.Dtos.Requests;
using OnionArchitecture.Core.Dtos.Responses;
using OnionArchitecture.Core.Mappers.Assignments;
using OnionArchitecture.Core.Services.Contracts;
using System.Threading.Tasks;

namespace OnionArchitecture.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly ICreateAssignmentService _createAssignmentService;
        private readonly IUpdateAssignmentService _updateAssignmentService;

        public AssignmentsController(ICreateAssignmentService createAssignmentService, IUpdateAssignmentService updateAssignmentService)
        {
            _createAssignmentService = createAssignmentService;
            _updateAssignmentService = updateAssignmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAssignmentRequestDto createAssignmentRequestDto)
        {
            CreateAssignmentResponseDto response = await _createAssignmentService.Create(createAssignmentRequestDto);
            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeComplete(int id)
        {
            UpdateAssignmentResponseDto response = await _updateAssignmentService.ChangeComplete(id);
            return Ok(response);
        }

    }
}
