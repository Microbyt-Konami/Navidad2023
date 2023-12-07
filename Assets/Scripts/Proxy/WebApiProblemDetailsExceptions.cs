using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace microbytkonamic.proxy
{
    public class WebApiProblemDetailsExceptions : System.Exception
    {
        public WebApiProblemDetailsExceptions()
        {
        }

        public WebApiProblemDetailsExceptions(WebApiProblemDetails problemDetails) : base(problemDetails.detail)
        {
            ProblemDetails = problemDetails;
        }

        public WebApiProblemDetailsExceptions(string message) : base(message)
        {
        }

        public WebApiProblemDetailsExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }

        public WebApiProblemDetailsExceptions(WebApiProblemDetails problemDetails, Exception innerException) : base(problemDetails.detail, innerException)
        {
            ProblemDetails = problemDetails;
        }

        protected WebApiProblemDetailsExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            info.AddValue(nameof(ProblemDetails), ProblemDetails);
        }

        public WebApiProblemDetails ProblemDetails { get; }
    }
}
