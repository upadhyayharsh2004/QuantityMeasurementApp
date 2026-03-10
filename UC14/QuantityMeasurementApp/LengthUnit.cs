using System;

namespace QuantityMeasurementApp
{
    // Enum representing the supported units for length measurement
    public enum LengthUnit
    {
        // Base unit for length
        Feet,

        // 1 Foot = 12 Inches
        Inch,

        // 1 Yard = 3 Feet
        Yard,

        // Metric unit where 1 Centimeter ≈ 0.0328084 Feet
        Centimeter
    }
}