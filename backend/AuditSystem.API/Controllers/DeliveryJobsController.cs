using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuditSystem.Application.Features.DeliveryJobs.Commands.CreateDeliveryJob;
using AuditSystem.Application.Features.DeliveryJobs.Commands.CompleteDeliveryJob;
using AuditSystem.Application.Features.DeliveryJobs.Queries.GetPendingDeliveryJobs;

namespace AuditSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DeliveryJobsController(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateJob([FromBody] CreateDeliveryJobCommand command, CancellationToken cancellationToken)
    {
        Guid jobId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetPendingJobs), 
            new { id = jobId }, 
            new { id = jobId }
        );
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompleteJob([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new CompleteDeliveryJobCommand(id);
        var success = await _mediator.Send(command, cancellationToken);
        
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPendingJobs(
        [FromQuery] int maxRetryCount = 3,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPendingDeliveryJobsQuery(maxRetryCount);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}