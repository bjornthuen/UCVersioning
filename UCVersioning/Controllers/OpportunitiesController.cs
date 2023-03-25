using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UCVersioning.Models.Requests;

namespace UCVersioning.Controllers;

/// <summary>
/// [DEPRECATED]
/// Opportunity Controller
/// </summary>
[Route("[controller]")]
[ApiVersion("0.1", Deprecated = true)]
[ApiController]
public class OpportunitiesController : ControllerBase
{

    /// <summary>
    /// Controller
    /// </summary>
    public OpportunitiesController()
    { }


    /// <summary>
    /// Get Opportunities
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("v{version:apiVersion}/[controller]")]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(OperationId = "GetOpportunities")]
    [SwaggerResponse(200, "Opportunities records returned", typeof(List<Guid>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult GetV1() => Get();

    /// <summary>
    /// Get Opportunities
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [SwaggerOperation(OperationId = "GetOpportunities")]
    [SwaggerResponse(200, "Opportunities records returned", typeof(List<Guid>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult Get()
    {
        return Ok(new List<Guid> { Guid.NewGuid(), Guid.NewGuid() });
    }


    /// <summary>
    /// Get opportunity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("v{version:apiVersion}/[controller]/{id:guid}")]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(OperationId = "GetOpportunity")]
    [SwaggerResponse(200, "Opportunity with id returned", typeof(Guid))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult GetV1(Guid id) => Get(id);

    /// <summary>
    /// Get opportunity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:guid}")]
    [SwaggerOperation(OperationId = "GetOpportunity")]
    [SwaggerResponse(200, "Opportunity with id returned", typeof(Guid))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult Get(Guid id)
    {
        return Ok(id);
    }

    /// <summary>
    /// [Deprecated]: Api have been deprecated. Available in newer version
    /// Create opportunity request
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Obsolete]
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