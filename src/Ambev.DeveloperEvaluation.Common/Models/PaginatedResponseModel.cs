namespace Ambev.DeveloperEvaluation.Common.Models;

public class PaginatedResponseModel<T> : ApiResponseWithDataModel<IEnumerable<T>>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}