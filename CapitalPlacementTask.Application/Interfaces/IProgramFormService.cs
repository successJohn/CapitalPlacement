using CapitalPlacementTask.Application.DTO.ProgramForm;
using CapitalPlacementTask.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Application.Interfaces
{
    public interface IProgramFormService
    {
        Task<BaseResponse<string>> CreateAsync(CreateProgramFormDto programDto);
        Task<BaseResponse<bool>> UpdateAsync(Guid id, CreateProgramFormDto programDto);
    }
}
