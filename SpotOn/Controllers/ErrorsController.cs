using Microsoft.AspNetCore.Mvc;

namespace SpotOn.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}