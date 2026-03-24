using System;

namespace QuantityMeasurementAppBusiness.Exceptions
{
    /// <summary>
    /// ============================================================================================================
    /// CLASS: DatabaseException
    /// ============================================================================================================
    /// 
    /// PURPOSE:
    /// This class represents a CUSTOM EXCEPTION specifically designed to handle
    /// database-related errors within the Quantity Measurement Application.
    /// 
    /// Instead of relying on generic exceptions (such as SqlException, Exception, etc.),
    /// this custom exception provides a more structured and meaningful way to represent
    /// database failures in the application.
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// WHY USE A CUSTOM DATABASE EXCEPTION?
    /// 
    /// Using a dedicated DatabaseException class provides several important advantages:
    /// 
    /// 1. CLEAR ERROR CONTEXT
    ///    → Instantly indicates that the error originated from database operations
    /// 
    /// 2. BETTER ERROR HANDLING
    ///    → Allows catching all database-related issues using a single exception type
    ///      Example:
    ///          catch (DatabaseException ex)
    /// 
    /// 3. CLEAN ARCHITECTURE
    ///    → Separates business logic errors from infrastructure (database) errors
    /// 
    /// 4. EXTENSIBILITY
    ///    → Future enhancements can include:
    ///        - Error codes
    ///        - Database query details
    ///        - Logging metadata
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// INHERITANCE:
    /// 
    /// This class inherits from:
    ///     System.Exception
    /// 
    /// This ensures:
    /// - Compatibility with standard exception handling mechanisms
    /// - Ability to use try-catch-finally blocks
    /// - Support for exception chaining
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// USAGE SCENARIOS:
    /// 
    /// This exception should be used in situations such as:
    /// 
    /// - Failure to connect to the database
    /// - Errors while executing SQL queries
    /// - Data retrieval or insertion failures
    /// - Transaction failures
    /// 
    /// Example:
    ///     throw new DatabaseException("Failed to retrieve data from database");
    /// 
    /// ============================================================================================================
    /// </summary>
    public class DatabaseException : Exception
    {
        /// <summary>
        /// CONSTRUCTOR 1:
        /// 
        /// Initializes a new instance of the DatabaseException class
        /// with a specified error message.
        /// 
        /// USE CASE:
        /// - When a database-related error occurs and you want to provide
        ///   a descriptive message explaining the issue.
        /// - No inner exception is involved.
        /// 
        /// EXAMPLE:
        ///     throw new DatabaseException("Unable to connect to database");
        /// 
        /// </summary>
        /// <param name="message">
        /// A human-readable message describing the database error.
        /// </param>
        public DatabaseException(string message) : base(message)
        {
        }

        /// <summary>
        /// CONSTRUCTOR 2:
        /// 
        /// Initializes a new instance of the DatabaseException class
        /// with a specified error message and an inner exception.
        /// 
        /// PURPOSE:
        /// - Supports exception chaining
        /// - Preserves the original exception details (stack trace, message)
        /// - Adds higher-level context specific to database operations
        /// 
        /// USE CASE:
        /// 
        /// When catching a low-level exception (e.g., SQL exception),
        /// and rethrowing it as a DatabaseException:
        /// 
        /// Example:
        ///     try
        ///     {
        ///         // Database operation
        ///     }
        ///     catch (Exception ex)
        ///     {
        ///         throw new DatabaseException("Error while executing database query", ex);
        ///     }
        /// 
        /// BENEFITS:
        /// - Helps in debugging by preserving original cause
        /// - Improves readability of error logs
        /// - Maintains abstraction between layers
        /// 
        /// </summary>
        /// <param name="message">
        /// A descriptive message explaining the database error.
        /// </param>
        /// <param name="innerException">
        /// The original exception that caused this error.
        /// </param>
        public DatabaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}