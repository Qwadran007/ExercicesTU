using Microsoft.AspNetCore.Mvc;

namespace FormationTestUnitaires.Controllers;

[ApiController]
[Route("[controller]")]
public class ProduitController : ControllerBase
{
    private readonly ILogger<ProduitController> _logger;

    public ProduitController(ILogger<ProduitController> logger)
    {
        _logger = logger;
    }

}
