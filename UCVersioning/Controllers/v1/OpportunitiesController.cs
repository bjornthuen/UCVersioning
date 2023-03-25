using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UCVersioning.Models.v1.Requests;

namespace UCVersioning.Controllers.v1;

/// <summary>
/// Opportunity Controller
/// </summary>
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class OpportunitiesController : ControllerBase
{

    /// <summary>
    /// Controller
    /// </summary>
    public OpportunitiesController()
    { }

    /// <summary>
    /// Create opportunity request
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateOpportunity")]
    [SwaggerResponse(201, "Returned id", typeof(Guid))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult Create(CreateOpportunityRequest request)
    {
        return Ok(new Guid());
    }
}