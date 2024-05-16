using CapitalPlacementTask.Application.DTO.ProgramForm;
using CapitalPlacementTask.Application.Interfaces;
using CapitalPlacementTask.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramFormController : ControllerBase
    {
        private readonly IProgramFormService _programFormService;
        public ProgramFormController(IProgramFormService programFormService)
        {
            _programFormService = programFormService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProgramFormDto programDto)
        {
            var result = await _programFormService.CreateAsync(programDto);

            return Ok(result);

        }

        [HttpPut("{programId}")]
        public async Task<IActionResult> Update(Guid programId, CreateProgramFormDto programDto)
        {
            var result = await _programFormService.UpdateAsync(programId, programDto);

            return Ok(result);

        }
    }
}
