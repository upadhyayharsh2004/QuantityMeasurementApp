using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    /// <summary>
    /// ============================================================================================================
    /// CLASS: WeightMeasurementImpl
    /// ============================================================================================================
    /// 
    /// PURPOSE:
    /// This class provides a concrete implementation of the IMeasurable interface specifically for handling
    /// Weight measurements within the Quantity Measurement Application.
    /// 
    /// It is responsible for:
    /// - Representing different weight units (Kilogram, Gram, Pound)
    /// - Converting values between different weight units
    /// - Standardizing all operations using a base unit (Kilogram)
    /// - Supporting arithmetic operations such as addition and subtraction
    /// - Dynamically resolving units based on string input
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// CORE DESIGN PRINCIPLE:
    /// 
    /// This implementation follows a "Base Unit Conversion Strategy".
    /// 
    /// The process works as follows:
    /// 1. Any input value is first converted into a BASE UNIT (Kilogram)
    /// 2. All internal calculations (if required) are performed using the base unit
    /// 3. The result is then converted back into the desired target unit
    /// 
    /// This approach ensures:
    /// - High accuracy in conversions
    /// - Elimination of direct unit-to-unit conversion complexity
    /// - Easy scalability when adding new units
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// BASE UNIT:
    /// 
    /// The base unit used in this implementation is:
    /// → Kilogram (kg)
    /// 
    /// All other units are defined relative to this base unit.
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// SUPPORTED UNITS AND THEIR RELATIONSHIP TO BASE UNIT:
    /// 
    /// - Kilogram   → 1.0       (Base Unit)
    /// - Gram       → 0.001     (1 gram = 0.001 kg)
    /// - Pound      → 0.453592  (1 pound ≈ 0.453592 kg)
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// ARITHMETIC SUPPORT:
    /// 
    /// Weight measurements fully support arithmetic operations such as:
    /// - Addition
    /// - Subtraction
    /// 
    /// Reason:
    /// Since all units can be converted into a common base unit (Kilogram),
    /// performing arithmetic operations becomes logically valid and consistent.
    /// 
    /// ------------------------------------------------------------------------------------------------------------
    /// EXTENSIBILITY:
    /// 
    /// This class is designed to be easily extendable.
    /// To add a new unit (e.g., Ounce), you only need to:
    /// - Add it to the WeightUnit enum
    /// - Define its conversion factor in GetConversionFactor()
    /// 
    /// No other logic changes are required.
    /// ============================================================================================================
    /// </summary>
    public class WeightMeasurementImpl : IMeasurable
    {
        /// <summary>
        /// Stores the current weight unit for this instance.
        /// 
        /// This variable determines:
        /// - Which conversion factor will be used
        /// - How input values are interpreted
        /// - How output values are returned
        /// 
        /// Example:
        /// If unit = Gram → all operations will treat values as grams.
        /// </summary>
        private MassMeasurementSystemIdentifier unit;

        /// <summary>
        /// CONSTRUCTOR:
        /// Initializes a new instance of the WeightMeasurementImpl class with a specific unit.
        /// 
        /// This constructor sets the context for all operations performed by this object.
        /// Once initialized, the object will behave according to the selected unit.
        /// 
        /// Example Usage:
        /// - new WeightMeasurementImpl(WeightUnit.Kilogram)
        /// - new WeightMeasurementImpl(WeightUnit.Gram)
        /// - new WeightMeasurementImpl(WeightUnit.Pound)
        /// 
        /// </summary>
        /// <param name="unitType">The weight unit to be assigned to this instance.</param>
        public WeightMeasurementImpl(MassMeasurementSystemIdentifier unitType)
        {
            unit = unitType;
        }

        /// <summary>
        /// Retrieves the conversion factor of the current unit relative to the base unit (Kilogram).
        /// 
        /// This factor is used to convert any given value into kilograms.
        /// 
        /// INTERNAL LOGIC:
        /// - Each unit has a predefined multiplier that represents its equivalent in kilograms.
        /// - The method uses a switch-case structure to determine the correct factor.
        /// 
        /// EXAMPLES:
        /// - Kilogram → 1.0 (already in base unit)
        /// - Gram → 0.001 (1 gram = 0.001 kg)
        /// - Pound → 0.453592 (1 pound ≈ 0.453592 kg)
        /// 
        /// ERROR HANDLING:
        /// - If an unsupported unit is encountered, an exception is thrown
        ///   to prevent incorrect conversions.
        /// 
        /// </summary>
        /// <returns>The conversion factor to convert the unit into kilograms.</returns>
        public double GetConversionFactor()
        {
            switch (unit)
            {
                case MassMeasurementSystemIdentifier.GLOBAL_STANDARD_MASS_UNIT:
                    return 1.0;

                case MassMeasurementSystemIdentifier.MICRO_SCALE_MASS_UNIT:
                    return 0.001;

                case MassMeasurementSystemIdentifier.REGIONAL_IMPERIAL_MASS_UNIT:
                    return 0.453592;

                default:
                    throw new ArgumentException("Invalid Weight Unit");
            }
        }

        /// <summary>
        /// Converts a given value from the current unit into the base unit (Kilogram).
        /// 
        /// FORMULA USED:
        ///     value_in_kg = value * conversion_factor
        /// 
        /// STEP-BY-STEP PROCESS:
        /// 1. Retrieve the conversion factor using GetConversionFactor()
        /// 2. Multiply the input value with the conversion factor
        /// 3. Return the result in kilograms
        /// 
        /// EXAMPLES:
        /// - 1000 grams → 1000 * 0.001 = 1 kg
        /// - 2 pounds → 2 * 0.453592 = 0.907184 kg
        /// 
        /// This method ensures all values are normalized into a single base unit
        /// for consistent processing and calculations.
        /// </summary>
        /// <param name="value">The value in the current unit.</param>
        /// <returns>The equivalent value in kilograms.</returns>
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        /// <summary>
        /// Converts a value from the base unit (Kilogram) back into the current unit.
        /// 
        /// FORMULA USED:
        ///     value_in_unit = base_value / conversion_factor
        /// 
        /// STEP-BY-STEP PROCESS:
        /// 1. Retrieve the conversion factor using GetConversionFactor()
        /// 2. Divide the base value by the conversion factor
        /// 3. Return the result in the target unit
        /// 
        /// EXAMPLES:
        /// - 1 kg → 1 / 0.001 = 1000 grams
        /// - 0.907184 kg → 0.907184 / 0.453592 = 2 pounds
        /// 
        /// This method is typically used after performing calculations in kilograms.
        /// </summary>
        /// <param name="baseValue">The value in kilograms.</param>
        /// <returns>The equivalent value in the current unit.</returns>
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        /// <summary>
        /// Returns the name of the current weight unit.
        /// 
        /// This method converts the enum value into its string representation.
        /// It is useful for:
        /// - Displaying unit names in UI
        /// - Logging and debugging
        /// - Providing readable output to users
        /// 
        /// EXAMPLES:
        /// - "Kilogram"
        /// - "Gram"
        /// - "Pound"
        /// </summary>
        /// <returns>The name of the unit as a string.</returns>
        public string GetUnitName()
        {
            return unit.ToString();
        }

        /// <summary>
        /// Indicates whether arithmetic operations are supported for weight measurements.
        /// 
        /// For weight:
        /// - Arithmetic operations are logically valid
        /// - Values can be safely converted into a common base unit (Kilogram)
        /// - Operations like addition and subtraction produce meaningful results
        /// 
        /// Therefore, this method always returns TRUE.
        /// </summary>
        /// <returns>True, indicating arithmetic operations are supported.</returns>
        public bool SupportsArithmetic()
        {
            return true;
        }

        /// <summary>
        /// Validates whether a specific arithmetic operation is allowed.
        /// 
        /// In the case of weight measurements:
        /// - There are no restrictions on arithmetic operations
        /// - All operations (Add, Subtract, etc.) are permitted
        /// 
        /// This method exists to maintain consistency with the interface contract.
        /// 
        /// NOTE:
        /// No validation logic is required, so this method intentionally contains no implementation.
        /// </summary>
        /// <param name="operation">The arithmetic operation to validate.</param>
        public void ValidateOperationSupport(string operation)
        {
            // All arithmetic operations are supported for Weight.
            // No validation or restriction is required here.
        }

        /// <summary>
        /// Returns the type/category of the measurement.
        /// 
        /// This method helps identify that this implementation belongs to "Weight".
        /// It can be used for:
        /// - Grouping different measurement types
        /// - Applying conditional logic based on measurement category
        /// - Displaying type information in UI
        /// 
        /// </summary>
        /// <returns>The string "Weight".</returns>
        public string GetMeasurementType()
        {
            return "Weight";
        }

        /// <summary>
        /// Dynamically retrieves a unit instance based on the provided unit name.
        /// 
        /// FUNCTIONALITY:
        /// - Converts a string input into a corresponding WeightUnit enum value
        /// - Creates and returns a new instance of WeightMeasurementImpl using that unit
        /// 
        /// FEATURES:
        /// - Case-insensitive parsing (user-friendly)
        /// - Supports dynamic unit selection at runtime
        /// 
        /// EXAMPLES:
        /// - Input: "Kilogram" → Returns WeightMeasurementImpl(Kilogram)
        /// - Input: "gram" → Returns WeightMeasurementImpl(Gram)
        /// - Input: "POUND" → Returns WeightMeasurementImpl(Pound)
        /// 
        /// USE CASE:
        /// Useful when unit names are provided as user input (e.g., from UI or API).
        /// </summary>
        /// <param name="unitName">The name of the unit as a string.</param>
        /// <returns>A new IMeasurable instance for the specified unit.</returns>
        public IMeasurable GetUnitByName(string unitName)
        {
            MassMeasurementSystemIdentifier parsedUnit = (MassMeasurementSystemIdentifier)Enum.Parse(typeof(MassMeasurementSystemIdentifier), unitName, true);
            return new WeightMeasurementImpl(parsedUnit);
        }
    }
}