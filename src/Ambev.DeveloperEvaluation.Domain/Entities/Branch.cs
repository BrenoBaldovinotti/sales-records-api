using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a branch where sales are conducted.
/// </summary>
public class Branch : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the branch.
    /// Must not be null or empty.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of sales associated with this branch.
    /// This is a navigation property.
    /// </summary>
    public ICollection<Sale> Sales { get; set; } = [];
}
