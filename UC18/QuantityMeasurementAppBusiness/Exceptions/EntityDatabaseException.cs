using System;

namespace QuantityMeasurementAppBusiness.Exceptions
{
    public class EntityDatabaseException : Exception
    {
        public EntityDatabaseException(string DatabaseMessage) : base(DatabaseMessage)
        {
        }

        public EntityDatabaseException(string DatabaseMessage, Exception DatabaseException)
            : base(DatabaseMessage, DatabaseException)
        {
        }
    }
}