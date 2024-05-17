using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Application.Utils
{
    public class BaseController: ControllerBase
    {
        public BaseController()
        {
            // _logger = LogManager.GetLogger(typeof(BaseController));
        }
        protected IActionResult ReturnResponse(dynamic model)
        {
            if (model.Status == RequestExecution.Successful)
            {
                return Ok(model);
            }

            return BadRequest(model);
        }

        protected Guid UserId
        {
            get { return Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value); }
        }

        protected DateTime CurrentDateTime
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        protected IActionResult HandleError(Exception ex, string customErrorMessage = null)
        {
            // _logger.Error(ex.Message, ex);
            // _logger.Error(ex.StackTrace, ex);


            BaseResponse<string> rsp = new BaseResponse<string>();
            rsp.Status = RequestExecution.Error;
#if DEBUG
            rsp.Errors = new List<string>() { $"Error: {(ex?.InnerException?.Message ?? ex.Message)} --> {ex?.StackTrace}" };
            return Ok(rsp);
#else
            rsp.Errors = new List<string>() { "An error occurred while processing your request!" };
            return BadRequest(rsp);
#endif
        }
    }
}
