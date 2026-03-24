namespace QuantityMeasurementAppModels.Enums
{
    /*
     * ============================================================================================
     * ENUM: WeightUnit
     * 
     * This enumeration defines a set of constants representing different units used
     * for measuring weight (or mass) in the application.
     * 
     * Purpose:
     * --------------------------------------------------------------------------------------------
     * - To standardize weight units across the entire system
     * - To avoid using hardcoded string values like "Kilogram", "Gram", or "Pound"
     * - To ensure type safety and prevent invalid or inconsistent unit usage
     * 
     * Why this enum is important:
     * --------------------------------------------------------------------------------------------
     * - Weight conversions require accurate identification of units
     * - Using enums simplifies logic for conversions, comparisons, and calculations
     * - Enhances code readability and maintainability
     * 
     * Default Internal Values:
     * --------------------------------------------------------------------------------------------
     * Each enum value is automatically assigned an integer starting from 0:
     * - Kilogram = 0
     * - Gram     = 1
     * - Pound    = 2
     * 
     * These units cover both metric and imperial systems of measurement.
     * ============================================================================================
     */
    public enum WeightUnit
    {
        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Kilogram".
         * 
         * Characteristics:
         * - Base unit of mass in the International System of Units (SI)
         * - Widely used worldwide for measuring weight
         * 
         * Conversion Insight:
         * - 1 Kilogram = 1000 Grams
         * 
         * Example:
         * - A person's body weight is typically measured in kilograms
         * ----------------------------------------------------------------------------------------
         */
        Kilogram,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Gram".
         * 
         * Characteristics:
         * - Smaller unit in the metric system
         * - Used for measuring lighter objects or smaller quantities
         * 
         * Conversion Insight:
         * - 1000 Grams = 1 Kilogram
         * 
         * Example:
         * - Food items or ingredients are often measured in grams
         * ----------------------------------------------------------------------------------------
         */
        Gram,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Pound".
         * 
         * Characteristics:
         * - Unit of weight used in the imperial system
         * - Commonly used in countries like the United States
         * 
         * Conversion Insight:
         * - 1 Pound ≈ 0.453592 Kilograms
         * 
         * Example:
         * - Body weight or packaged goods may be measured in pounds
         * 
         * Note:
         * - Useful for supporting international unit conversions
         * ----------------------------------------------------------------------------------------
         */
        Pound
    }
}