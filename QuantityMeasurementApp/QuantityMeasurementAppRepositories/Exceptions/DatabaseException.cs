namespace QuantityMeasurementAppRepositories.Exceptions
{
    /*
     * ============================================================================================
     * CLASS: DatabaseException
     * 
     * This is a custom exception class specifically designed to handle errors related to
     * database operations within the application.
     * 
     * Why create a custom exception?
     * --------------------------------------------------------------------------------------------
     * - Helps in categorizing errors (e.g., database errors vs validation errors)
     * - Makes error handling more meaningful and structured
     * - Improves debugging by providing context-specific exception types
     * - Allows higher layers (like services or controllers) to react differently
     *   depending on the type of exception
     * 
     * Instead of throwing a generic Exception, using DatabaseException clearly indicates
     * that something went wrong at the database or data access level.
     * 
     * Inheritance:
     * --------------------------------------------------------------------------------------------
     * This class inherits from the base 'Exception' class provided by .NET.
     * 
     * This means:
     * - It behaves like a standard exception
     * - It can be caught using catch (Exception) or catch (DatabaseException)
     * - It supports all built-in exception features like message, stack trace, etc.
     * ============================================================================================
     */
    public class DatabaseException : Exception
    {
        /*
         * ----------------------------------------------------------------------------------------
         * CONSTRUCTOR 1: DatabaseException(string message)
         * 
         * Purpose:
         * - Initializes a new instance of the DatabaseException class with a specific error message.
         * 
         * Parameter:
         * - message : A descriptive explanation of what went wrong in the database operation.
         * 
         * Behavior:
         * - Calls the base Exception class constructor using 'base(message)'
         * - This ensures that the error message is properly stored and accessible
         *   via the Exception.Message property
         * 
         * Example Usage:
         * - throw new DatabaseException("Failed to connect to the database");
         * ----------------------------------------------------------------------------------------
         */
        public DatabaseException(string message) : base(message) { }

        /*
         * ----------------------------------------------------------------------------------------
         * CONSTRUCTOR 2: DatabaseException(string message, Exception innerException)
         * 
         * Purpose:
         * - Initializes a new instance of the DatabaseException class with both a custom
         *   error message and an inner exception.
         * 
         * Parameters:
         * - message         : A descriptive explanation of the error
         * - innerException  : The original exception that caused this error
         * 
         * Why use innerException?
         * ----------------------------------------------------------------------------------------
         * - Helps preserve the original error details
         * - Maintains the exception chain for better debugging
         * - Allows developers to trace back to the root cause of the problem
         * 
         * Behavior:
         * - Calls the base Exception constructor with both message and innerException
         * 
         * Example Usage:
         * - try
         *   {
         *       // Database operation
         *   }
         *   catch (Exception ex)
         *   {
         *       throw new DatabaseException("Error while saving data", ex);
         *   }
         * ----------------------------------------------------------------------------------------
         */
        public DatabaseException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}