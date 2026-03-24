using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using QuantityMeasurementAppModels.DTOs;

namespace QuantityMeasurementAppServices.Interfaces
{
    /// <summary>
    /// ============================================================================================================
    /// INTERFACE: IQuantityMeasurementService
    /// ============================================================================================================
    /// 
    /// PURPOSE:
    /// This interface defines the CONTRACT for all quantity measurement-related operations
    /// in the application service layer.
    /// 
    /// It acts as a bridge between:
    /// - The BUSINESS LOGIC layer (implementations like Quantity, Measurement classes)
    /// - The PRESENTATION/API layer (controllers, UI, etc.)
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// DESIGN PRINCIPLE:
    /// 
    /// This interface follows key software design principles:
    /// 
    /// 1. ABSTRACTION
    ///    → Hides implementation details from consumers
    /// 
    /// 2. LOOSE COUPLING
    ///    → Consumers depend on interface, not concrete implementation
    /// 
    /// 3. SEPARATION OF CONCERNS
    ///    → Keeps business logic separate from service orchestration
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// DATA TRANSFER OBJECT (DTO):
    /// 
    /// All methods use QuantityDTO instead of domain models.
    /// 
    /// WHY DTO?
    /// - Prevents exposing internal business models
    /// - Provides a clean contract for API/UI
    /// - Enables validation and transformation at service boundaries
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// RESPONSIBILITIES:
    /// 
    /// This service is responsible for:
    /// - Performing arithmetic operations on quantities
    /// - Converting between different units
    /// - Comparing quantities across units
    /// - Ensuring correct unit handling via target unit inputs
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// NOTE:
    /// 
    /// The actual logic for conversion and arithmetic is handled in business layer classes.
    /// This service simply orchestrates calls and ensures correct data flow.
    /// 
    /// ============================================================================================================
    /// </summary>
    public interface IQuantityMeasurementService
    {
        /// <summary>
        /// METHOD: Add
        /// --------------------------------------------------------------------------------------------------------
        /// 
        /// PURPOSE:
        /// Performs addition of two quantity values and returns the result in a specified target unit.
        /// 
        /// PROCESS:
        /// 1. Validate both input quantities
        /// 2. Convert both quantities into a common base unit
        /// 3. Perform addition in base unit
        /// 4. Convert the result into the target unit
        /// 
        /// EXAMPLE:
        /// - Input: 1 Meter + 100 Centimeter, targetUnit = "Meter"
        /// - Output: 2 Meter
        /// 
        /// </summary>
        /// <param name="first">The first quantity operand.</param>
        /// <param name="second">The second quantity operand.</param>
        /// <param name="targetUnit">The unit in which result should be returned.</param>
        /// <returns>A QuantityDTO containing the result of addition.</returns>
        QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnit);

        /// <summary>
        /// METHOD: Subtract
        /// --------------------------------------------------------------------------------------------------------
        /// 
        /// PURPOSE:
        /// Performs subtraction between two quantities and returns the result in a specified target unit.
        /// 
        /// PROCESS:
        /// 1. Convert both quantities to base unit
        /// 2. Subtract second from first
        /// 3. Convert result into target unit
        /// 
        /// EXAMPLE:
        /// - Input: 2 Meter - 50 Centimeter, targetUnit = "Meter"
        /// - Output: 1.5 Meter
        /// 
        /// NOTE:
        /// - Result may be rounded depending on implementation rules
        /// 
        /// </summary>
        /// <param name="first">The quantity from which value is subtracted.</param>
        /// <param name="second">The quantity to subtract.</param>
        /// <param name="targetUnit">The desired unit of the result.</param>
        /// <returns>A QuantityDTO representing the subtraction result.</returns>
        QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnit);

        /// <summary>
        /// METHOD: Divide
        /// --------------------------------------------------------------------------------------------------------
        /// 
        /// PURPOSE:
        /// Divides one quantity by another and returns a scalar (numeric) result.
        /// 
        /// PROCESS:
        /// 1. Convert both quantities into base unit
        /// 2. Perform division (first / second)
        /// 
        /// EXAMPLE:
        /// - Input: 10 Meter ÷ 2 Meter
        /// - Output: 5
        /// 
        /// VALIDATION:
        /// - Division by zero is not allowed
        /// 
        /// </summary>
        /// <param name="first">The numerator quantity.</param>
        /// <param name="second">The denominator quantity.</param>
        /// <returns>A double representing the division result.</returns>
        double Divide(QuantityDTO first, QuantityDTO second);

        /// <summary>
        /// METHOD: Compare
        /// --------------------------------------------------------------------------------------------------------
        /// 
        /// PURPOSE:
        /// Compares two quantities to determine whether they are equal.
        /// 
        /// PROCESS:
        /// 1. Convert both quantities into base unit
        /// 2. Compare values using tolerance (epsilon)
        /// 
        /// WHY EPSILON?
        /// - Floating-point values may have precision errors
        /// - Direct comparison (==) is unreliable
        /// 
        /// EXAMPLE:
        /// - 1 Meter == 100 Centimeter → TRUE
        /// 
        /// </summary>
        /// <param name="first">The first quantity to compare.</param>
        /// <param name="second">The second quantity to compare.</param>
        /// <returns>True if both quantities are equal, otherwise false.</returns>
        bool Compare(QuantityDTO first, QuantityDTO second);

        /// <summary>
        /// METHOD: Convert
        /// --------------------------------------------------------------------------------------------------------
        /// 
        /// PURPOSE:
        /// Converts a given quantity into a specified target unit.
        /// 
        /// PROCESS:
        /// 1. Convert input quantity into base unit
        /// 2. Convert base value into target unit
        /// 
        /// EXAMPLE:
        /// - Input: 1 Meter → targetUnit = "Centimeter"
        /// - Output: 100 Centimeter
        /// 
        /// </summary>
        /// <param name="quantity">The quantity to convert.</param>
        /// <param name="targetUnit">The unit to convert into.</param>
        /// <returns>A new QuantityDTO with converted value and unit.</returns>
        QuantityDTO Convert(QuantityDTO quantity, string targetUnit);
    }
}