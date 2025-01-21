namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Response model for DeleteSale operation.
/// </summary>
public class DeleteSaleResponse
{
    /// <summary>
    /// Indicates whether the deletion was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// The message regarding the result of the deletion.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
