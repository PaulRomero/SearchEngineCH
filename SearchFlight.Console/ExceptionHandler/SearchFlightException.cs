using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SearchFlight.Console.ExceptionHandler
{
    public class SearchFlightException: Exception
    {
        public SearchFlightException()
        {
        }

        public SearchFlightException(string message)
            : base(message)
        {
        }

        public SearchFlightException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SearchFlightException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
