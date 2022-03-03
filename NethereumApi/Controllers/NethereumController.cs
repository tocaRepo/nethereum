namespace NethereumApi.Controllers;

using System.Numerics;
using Microsoft.AspNetCore.Mvc;

using NethereumApi.Models;
using NethereumApi.Services;

[ApiController]
[Route("api")]
public class NethereumController : ControllerBase
{

    private readonly ILogger<NethereumController> _logger;
    private readonly INethereumService _nethereumService;
    public NethereumController(ILogger<NethereumController> logger, INethereumService nethereumService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _nethereumService = nethereumService ?? throw new ArgumentNullException(nameof(nethereumService));

    }

    [HttpGet("GetEthBalance/{wallet}")]
    public async Task<IActionResult> GetEthBalanceByWalletId(string wallet)
    {
        if (!string.IsNullOrEmpty(wallet))
        {
            try
            {
                decimal balance = await _nethereumService.GetEthBalanceByWalletIdAsync(wallet);
                GetBalanceResponse response = new GetBalanceResponse()
                {
                    Balance = balance
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while executing GetBalanceByWalletId");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        return BadRequest();
    }

   
}
