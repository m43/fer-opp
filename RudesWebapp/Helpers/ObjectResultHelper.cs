using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Services;

namespace RudesWebapp.Helpers
{
    public static class ObjectResultHelper
    {
        public static ObjectResult From(object value, int? statusCode)
        {
            return new ObjectResult(value) {StatusCode = statusCode};
        }

        public static ObjectResult FromServiceResult(ServiceResult serviceResult)
        {
            return new ObjectResult(serviceResult) {StatusCode = serviceResult.StatusCode};
        }
    }
}