using System.Text.Json.Serialization;


namespace ShopAIDesktop.Src.Domain.Common;


public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    public ApiResponse() { }

    public ApiResponse(int code, string status, string message, T? data)
    {
        this.Code = code;
        this.Status = status;
        this.Message = message;
        this.Data = data;
    }


    public static ApiResponse<T> Success(T data, string message)
    {
        return new ApiResponse<T>(200, "OK", message, data);
    }

    public static ApiResponse<T> Created(T data, string message)
    {
        return new ApiResponse<T>(201, "CREATED", message, data);
    }

    public static ApiResponse<T> Error(int code, string status, string message)
    {
        return new ApiResponse<T>(code, status, message, default);
    }
}
