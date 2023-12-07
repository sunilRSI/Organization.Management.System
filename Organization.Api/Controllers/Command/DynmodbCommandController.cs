 
using Microsoft.AspNetCore.Mvc;
using Organization.Business.DbContext.Command; 

namespace Organization.Api.Controllers.Command
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "DynmodbCommand")]
    public class DynmodbCommandController : ControllerBase
    {
        private readonly ILogger<DynmodbCommandController> _logger;
        private readonly IDbContextCommandManager _dbContextCommandManager;

        public DynmodbCommandController(ILogger<DynmodbCommandController> logger, IDbContextCommandManager dbContextCommandManager)
        {
            _logger = logger;
            _dbContextCommandManager = dbContextCommandManager;
        }
        public async Task<IActionResult> Initilize()
        {
            await _dbContextCommandManager.Initialize();
            return Ok();
        }
    }
}
