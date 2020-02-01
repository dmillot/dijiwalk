using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DijiWalk.Common.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Connection { get; set; }

        public ApiException(Exception inner, HttpStatusCode status, string message = "") : base(message, inner)
        {
            Connection = true;
            StatusCode = status;
        }

        public ApiException( Exception inner, bool connection, string message = "") : base(message, inner)
        {
            Connection = connection;
            StatusCode = HttpStatusCode.ServiceUnavailable;
        }
    }
}

