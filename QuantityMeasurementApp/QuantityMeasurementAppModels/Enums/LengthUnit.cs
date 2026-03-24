namespace QuantityMeasurementAppModels.Enums
{
    /*
     * ============================================================================================
     * ENUM: LengthUnit
     * 
     * This enumeration defines a fixed set of constants that represent different units
     * of length measurement used within the application.
     * 
     * Purpose of this enum:
     * --------------------------------------------------------------------------------------------
     * - To standardize all length-related units in one place
     * - To avoid the use of hardcoded strings like "Feet", "Inch", etc.
     * - To improve code readability, maintainability, and type safety
     * 
     * Why enums are useful here:
     * --------------------------------------------------------------------------------------------
     * - Prevents invalid unit values from being used
     * - Makes switch-case or conditional logic cleaner
     * - Helps developers clearly understand which units are supported
     * 
     * Default Behavior:
     * --------------------------------------------------------------------------------------------
     * Each enum value is internally assigned an integer starting from 0:
     * - Feet        = 0
     * - Inch        = 1
     * - Yard        = 2
     * - Centimeter  = 3
     * 
     * These units can be used in conversion logic, calculations, and validations
     * within the quantity measurement system.
     * ============================================================================================
     */
    public enum LengthUnit
    {
        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Feet".
         * 
         * Common Usage:
         * - Widely used in the imperial system
         * - Often used for measuring height, room dimensions, etc.
         * 
         * Example:
         * - 5 Feet (height of a person)
         * ----------------------------------------------------------------------------------------
         */
        Feet,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Inch".
         * 
         * Common Usage:
         * - Smaller unit in the imperial system
         * - Frequently used for measuring small lengths such as screen sizes,
         *   furniture dimensions, etc.
         * 
         * Example:
         * - 12 Inches = 1 Foot
         * ----------------------------------------------------------------------------------------
         */
        Inch,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Yard".
         * 
         * Common Usage:
         * - Larger unit in the imperial system
         * - Commonly used in fields, sports measurements, and fabric measurements
         * 
         * Example:
         * - 1 Yard = 3 Feet
         * ----------------------------------------------------------------------------------------
         */
        Yard,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Centimeter".
         * 
         * Common Usage:
         * - Part of the metric system
         * - Widely used in scientific measurements and daily applications
         * 
         * Example:
         * - 100 Centimeters = 1 Meter
         * 
         * Note:
         * - This allows interoperability between metric and imperial systems
         * ----------------------------------------------------------------------------------------
         */
        Centimeter
        
    }
}