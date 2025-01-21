namespace Ambev.DeveloperEvaluation.Common.Models;

public class ApiResponseWithDataModel<T> : ApiResponseModel
{
    public T? Data { get; set; }
}
