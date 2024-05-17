using CapitalPlacementTask.Application.DTO.CandidateApplication;
using CapitalPlacementTask.Application.DTO.ProgramForm;
using CapitalPlacementTask.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public CandidateApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        [HttpPost]
        [Route("{programId}")]
        public async Task<IActionResult> Create([FromRoute]Guid programId, CreateCandidateApplicationDto programDto)
        {
            var result = await _applicationService.ApplyAsync(programId, programDto);

            return Ok(result);

        }
    }
}
