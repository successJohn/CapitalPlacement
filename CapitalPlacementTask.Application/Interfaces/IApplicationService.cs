using CapitalPlacementTask.Application.DTO.CandidateApplication;
using CapitalPlacementTask.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Application.Interfaces
{
    public interface IApplicationService
    {
        Task<BaseResponse<string>> ApplyAsync(Guid programId, CreateCandidateApplicationDto candidateApplicationDto);
    }
}
