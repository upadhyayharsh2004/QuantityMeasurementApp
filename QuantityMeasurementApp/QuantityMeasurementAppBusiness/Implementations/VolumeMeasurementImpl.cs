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
    /// specifically for Volume measurements.
    /// 
    /// It supports different volume units such as Litre, Millilitre, and Gallon.
    /// The class enables:
    /// - Conversion between different volume units
    /// - Standardization using a base unit (Litre)
    /// - Support for arithmetic operations on volume values
    /// 
    /// The base unit used in this implementation is "Litre".
    /// All conversions are performed relative to Litre to ensure consistency.
    /// </summary>
    public class VolumeMeasurementImpl : IMeasurable
    {
        /// <summary>
        /// Stores the current volume unit for this instance.
        /// This determines how conversion operations will be handled.
        /// </summary>
        private VolumeUnit unit;

        /// <summary>
        /// Constructor to initialize the VolumeMeasurementImpl object with a specific unit.
        /// 
        /// Example:
        /// - VolumeMeasurementImpl(VolumeUnit.Litre)
        /// - VolumeMeasurementImpl(VolumeUnit.Millilitre)
        /// 
        /// This sets the unit context for all conversions and operations.
        /// </summary>
        /// <param name="unitType">The volume unit type.</param>
        public VolumeMeasurementImpl(VolumeUnit unitType)
        {
            unit = unitType;
        }

        /// <summary>
        /// Returns the conversion factor of the current unit relative to the base unit (Litre).
        /// 
        /// Conversion references:
        /// - Litre → 1.0 (base unit)
        /// - Millilitre → 0.001 (1 ml = 0.001 litre)
        /// - Gallon → 3.78541 (1 gallon ≈ 3.78541 litres)
        /// 
        /// This factor is used to convert any value into litres for standardized calculations.
        /// </summary>
        /// <returns>The conversion factor relative to Litre.</returns>
        public double GetConversionFactor()
        {
            switch (unit)
            {
                case VolumeUnit.Litre:
                    return 1.0;

                case VolumeUnit.Millilitre:
                    return 0.001;

                case VolumeUnit.Gallon:
                    return 3.78541;

                default:
                    throw new ArgumentException("Invalid Volume Unit");
            }
        }

        /// <summary>
        /// Converts a given value from the current unit into the base unit (Litre).
        /// 
        /// Formula used:
        ///     value_in_litre = value * conversion_factor
        /// 
        /// Example:
        /// - 1000 millilitres → 1000 * 0.001 = 1 litre
        /// - 2 gallons → 2 * 3.78541 = 7.57082 litres
        /// 
        /// This ensures all volume values are normalized into litres for consistent processing.
        /// </summary>
        /// <param name="value">The value in the current unit.</param>
        /// <returns>The equivalent value in litres.</returns>
        public double ConvertToBaseUnit(double value)
        {
            return value * GetConversionFactor();
        }

        /// <summary>
        /// Converts a value from the base unit (Litre) back into the current unit.
        /// 
        /// Formula used:
        ///     value_in_unit = base_value / conversion_factor
        /// 
        /// Example:
        /// - 1 litre → 1 / 0.001 = 1000 millilitres
        /// - 7.57082 litres → 7.57082 / 3.78541 = 2 gallons
        /// 
        /// This method is typically used after performing calculations in the base unit.
        /// </summary>
        /// <param name="baseValue">The value in litres.</param>
        /// <returns>The equivalent value in the current unit.</returns>
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / GetConversionFactor();
        }

        /// <summary>
        /// Returns the name of the current volume unit.
        /// 
        /// Example outputs:
        /// - "Litre"
        /// - "Millilitre"
        /// - "Gallon"
        /// 
        /// This is useful for displaying unit information in user interfaces or logs.
        /// </summary>
        /// <returns>The unit name as a string.</returns>
        public string GetUnitName()
        {
            return unit.ToString();
        }

        /// <summary>
        /// Indicates whether arithmetic operations (such as addition and subtraction)
        /// are supported for volume measurements.
        /// 
        /// Since all volume values can be converted to a common base unit (Litre),
        /// arithmetic operations are valid and meaningful.
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
        /// For volume measurements, all arithmetic operations are supported,
        /// so no validation restrictions are applied.
        /// 
        /// This method exists to fulfill the interface contract and maintain consistency
        /// across different measurement types.
        /// </summary>
        /// <param name="operation">The name of the arithmetic operation.</param>
        public void ValidateOperationSupport(string operation)
        {
            // All arithmetic operations are supported for Volume,
            // so no validation logic is required.
        }

        /// <summary>
        /// Returns the type/category of measurement.
        /// 
        /// This identifies that this implementation is related to Volume measurement.
        /// It can be used for grouping logic or applying specific rules in the application.
        /// 
        /// Example return value:
        /// - "Volume"
        /// </summary>
        /// <returns>The measurement type as a string.</returns>
        public string GetMeasurementType()
        {
            return "Volume";
        }

        /// <summary>
        /// Retrieves a volume unit instance based on the provided unit name.
        /// 
        /// This method converts a string into a VolumeUnit enum value
        /// and returns a new instance of VolumeMeasurementImpl configured with that unit.
        /// 
        /// The parsing is case-insensitive for better flexibility and user experience.
        /// 
        /// Example:
        /// - Input: "Litre" → Returns VolumeMeasurementImpl(VolumeUnit.Litre)
        /// - Input: "millilitre" → Returns VolumeMeasurementImpl(VolumeUnit.Millilitre)
        /// 
        /// This is useful for dynamic unit selection and handling user input.
        /// </summary>
        /// <param name="unitName">The name of the volume unit.</param>
        /// <returns>An IMeasurable instance corresponding to the specified unit.</returns>
        public IMeasurable GetUnitByName(string unitName)
        {
            VolumeUnit parsedUnit = (VolumeUnit)Enum.Parse(typeof(VolumeUnit), unitName, true);
            return new VolumeMeasurementImpl(parsedUnit);
        }
    }
}