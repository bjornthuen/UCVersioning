using System.ComponentModel.DataAnnotations;

namespace UCVersioning.Models.v2.Requests;

/// <summary>
/// Product Request
/// </summary>
public class CreateOpportunityRequest
{
    /// <summary>
    /// Name
    /// </summary>
    [Required]
    public string? Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Expected Earning on the opportunity
    /// </summary>
    public decimal ExpcetedAmount { get; set; }
}
