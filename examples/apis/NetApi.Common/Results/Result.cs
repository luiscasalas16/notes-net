using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;

namespace NetApi.Common.Results
{
    public class Result : IResult, IEndpointMetadataProvider, IStatusCodeHttpResult
    {
        public const int SuccessCode = 200;
        public const int ClientErrorCode = 400;
        public const int ServerErrorCode = 500;
        public const string ClientErrorTitle = "Bad Request Error";
        public const string ServerErrorTitle = "Internal Server Error";

        public int Code { get; }

        public bool IsSuccess => Code == SuccessCode;
        public bool IsClientError => Code == ClientErrorCode;
        public bool IsServerError => Code == ServerErrorCode;

        public List<ResultError> Errors { get; }

        protected internal Result(int code)
            : this(code, []) { }

        protected internal Result(int code, List<ResultError> errors)
        {
            Code = code;
            Errors = errors;
        }

        public static Result Success() => new(SuccessCode);

        public static Result<TValue> Success<TValue>(TValue data) => new(SuccessCode, data);

        public static Task<Result> SuccessAsync() => Task.FromResult(Success());

        public static Task<Result<TValue>> SuccessAsync<TValue>(TValue data) => Task.FromResult(Success(data));

        public static Result Failure(ResultError error) => new(ClientErrorCode, [error]);

        public static Result<TValue> Failure<TValue>(ResultError error) => new(ClientErrorCode, [error]);

        public static Result Failure(List<ResultError> errors) => new(ClientErrorCode, errors);

        public static Result<TValue> Failure<TValue>(List<ResultError> errors) => new(ClientErrorCode, errors);

        public static Task<Result> FailureAsync(ResultError error) => Task.FromResult(Failure(error));

        public static Task<Result<TValue>> FailureAsync<TValue>(ResultError error) => Task.FromResult(Failure<TValue>(error));

        public static Task<Result> FailureAsync(List<ResultError> error) => Task.FromResult(Failure(error));

        public static Task<Result<TValue>> FailureAsync<TValue>(List<ResultError> error) => Task.FromResult(Failure<TValue>(error));

        #region IResult

        int? IStatusCodeHttpResult.StatusCode => Code;

        public Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            httpContext.Response.StatusCode = Code;

            switch (Code)
            {
                case ClientErrorCode:
                    ResultClientError resultClientError = new(Errors);

                    return httpContext.Response.WriteAsJsonAsync(resultClientError);
                default:
                    return Task.CompletedTask;
            }
        }

        static void IEndpointMetadataProvider.PopulateMetadata(MethodInfo method, EndpointBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(method);
            ArgumentNullException.ThrowIfNull(builder);

            builder.Metadata.Add(new ProducesResponseTypeMetadata(StatusCodes.Status200OK, typeof(void)));
        }

        public static Result<TDestination> Convert<TSource, TDestination>(Result<TSource> result)
        {
            return new Result<TDestination>(result.Code, result.Errors);
        }

        #endregion
    }

    public class Result<TValue> : Result, IResult, IEndpointMetadataProvider, IStatusCodeHttpResult, IValueHttpResult, IValueHttpResult<TValue>
    {
        public TValue? Value { get; }

        protected internal Result(int code)
            : this(code, []) { }

        protected internal Result(int code, List<ResultError> errors)
            : this(code, default, errors) { }

        protected internal Result(int code, TValue? value)
            : this(code, value, []) { }

        protected internal Result(int code, TValue? value, List<ResultError> errors)
            : base(code, errors)
        {
            Value = value;
        }

        #region IResult

        object? IValueHttpResult.Value => Value;

        int? IStatusCodeHttpResult.StatusCode => Code;

        public new Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            httpContext.Response.StatusCode = Code;

            switch (Code)
            {
                case SuccessCode:
                    if (Value is null)
                        return Task.CompletedTask;

                    return httpContext.Response.WriteAsJsonAsync<object>(Value);
                case ClientErrorCode:
                    ResultClientError resultClientError = new(Errors);

                    return httpContext.Response.WriteAsJsonAsync(resultClientError);
                default:
                    return Task.CompletedTask;
            }
        }

        static void IEndpointMetadataProvider.PopulateMetadata(MethodInfo method, EndpointBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(method);
            ArgumentNullException.ThrowIfNull(builder);

            builder.Metadata.Add(new ProducesResponseTypeMetadata(StatusCodes.Status200OK, typeof(TValue), ["application/json"]));
        }

        #endregion
    }
}
