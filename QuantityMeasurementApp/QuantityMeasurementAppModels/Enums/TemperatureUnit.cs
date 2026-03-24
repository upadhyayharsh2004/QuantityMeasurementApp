namespace QuantityMeasurementAppModels.Enums
{
    /*
     * ============================================================================================
     * ENUM: TemperatureUnit
     * 
     * This enumeration defines a set of constants representing different units of temperature
     * measurement supported by the application.
     * 
     * Purpose:
     * --------------------------------------------------------------------------------------------
     * - To standardize temperature units across the system
     * - To eliminate the use of hardcoded strings like "Celsius", "Fahrenheit", etc.
     * - To ensure type safety and reduce the chances of invalid or misspelled units
     * 
     * Why this enum is important:
     * --------------------------------------------------------------------------------------------
     * - Temperature conversions require precise identification of units
     * - Using enums makes conversion logic cleaner (especially in switch-case statements)
     * - Improves readability and maintainability of the codebase
     * 
     * Default Internal Values:
     * --------------------------------------------------------------------------------------------
     * Each enum value is automatically assigned an integer starting from 0:
     * - Celsius     = 0
     * - Fahrenheit  = 1
     * - Kelvin      = 2
     * 
     * These units are widely used in scientific, industrial, and everyday applications.
     * ============================================================================================
     */
    public enum TemperatureUnit
    {
        /*
         * ----------------------------------------------------------------------------------------
         * Represents the Celsius temperature scale.
         * 
         * Characteristics:
         * - Part of the metric system
         * - Water freezes at 0°C and boils at 100°C (under standard conditions)
         * - Commonly used in most countries for daily temperature measurement
         * 
         * Example:
         * - Normal room temperature ≈ 25°C
         * ----------------------------------------------------------------------------------------
         */
        Celsius,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the Fahrenheit temperature scale.
         * 
         * Characteristics:
         * - Commonly used in countries like the United States
         * - Water freezes at 32°F and boils at 212°F
         * 
         * Conversion Insight:
         * - °F = (°C × 9/5) + 32
         * 
         * Example:
         * - Normal room temperature ≈ 77°F
         * ----------------------------------------------------------------------------------------
         */
        Fahrenheit,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the Kelvin temperature scale.
         * 
         * Characteristics:
         * - SI (International System of Units) base unit for temperature
         * - Absolute scale (no negative values)
         * - 0 K represents absolute zero (the lowest possible temperature)
         * 
         * Conversion Insight:
         * - K = °C + 273.15
         * 
         * Usage:
         * - Widely used in scientific and thermodynamic calculations
         * 
         * Example:
         * - Room temperature ≈ 298 K
         * ----------------------------------------------------------------------------------------
         */
        Kelvin
    }
}