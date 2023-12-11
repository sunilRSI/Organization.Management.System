using Microsoft.AspNetCore.Mvc;
using Organization.Business.Employeee.Command;
using Organization.Business.Employeee.Models;
using Organization.Business.SQS.Command;
using Organization.Entity.Constants; 

namespace Organization.Api.Controllers.Command
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "EmployeeCommand")]
    public class EmployeeCommandController : ControllerBase
    {
        private readonly IEmployeeCommandManger _employeeCommandManger;
        private readonly ILogger<EmployeeCommandManger> _logger; 
        public EmployeeCommandController(IEmployeeCommandManger employeeCommandManger, ILogger<EmployeeCommandManger> logger)
        {
            _employeeCommandManger = employeeCommandManger;
            _logger = logger; 
        }
        [HttpPost]
        [Route("AddEmployee")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidDataException();
                }
                var employee = await _employeeCommandManger.CreateEmployeeAsync(employeeCreateModel, cancellationToken);
                return Created("", employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmployeeAsync([FromBody] EmployeeReadModel employeeUpdateModel, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidDataException();
                }

                await _employeeCommandManger.UpdateEmployeeAsync(employeeUpdateModel, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return BadRequest();
        }

        [HttpDelete("{employeeId:guid}")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _employeeCommandManger.DeleteEmployeeAsync(employeeId, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return BadRequest();
        }
    }
}
