using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace RudesWebapp.Services
{
    /**
     * Note: see ServiceResultOfT
     */
    public class ServiceResult
    {
        protected internal List<ServiceError> Errors = new List<ServiceError>();

        public bool Succeeded { get; set; }

        [JsonIgnore] public int StatusCode { get; set; }

        public static readonly ServiceResult Success = new ServiceResult
            {Succeeded = true, StatusCode = StatusCodes.Status200OK};

        public ServiceResult()
        {
        }

        public ServiceResult(params ServiceError[] errors)
        {
            Succeeded = false;
            if (errors != null)
            {
                Errors.AddRange(errors);
            }
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public ServiceResult(int statusCode)
        {
            StatusCode = statusCode;
        }
        
        public ModelStateDictionary FillModelState(ModelStateDictionary modelState)
        {
            foreach (var error in Errors)
            {
                modelState.AddModelError(error.Property, error.Description);
            }

            return modelState;
        }
        
        /// <summary>
        /// Factory method that creates an unsuccessful result with status code 400 and given errors.
        /// </summary>
        public static ServiceResult Failed(params ServiceError[] errors)
        {
            return new ServiceResult(errors);
        }

        /// <summary>
        /// Set the status code of the result to the one given.
        /// </summary>
        public ServiceResult WithStatusCode(int statusCode)
        {
            StatusCode = statusCode;
            return this;
        }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}