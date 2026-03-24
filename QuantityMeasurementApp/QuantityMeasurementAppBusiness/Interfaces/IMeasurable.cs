using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementAppBusiness.Interfaces
{
    /// <summary>
    /// The IMeasurable interface defines a standard contract for all measurable units
    /// (such as Length, Weight, Temperature, etc.) in the Quantity Measurement application.
    /// 
    /// Any class implementing this interface must provide logic for:
    /// - Unit conversion (to and from a base unit)
    /// - Unit identification (name and type)
    /// - Arithmetic operation validation and support
    /// - Retrieval of units dynamically by name
    /// 
    /// This abstraction ensures consistency and scalability across different measurement types.
    /// </summary>
    public interface IMeasurable
    {
        /// <summary>
        /// Returns the conversion factor of the current unit relative to its base unit.
        /// 
        /// For example:
        /// - If the unit is "Kilometer", the conversion factor might be 1000 (meters).
        /// - If the unit is "Gram", the conversion factor might be 0.001 (kilograms).
        /// 
        /// This factor is used internally for converting values between units.
        /// </summary>
        double GetConversionFactor();

        /// <summary>
        /// Converts a given value from the current unit into its corresponding base unit.
        /// 
        /// Example:
        /// - Converting 5 kilometers into meters → 5000 meters
        /// 
        /// This method ensures that all calculations can be standardized using a common base unit.
        /// </summary>
        /// <param name="value">The value in the current unit that needs to be converted.</param>
        /// <returns>The equivalent value in the base unit.</returns>
        double ConvertToBaseUnit(double value);

        /// <summary>
        /// Converts a value from the base unit back into the current unit.
        /// 
        /// Example:
        /// - Converting 1000 meters into kilometers → 1 kilometer
        /// 
        /// This is typically used after performing calculations in base units.
        /// </summary>
        /// <param name="baseValue">The value expressed in the base unit.</param>
        /// <returns>The equivalent value in the current unit.</returns>
        double ConvertFromBaseUnit(double baseValue);

        /// <summary>
        /// Retrieves the name of the unit.
        /// 
        /// Example:
        /// - "Meter"
        /// - "Kilogram"
        /// - "Celsius"
        /// 
        /// This helps in identifying and displaying the unit in a user-friendly way.
        /// </summary>
        /// <returns>The name of the unit as a string.</returns>
        string GetUnitName();

        /// <summary>
        /// Indicates whether arithmetic operations (such as addition and subtraction)
        /// are supported for the current measurement type.
        /// 
        /// For example:
        /// - Length and Weight typically support arithmetic operations.
        /// - Temperature may have restricted arithmetic support depending on context.
        /// 
        /// This method is part of UC-14 (Temperature Measurement enhancement).
        /// </summary>
        /// <returns>True if arithmetic operations are supported; otherwise, false.</returns>
        bool SupportsArithmetic();

        /// <summary>
        /// Validates whether a specific arithmetic operation is allowed for the current unit.
        /// 
        /// If the operation is not supported, this method should throw an exception
        /// or handle the validation failure appropriately.
        /// 
        /// Example:
        /// - Allow: Addition for Length
        /// - Restrict: Certain operations for Temperature
        /// 
        /// This ensures safe and correct usage of arithmetic across measurement types.
        /// </summary>
        /// <param name="operation">The name of the operation (e.g., "Add", "Subtract").</param>
        void ValidateOperationSupport(string operation);

        /// <summary>
        /// Returns the category or type of measurement this unit belongs to.
        /// 
        /// Example:
        /// - "Length"
        /// - "Weight"
        /// - "Temperature"
        /// 
        /// This helps group units and apply appropriate rules or logic.
        /// </summary>
        /// <returns>The measurement type as a string.</returns>
        string GetMeasurementType();    

        /// <summary>
        /// Retrieves a unit instance based on its name.
        /// 
        /// This method enables dynamic lookup of units, allowing the system
        /// to fetch the correct unit implementation at runtime.
        /// 
        /// Example:
        /// - Input: "Meter"
        /// - Output: Corresponding IMeasurable implementation for Meter
        /// 
        /// Useful for parsing user input or performing conversions between arbitrary units.
        /// </summary>
        /// <param name="unitName">The name of the unit to retrieve.</param>
        /// <returns>An object implementing IMeasurable for the specified unit.</returns>
        IMeasurable GetUnitByName(string unitName);  
    }
}