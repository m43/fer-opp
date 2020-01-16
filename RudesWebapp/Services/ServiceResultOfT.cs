using System.Linq;
using Microsoft.AspNetCore.Http;

namespace RudesWebapp.Services
{
    /**
     * Note that ServiceResult<T> and ServiceResult need remodeling. Not sure how to implement generic subclass
     * by sticking to DRY and the rest by still having the easy of use that factory methods provide.
     */
    public class ServiceResult<T> : ServiceResult
    {
        public T Value { get; set; }

        public new static ServiceResult<T> Success()
        {
            return new ServiceResult<T> {Succeeded = true, StatusCode = StatusCodes.Status200OK};
        }


        private ServiceResult() : base()
        {
        }

        public ServiceResult(int statusCode) : base(statusCode)
        {
        }

        public ServiceResult(params ServiceError[] errors) : base(errors)
        {
        }

        public static ServiceResult<T> FromServiceResult(ServiceResult serviceResult)
        {
            return new ServiceResult<T> {Errors = serviceResult.Errors.ToList(), Succeeded = serviceResult.Succeeded};
        }

        public static ServiceResult<T> FromValue(T value)
        {
            return new ServiceResult<T> {Value = value, Succeeded = true};
        }

        public static ServiceResult<T> FromResultAndValue(ServiceResult serviceResult, T value)
        {
            var result = FromServiceResult(serviceResult);
            result.Value = value;
            return result;
        }
    }
}