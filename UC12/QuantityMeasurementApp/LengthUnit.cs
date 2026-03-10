using System;

namespace QuantityMeasurementApp
{
    // Enum representing different units of length
    public enum LengthUnit
    {
        // Base unit for length measurement
        Feet,

        // 1 Foot = 12 Inches
        Inch,

        // 1 Yard = 3 Feet
        Yard,

        // Metric unit where 1 Centimeter ≈ 0.0328084 Feet
        Centimeter
    }
}