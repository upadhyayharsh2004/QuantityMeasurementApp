using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementAppBusiness.Exceptions
{
    /// <summary>
    /// ============================================================================================================
    /// CLASS: QuantityMeasurementException
    /// ============================================================================================================
    /// 
    /// PURPOSE:
    /// This class represents a CUSTOM EXCEPTION specifically designed for the
    /// Quantity Measurement Application.
    /// 
    /// Instead of using generic exceptions (like ArgumentException, InvalidOperationException, etc.),
    /// this custom exception allows us to:
    /// - Provide more meaningful and domain-specific error messages
    /// - Improve debugging and error tracking
    /// - Maintain consistency in exception handling across the application
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// WHY CUSTOM EXCEPTION?
    /// 
    /// Using a custom exception class provides several advantages:
    /// 
    /// 1. DOMAIN-SPECIFIC ERROR HANDLING
    ///    → Clearly indicates that an error is related to quantity measurement logic
    /// 
    /// 2. BETTER READABILITY
    ///    → Easier to understand errors when reading logs or debugging
    /// 
    /// 3. CENTRALIZED ERROR MANAGEMENT
    ///    → Allows handling all measurement-related exceptions in one place
    /// 
    /// 4. EXTENSIBILITY
    ///    → Additional properties (like error codes) can be added later if needed
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// INHERITANCE:
    /// 
    /// This class inherits from:
    ///     System.Exception
    /// 
    /// This means it behaves like a standard exception but is specialized
    /// for this application.
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// USAGE SCENARIOS:
    /// 
    /// This exception can be thrown in cases such as:
    /// - Invalid unit conversions
    /// - Unsupported arithmetic operations
    /// - Mismatched measurement categories
    /// - Invalid input values
    /// 
    /// Example:
    ///     throw new QuantityMeasurementException("Invalid unit conversion");
    /// 
    /// ============================================================================================================
    /// </summary>
    public class QuantityMeasurementException : Exception
    {
        /// <summary>
        /// CONSTRUCTOR 1:
        /// 
        /// Initializes a new instance of the QuantityMeasurementException class
        /// with a specified error message.
        /// 
        /// This constructor is used when:
        /// - You want to throw an exception with a custom message
        /// - There is no underlying (inner) exception to attach
        /// 
        /// Example:
        ///     throw new QuantityMeasurementException("Invalid measurement input");
        /// 
        /// </summary>
        /// <param name="message">A descriptive message explaining the cause of the exception.</param>
        public QuantityMeasurementException(string message) : base(message)
        {
        }

        /// <summary>
        /// CONSTRUCTOR 2:
        /// 
        /// Initializes a new instance of the QuantityMeasurementException class
        /// with a specified error message and an inner exception.
        /// 
        /// This constructor is useful for:
        /// - Exception chaining
        /// - Preserving original exception details
        /// - Providing additional context to higher-level error handlers
        /// 
        /// Example:
        ///     try
        ///     {
        ///         // Some operation
        ///     }
        ///     catch (Exception ex)
        ///     {
        ///         throw new QuantityMeasurementException("Error during conversion", ex);
        ///     }
        /// 
        /// BENEFIT:
        /// - Helps trace the root cause of the error while adding domain-specific context
        /// 
        /// </summary>
        /// <param name="message">A descriptive message explaining the error.</param>
        /// <param name="innerException">The original exception that caused this error.</param>
        public QuantityMeasurementException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}