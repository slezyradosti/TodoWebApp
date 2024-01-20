using Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace TodoWebApp.Controllers;

[Microsoft.AspNetCore.Components.Route("[controller]")]
public class BaseController : Controller
{
   [NonAction]
   protected ActionResult HandleResult<T>(Result<T> result)
   {
   if (result == null)
   {
       return NotFound();
   }
   if (!result.IsSuccess)
   {
       return BadRequest(result.Error);
   }
   if (result.IsSuccess && result.Value == null)
   {
       return NotFound();
   }
   if (result.IsSuccess && result.Value != null)
   {
        return Ok(result.Value);
   }
            
   return BadRequest(); 
   } 
}
    