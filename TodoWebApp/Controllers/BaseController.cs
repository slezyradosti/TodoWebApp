using System.Text.Json;
using Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace TodoWebApp.Controllers;

public class BaseController : Controller
{
   [NonAction]
   protected IActionResult HandleResult<T>(Result<T> result)
   {
   if (result == null)
   {
       return Json(NotFound());
   }
   if (!result.IsSuccess)
   {
       return Json( BadRequest(result.Error));
   }
   if (result.IsSuccess && result.Value == null)
   {
       return Json(NotFound());
   }
   if (result.IsSuccess && result.Value != null)
   {
        return Json(Ok(result.Value));
   }
            
   return Json(BadRequest()); 
   } 
}
    