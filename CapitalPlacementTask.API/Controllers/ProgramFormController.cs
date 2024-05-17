using CapitalPlacementTask.Application.DTO.CandidateApplication;
using CapitalPlacementTask.Application.DTO.ProgramForm;
using CapitalPlacementTask.Application.Interfaces;
using CapitalPlacementTask.Application.Utils;
using CapitalPlacementTask.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramFormController : BaseController
    {
        private readonly IProgramFormService _programFormService;
        public ProgramFormController(IProgramFormService programFormService)
        {
            _programFormService = programFormService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProgramFormDTO programDto)
        {
            return ReturnResponse(await _programFormService.CreateAsync(programDto));
        }

        [HttpPut("{programId}")]
        public async Task<IActionResult> Update(Guid programId, CreateProgramFormDTO programDto)
        {
            return ReturnResponse(await _programFormService.UpdateAsync(programId, programDto));

        }
    }
}
