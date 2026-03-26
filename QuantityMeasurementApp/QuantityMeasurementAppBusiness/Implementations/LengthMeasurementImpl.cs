using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    /// <summary>
    /// This class provides the implementation of the IMeasurable interface specifically for Length measurements.
    /// 
    /// It supports multiple length units such as Feet, Inch, Yard, and Centimeter,
    /// and provides functionality to:
    /// - Convert values between different units using a base unit (Feet)
    /// - Retrieve unit-specific information
    /// - Support arithmetic operations on length values
    /// 
    /// The base unit chosen for all conversions in this implementation is "Feet".
    /// All other units are converted to and from Feet for consistency.
    /// </summary>
    public class LengthMeasurementImpl : IMeasurable
    {
        /// <summary>
        /// Stores the current length unit type for this instance.
        /// This determines how conversion and representation will be handled.
        /// </summary>
        private LinearMeasurementUnitCategoryIdentifier unit;

        /// <summary>
        /// Constructor to initialize the LengthMeasurementImpl object with a specific length unit.
        /// 
        /// Example:
        /// - LengthMeasurementImpl(LengthUnit.Feet)
        /// - LengthMeasurementImpl(LengthUnit.Inch)
        /// 
        /// This sets the context for all operations performed by this object.
        /// </summary>
        /// <param name="unitType">The specific LengthUnit enum value representing the unit.</param>
        public LengthMeasurementImpl(LinearMeasurementUnitCategoryIdentifier unitType)
        {
            unit = unitType;
        }

        /// <summary>
        /// Returns the conversion factor of the current unit relative to the base unit (Feet).
        /// 
        /// This factor is used to convert any given value into Feet.
        /// 
        /// Conversion references:
        /// - Feet → 1.0 (base unit)
        /// - Inch → 1/12 (since 12 inches = 1 foot)
        /// - Yard → 3.0 (since 1 yard = 3 feet)
        /// - Centimeter → 0.0328084 (1 cm ≈ 0.0328084 feet)
        /// 
        /// This method plays a critical role in standardizing all unit conversions.
        /// </summary>
        /// <returns>The conversion factor relative to Feet.</returns>
        public double GetConversionFactor()
        {
            switch (unit)
            {
                case LinearMeasurementUnitCategoryIdentifier.IMPERIAL_FOOT_BASED_UNIT:
                    return 1.0;

                case LinearMeasurementUnitCategoryIdentifier.IMPERIAL_SMALL_SCALE_UNIT:
                    return 1.0 / 12.0;

                case LinearMeasurementUnitCategoryIdentifier.IMPERIAL_EXTENDED_RANGE_UNIT:
                    return 3.0;

                case LinearMeasurementUnitCategoryIdentifier.METRIC_STANDARD_SUBDIVISION_UNIT:
                    return 0.0328084;

                default:
                    throw new ArgumentException("Invalid Length Unit");
            }
        }

        /// <summary>
        /// Converts a given value from the current unit into the base unit (Feet).
        /// 
        /// The conversion is performed using the formula:
        ///     value_in_feet = value * conversion_factor
        /// 
        /// Example:
        /// - 12 inches → 12 * (1/12) = 1 foot
        /// - 2 yards → 2 * 3 = 6 feet
        /// 
        /// This ensures all calculations can be standardized using Feet.
        /// </summary>
        /// <param name="value">The value in the current unit.</param>
        /// <returns>The equivalent value in Feet.</returns>
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        /// <summary>
        /// Converts a value from the base unit (Feet) back to the current unit.
        /// 
        /// The conversion is performed using the formula:
        ///     value_in_unit = base_value / conversion_factor
        /// 
        /// Example:
        /// - 1 foot → 1 / (1/12) = 12 inches
        /// - 6 feet → 6 / 3 = 2 yards
        /// 
        /// This method is typically used after performing calculations in the base unit.
        /// </summary>
        /// <param name="baseValue">The value in Feet.</param>
        /// <returns>The equivalent value in the current unit.</returns>
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        /// <summary>
        /// Returns the name of the current unit as a string.
        /// 
        /// This is useful for displaying the unit in UI or logs.
        /// 
        /// Example outputs:
        /// - "Feet"
        /// - "Inch"
        /// - "Yard"
        /// - "Centimeter"
        /// </summary>
        /// <returns>The unit name as a string.</returns>
        public string GetUnitName()
        {
            return unit.ToString();
        }

        /// <summary>
        /// Indicates whether arithmetic operations (such as addition and subtraction)
        /// are supported for length measurements.
        /// 
        /// For Length, arithmetic operations are fully supported because:
        /// - All values can be normalized to a base unit (Feet)
        /// - Operations like addition and subtraction are logically valid
        /// 
        /// Therefore, this method always returns true.
        /// </summary>
        /// <returns>True, since arithmetic operations are supported.</returns>
        public bool SupportsArithmetic()
        {
            return true;
        }

        /// <summary>
        /// Validates whether a specific arithmetic operation is allowed.
        /// 
        /// In the case of Length measurements, there are no restrictions on arithmetic operations.
        /// All operations such as addition and subtraction are considered valid.
        /// 
        /// Therefore, this method does not perform any checks or throw exceptions.
        /// It is implemented to comply with the interface contract.
        /// </summary>
        /// <param name="operation">The name of the arithmetic operation.</param>
        public void ValidateOperationSupport(string operation)
        {
            // All arithmetic operations are supported for Length,
            // so no validation logic is required here.
        }

        /// <summary>
        /// Returns the type/category of measurement.
        /// 
        /// This helps in identifying that this implementation belongs to Length measurement.
        /// It can be used for grouping, validation, or conditional logic in the application.
        /// 
        /// Example return value:
        /// - "Length"
        /// </summary>
        /// <returns>The measurement type as a string.</returns>
        public string GetMeasurementType()
        {
            return "Length";
        }

        /// <summary>
        /// Retrieves a unit instance based on the provided unit name.
        /// 
        /// This method dynamically converts a string into a LengthUnit enum value
        /// and returns a new instance of LengthMeasurementImpl configured with that unit.
        /// 
        /// The parsing is case-insensitive to improve usability.
        /// 
        /// Example:
        /// - Input: "Feet" → Returns LengthMeasurementImpl(LengthUnit.Feet)
        /// - Input: "inch" → Returns LengthMeasurementImpl(LengthUnit.Inch)
        /// 
        /// This is particularly useful for handling user input or dynamic unit selection.
        /// </summary>
        /// <param name="unitName">The name of the unit as a string.</param>
        /// <returns>A new IMeasurable instance corresponding to the specified unit.</returns>
        public IMeasurable GetUnitByName(string unitName)
        {
            LinearMeasurementUnitCategoryIdentifier parsedUnit = (LinearMeasurementUnitCategoryIdentifier)Enum.Parse(typeof(LinearMeasurementUnitCategoryIdentifier), unitName, true);
            return new LengthMeasurementImpl(parsedUnit);
        }
    }
}