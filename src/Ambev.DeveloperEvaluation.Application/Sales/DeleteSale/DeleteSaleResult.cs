namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Result model for DeleteSale operation.
/// </summary>
public class DeleteSaleResult
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
