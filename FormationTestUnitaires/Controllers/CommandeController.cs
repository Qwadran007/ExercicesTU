using Microsoft.AspNetCore.Mvc;

namespace FormationTestUnitaires.Controllers;

[ApiController]
[Route("[controller]")]
public class CommandeController : ControllerBase
{

    private readonly ILogger<CommandeController> _logger;

    public CommandeController(ILogger<CommandeController> logger)
    {
        _logger = logger;
    }

}
