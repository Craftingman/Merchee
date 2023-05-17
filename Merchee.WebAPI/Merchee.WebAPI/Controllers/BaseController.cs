using FluentResults;
using Merchee.BLL.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Merchee.WebAPI.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult HandleResult<TResult>(Result<TResult> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return HandleFailedResult(result.ToResult());
        }

        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
            {
                return Ok();
            }

            return HandleFailedResult(result);
        }

        protected IActionResult HandleFailedResult(Result result)
        {
            var error = result.Errors.FirstOrDefault();
            if (error is null || string.IsNullOrEmpty(error.Message))
                return Problem("Unexpected internal server error");
            if (error is NotFoundError)
                return NotFound();
            if (error is BadRequestError)
                return BadRequest();

            return Problem(error.Message);
        }
    }
}
