namespace Deal.DeveloperEvaluation.WebApi.Dtos;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }
}
