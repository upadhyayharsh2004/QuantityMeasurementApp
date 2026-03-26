namespace QuantityMeasurementAppRepositories.Exceptions
{
    /*
     * Custom exception class for handling all database-related failures
     */
    public class ExtremelyCriticalDatabaseOperationFailureException : Exception
    {
        /*
         * Constructor for initializing exception with detailed error message
         */
        public ExtremelyCriticalDatabaseOperationFailureException(
            string detailedErrorMessageDescribingDatabaseFailureScenario)
            : base("❌ Database operation failed: " + detailedErrorMessageDescribingDatabaseFailureScenario)
        {
        }

        /*
         * Constructor for initializing exception with error message and inner exception
         */
        public ExtremelyCriticalDatabaseOperationFailureException(
            string detailedErrorMessageDescribingDatabaseFailureScenario,
            Exception originalUnderlyingExceptionThatCausedDatabaseFailure)
            : base(
                "❌ Database operation encountered a critical failure: " 
                + detailedErrorMessageDescribingDatabaseFailureScenario,
                originalUnderlyingExceptionThatCausedDatabaseFailure)
        {
        }
    }
}