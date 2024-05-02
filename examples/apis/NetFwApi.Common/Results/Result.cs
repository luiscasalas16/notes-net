using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace NetFwApi.Common.Results
{
    public class Result : IHttpActionResult
    {
        protected readonly HttpRequestMessage Request;

        public const int SuccessCode = 200;
        public const int ClientErrorCode = 400;
        public const int ServerErrorCode = 500;
        public const string ClientErrorTitle = "Bad Request Error";
        public const string ServerErrorTitle = "Internal Server Error";

        public int Code { get; }

        public bool IsSuccess => Code == SuccessCode;
        public bool IsClientError => Code == ClientErrorCode;
        public bool IsServerError => Code == ServerErrorCode;

        public List<Error> Errors { get; }

        protected internal Result(int code, HttpRequestMessage request)
            : this(code, new List<Error>(), request) { }

        protected internal Result(int code, List<Error> errors, HttpRequestMessage request)
        {
            Code = code;
            Errors = errors;
            Request = request;
        }

        public static Result Success(HttpRequestMessage request) => new Result(SuccessCode, request);

        public static Result<TValue> Success<TValue>(TValue data, HttpRequestMessage request) => new Result<TValue>(SuccessCode, data, request);

        public static Task<Result> SuccessAsync(HttpRequestMessage request) => Task.FromResult(Success(request));

        public static Task<Result<TValue>> SuccessAsync<TValue>(TValue data, HttpRequestMessage request) => Task.FromResult(Success(data, request));

        public static Result Failure(Error error, HttpRequestMessage request) => new Result(ClientErrorCode, new List<Error>() { error }, request);

        public static Result<TValue> Failure<TValue>(Error error, HttpRequestMessage request) => new Result<TValue>(ClientErrorCode, new List<Error>() { error }, request);

        public static Result Failure(List<Error> errors, HttpRequestMessage request) => new Result(ClientErrorCode, errors, request);

        public static Result<TValue> Failure<TValue>(List<Error> errors, HttpRequestMessage request) => new Result<TValue>(ClientErrorCode, errors, request);

        public static Task<Result> FailureAsync(Error error, HttpRequestMessage request) => Task.FromResult(Failure(error, request));

        public static Task<Result<TValue>> FailureAsync<TValue>(Error error, HttpRequestMessage request) => Task.FromResult(Failure<TValue>(error, request));

        public static Task<Result> FailureAsync(List<Error> error, HttpRequestMessage request) => Task.FromResult(Failure(error, request));

        public static Task<Result<TValue>> FailureAsync<TValue>(List<Error> error, HttpRequestMessage request) => Task.FromResult(Failure<TValue>(error, request));

        public static Result<TDestination> Convert<TSource, TDestination>(Result<TSource> result, HttpRequestMessage request)
        {
            return new Result<TDestination>(result.Code, result.Errors, request);
        }

        #region IHttpActionResult

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Request.CreateResponse((HttpStatusCode)Code));
        }

        #endregion
    }

    public class Result<TValue> : Result, IHttpActionResult
    {
        public TValue Value { get; }

        protected internal Result(int code, HttpRequestMessage request)
            : this(code, new List<Error>(), request) { }

        protected internal Result(int code, List<Error> errors, HttpRequestMessage request)
            : this(code, default, errors, request) { }

        protected internal Result(int code, TValue value, HttpRequestMessage request)
            : this(code, value, new List<Error>(), request) { }

        protected internal Result(int code, TValue value, List<Error> errors, HttpRequestMessage request)
            : base(code, errors, request)
        {
            Value = value;
        }

        #region IHttpActionResult

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (Value == null)
                return Task.FromResult(Request.CreateResponse((HttpStatusCode)Code));
            else
                return Task.FromResult(Request.CreateResponse((HttpStatusCode)Code, Value));
        }

        #endregion
    }
}
