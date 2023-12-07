using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organization.Business.Employeee.Query;

namespace Organization.Api.Controllers.Query
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "EmployeeQuery")]
    public class EmployeeQueryController : ControllerBase
    {
        private readonly IEmployeeQueryManger _employeeQueryManger;
        private readonly ILogger<EmployeeQueryController> _logger;

        public EmployeeQueryController(IEmployeeQueryManger employeeQueryManger ,ILogger<EmployeeQueryController> logger)
        {
            _employeeQueryManger=employeeQueryManger;
            _logger=logger;
        }

        [HttpGet("{employeeId:guid}")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByEmployeeId(Guid employeeId, CancellationToken cancellationToken = default)
        {
            try
            {
                var employee = await _employeeQueryManger.GetEmployeeByIdAsync(employeeId, cancellationToken);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken = default)
        {
            try
            {
                var employee = await _employeeQueryManger.GetAllEmployeeAsync(cancellationToken);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return BadRequest();
        }
    }
}
