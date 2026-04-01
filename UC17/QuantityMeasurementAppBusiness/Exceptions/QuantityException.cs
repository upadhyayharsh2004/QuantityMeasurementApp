    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace QuantityMeasurementAppBusiness.Exceptions
    {
        public class QuantityException : Exception
        {
            public QuantityException(string ExceptionMessage) : base(ExceptionMessage)
            {
            }

             public QuantityException(string QuantityMessage, Exception DatabaseException) : base(QuantityMessage, DatabaseException)
            {
            }
        }
    }