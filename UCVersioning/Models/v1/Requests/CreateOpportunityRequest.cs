namespace UCVersioning.Models.v1.Requests;

/// <summary>
/// Product Request
/// </summary>
public class CreateOpportunityRequest
{
    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Expected Earning on the opportunity
    /// </summary>
    public int ExpcetedAmount { get; set; }
}
