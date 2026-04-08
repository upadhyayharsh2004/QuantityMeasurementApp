    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace QuantityMeasurementAppBusiness.Exceptions
    {
        public class QuantityException : Exception
        {
            //Exception class is automatically present inside the using System.Collections.Generic or System.Exception
            public QuantityException(string ExceptionMessage) : base(ExceptionMessage)
            {
            }

             public QuantityException(string QuantityMessage, Exception DatabaseException) : base(QuantityMessage, DatabaseException)
            {
            }
        }
    }