using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UCVersioning.Models.v2.Requests;

namespace UCVersioning.Controllers.v2;

/// <summary>
/// Opportunity Controller
/// </summary>
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[ApiController]
public class OpportunitiesController : ControllerBase
{
    /// <summary>
    /// Constructor
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

    /// <summary>
    /// Get Opportunity by produt
    /// </summary>
    /// <param name="productName"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetByProductName")]
    [SwaggerOperation(OperationId = "GetOpportunityByProductName")]
    [SwaggerResponse(200, "Opportunity with id returned", typeof(Guid))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult GetByProductName([FromQuery] string productName)
    {
        return Ok(new Guid());
    }
}