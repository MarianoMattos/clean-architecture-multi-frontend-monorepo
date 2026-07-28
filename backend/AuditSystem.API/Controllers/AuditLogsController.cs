using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuditSystem.Application.Features.AuditLogs.Commands.CreateAuditLog;
using AuditSystem.Application.Features.AuditLogs.Queries.GetAuditLogsList;

namespace AuditSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuditLogsController(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateLog([FromBody] CreateAuditLogCommand command, CancellationToken cancellationToken)
    {
        Guid logId = await _mediator.Send(command, cancellationToken);
        
        return CreatedAtAction(
            nameof(GetLogsBySource), 
            new { systemSource = command.SystemSource }, 
            new { id = logId }
        );
    }

    [HttpGet("source/{systemSource}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLogsBySource(
        [FromRoute] string systemSource,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAuditLogsListQuery(systemSource, page, pageSize);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}