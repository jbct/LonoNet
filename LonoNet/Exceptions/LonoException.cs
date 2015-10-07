using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace LonoNet.Exceptions
{
    public class LonoException : Exception
    {
        public LonoException()
        {

        }

        public LonoException(string message) : base(message)
        {
        }
    }

    public class LonoRestException : LonoException
    {
        /// <summary>
        /// Returned status code from the request
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Expected status codes to have seen instead of the one recieved. 
        /// </summary>
        public HttpStatusCode[] ExpectedCodes { get; private set; }

        /// <summary>
        /// The response of the error call (for Debugging use)
        /// </summary>
        public IRestResponse Response { get; private set; }

        public LonoRestException()
        {
        }

        public LonoRestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a LonoRestException with the REST response which caused the exception, and the status codes which were expected. 
        /// </summary>
        /// <param name="r">REST response which was not expected.</param>
        /// <param name="expectedCodes">The expected status codes which were not found.</param>
        public LonoRestException(IRestResponse r, params HttpStatusCode[] expectedCodes)
        {
            Response = r;
            StatusCode = r.StatusCode;
            ExpectedCodes = expectedCodes;
        }

        /// <summary>
        /// Overridden message for Lono Exception. 
        /// <returns>
        /// The exception message in the format of "Received Response [{0}] : Expected to see [{1}]. The HTTP response was [{2}].
        /// </returns>
        /// </summary>
	    public override string Message
        {
            get
            {
                return string.Format("Received Response [{0}] : Expected to see [{1}]. The HTTP response was [{2}].",
                    Response.StatusCode,
                    string.Join(", ", ExpectedCodes.Select(code => Enum.GetName(typeof(HttpStatusCode), code))),
                    Response.Content);
            }
        }
    }
}
