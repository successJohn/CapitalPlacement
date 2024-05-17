using CapitalPlacementTask.Application.DTO.CandidateApplication;
using CapitalPlacementTask.Application.DTO.ProgramForm;
using CapitalPlacementTask.Application.Interfaces;
using CapitalPlacementTask.Application.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CapitalPlacementTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateApplicationController : BaseController
    {
        private readonly IApplicationService _applicationService;
        public CandidateApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        [Route("{programId}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Create([FromRoute]Guid programId, CreateCandidateApplicationDto programDto)
        {

            return ReturnResponse(await _applicationService.ApplyAsync(programId,programDto));

        }
    }
}
