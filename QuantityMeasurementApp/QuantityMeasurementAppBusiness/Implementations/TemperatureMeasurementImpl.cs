using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    /// <summary>
    /// This class provides the implementation of the IMeasurable interface
    /// specifically for Temperature measurements.
    /// 
    /// Unlike other measurement types (such as Length or Weight),
    /// temperature conversions are not linear scaling using a simple factor.
    /// Instead, they involve formulas with offsets (e.g., +32, -273.15).
    /// 
    /// The base unit used for all temperature conversions in this implementation is Celsius.
    /// All other units (Fahrenheit and Kelvin) are converted to and from Celsius.
    /// 
    /// Additionally, arithmetic operations (like addition and subtraction)
    /// are intentionally restricted for temperature values to maintain correctness.
    /// </summary>
    public class TemperatureMeasurementImpl : IMeasurable
    {
        /// <summary>
        /// Stores the current temperature unit for this instance.
        /// This determines how conversion logic will be applied.
        /// </summary>
        private ThermalScaleRepresentationIdentifier unit;

        /// <summary>
        /// Constructor to initialize the TemperatureMeasurementImpl object
        /// with a specific temperature unit.
        /// 
        /// Example:
        /// - TemperatureMeasurementImpl(TemperatureUnit.Celsius)
        /// - TemperatureMeasurementImpl(TemperatureUnit.Fahrenheit)
        /// 
        /// This sets the context for all conversions and operations.
        /// </summary>
        /// <param name="unitType">The temperature unit type.</param>
        public TemperatureMeasurementImpl(ThermalScaleRepresentationIdentifier unitType)
        {
            unit = unitType;
        }

        /// <summary>
        /// Returns a conversion factor for the unit.
        /// 
        /// NOTE:
        /// Temperature conversions do not rely on a simple multiplication factor
        /// like other measurements (e.g., Length or Weight).
        /// 
        /// Because temperature involves offsets (like 32 in Fahrenheit or 273.15 in Kelvin),
        /// this method is not meaningfully applicable.
        /// 
        /// Therefore, a default value of 1.0 is returned to satisfy the interface contract.
        /// </summary>
        /// <returns>Always returns 1.0 for temperature.</returns>
        public double GetConversionFactor()
        {
            return 1.0;
        }

        /// <summary>
        /// Converts a given temperature value from the current unit into the base unit (Celsius).
        /// 
        /// Conversion formulas used:
        /// - Celsius → Celsius: value (no change)
        /// - Fahrenheit → Celsius: (value - 32) × 5/9
        /// - Kelvin → Celsius: value - 273.15
        /// 
        /// This method standardizes all temperature values into Celsius
        /// so that further processing can be done consistently.
        /// </summary>
        /// <param name="value">The temperature value in the current unit.</param>
        /// <returns>The equivalent temperature in Celsius.</returns>
        public double ConvertToBaseUnit(double value)
        {
            switch (unit)
            {
                case ThermalScaleRepresentationIdentifier.STANDARD_METRIC_HEAT_SCALE:
                    return value;

                case ThermalScaleRepresentationIdentifier.REGIONAL_HEAT_MEASUREMENT_SCALE:
                    return (value - 32) * 5 / 9;

                case ThermalScaleRepresentationIdentifier.ABSOLUTE_ENERGY_SCALE_SYSTEM:
                    return value - 273.15;

                default:
                    throw new ArgumentException("Invalid Temperature Unit");
            }
        }

        /// <summary>
        /// Converts a temperature value from the base unit (Celsius)
        /// into the current target unit.
        /// 
        /// Conversion formulas used:
        /// - Celsius → Celsius: value (no change)
        /// - Celsius → Fahrenheit: (value × 9/5) + 32
        /// - Celsius → Kelvin: value + 273.15
        /// 
        /// This method is typically used after performing operations
        /// in the base unit (Celsius).
        /// </summary>
        /// <param name="baseValue">The temperature value in Celsius.</param>
        /// <returns>The equivalent temperature in the current unit.</returns>
        public double ConvertFromBaseUnit(double baseValue)
        {
            switch (unit)
            {
                case ThermalScaleRepresentationIdentifier.STANDARD_METRIC_HEAT_SCALE:
                    return baseValue;

                case ThermalScaleRepresentationIdentifier.REGIONAL_HEAT_MEASUREMENT_SCALE:
                    return (baseValue * 9 / 5) + 32;

                case ThermalScaleRepresentationIdentifier.ABSOLUTE_ENERGY_SCALE_SYSTEM:
                    return baseValue + 273.15;

                default:
                    throw new ArgumentException("Invalid Temperature Unit");
            }
        }

        /// <summary>
        /// Returns the name of the current temperature unit.
        /// 
        /// Example outputs:
        /// - "Celsius"
        /// - "Fahrenheit"
        /// - "Kelvin"
        /// 
        /// This is useful for displaying unit information in UI or logs.
        /// </summary>
        /// <returns>The unit name as a string.</returns>
        public string GetUnitName()
        {
            return unit.ToString();
        }

        /// <summary>
        /// Indicates whether arithmetic operations are supported for temperature values.
        /// 
        /// In this implementation, arithmetic operations are NOT supported because:
        /// - Adding or subtracting temperatures directly can be misleading or incorrect
        /// - Temperature differences and absolute temperatures are conceptually different
        /// 
        /// Therefore, this method always returns false.
        /// </summary>
        /// <returns>False, since arithmetic operations are not supported.</returns>
        public bool SupportsArithmetic()
        {
            return false;
        }

        /// <summary>
        /// Validates whether a specific arithmetic operation is allowed on temperature values.
        /// 
        /// Since temperature does not support arithmetic operations in this design,
        /// this method always throws a NotSupportedException.
        /// 
        /// This prevents invalid operations such as:
        /// - Adding two temperatures
        /// - Subtracting temperatures incorrectly
        /// 
        /// Ensures the integrity and correctness of temperature handling.
        /// </summary>
        /// <param name="operation">The name of the attempted operation.</param>
        public void ValidateOperationSupport(string operation)
        {
            throw new NotSupportedException("Temperature does not support " + operation + " operation");
        }

        /// <summary>
        /// Returns the type/category of measurement.
        /// 
        /// This helps identify that this implementation is related to Temperature.
        /// It can be used for grouping logic or applying specific rules.
        /// 
        /// Example return value:
        /// - "Temperature"
        /// </summary>
        /// <returns>The measurement type as a string.</returns>
        public string GetMeasurementType()
        {
            return "Temperature";
        }

        /// <summary>
        /// Retrieves a temperature unit instance based on the provided unit name.
        /// 
        /// This method converts a string into a TemperatureUnit enum value
        /// and returns a new TemperatureMeasurementImpl instance configured with that unit.
        /// 
        /// The parsing is case-insensitive for better usability.
        /// 
        /// Example:
        /// - Input: "Celsius" → Returns TemperatureMeasurementImpl(TemperatureUnit.Celsius)
        /// - Input: "fahrenheit" → Returns TemperatureMeasurementImpl(TemperatureUnit.Fahrenheit)
        /// 
        /// Useful for handling dynamic user input or flexible unit selection.
        /// </summary>
        /// <param name="unitName">The name of the temperature unit.</param>
        /// <returns>An IMeasurable instance for the specified unit.</returns>
        public IMeasurable GetUnitByName(string unitName)
        {
            ThermalScaleRepresentationIdentifier parsedUnit = (ThermalScaleRepresentationIdentifier)Enum.Parse(typeof(ThermalScaleRepresentationIdentifier), unitName, true);
            return new TemperatureMeasurementImpl(parsedUnit);
        }
    }
}