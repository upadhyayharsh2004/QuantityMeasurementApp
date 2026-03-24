namespace QuantityMeasurementAppModels.Enums
{
    /*
     * ============================================================================================
     * ENUM: VolumeUnit
     * 
     * This enumeration defines a fixed set of constants that represent different units
     * used for measuring volume within the application.
     * 
     * Purpose:
     * --------------------------------------------------------------------------------------------
     * - To provide a standardized way of representing volume units
     * - To avoid using hardcoded string values like "Litre", "Millilitre", etc.
     * - To ensure type safety and reduce the possibility of invalid inputs
     * 
     * Importance in the application:
     * --------------------------------------------------------------------------------------------
     * - Volume conversions (e.g., Litre to Millilitre, Gallon to Litre) require consistent
     *   identification of units
     * - This enum helps simplify conversion logic using switch-case or conditional statements
     * - Improves readability and maintainability of the codebase
     * 
     * Default Internal Values:
     * --------------------------------------------------------------------------------------------
     * Each enum value is assigned an integer automatically starting from 0:
     * - Litre       = 0
     * - Millilitre  = 1
     * - Gallon      = 2
     * 
     * These units are commonly used in both metric and imperial systems.
     * ============================================================================================
     */
    public enum VolumeUnit
    {
        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Litre".
         * 
         * Characteristics:
         * - Standard unit of volume in the metric system
         * - Widely used in everyday measurements (liquids like water, milk, fuel, etc.)
         * 
         * Conversion Insight:
         * - 1 Litre = 1000 Millilitres
         * 
         * Example:
         * - A water bottle may contain 1 Litre of water
         * ----------------------------------------------------------------------------------------
         */
        Litre,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Millilitre".
         * 
         * Characteristics:
         * - Smaller unit of volume in the metric system
         * - Used for precise and smaller measurements (medicine, cooking, lab work)
         * 
         * Conversion Insight:
         * - 1000 Millilitres = 1 Litre
         * 
         * Example:
         * - A spoon may hold about 5 Millilitres of liquid
         * ----------------------------------------------------------------------------------------
         */
        Millilitre,

        /*
         * ----------------------------------------------------------------------------------------
         * Represents the unit "Gallon".
         * 
         * Characteristics:
         * - Unit of volume used in the imperial and US customary systems
         * - Commonly used for measuring large quantities of liquid (fuel, water storage)
         * 
         * Conversion Insight:
         * - 1 Gallon ≈ 3.785 Litres (US gallon)
         * 
         * Note:
         * - There are slight differences between US gallon and UK gallon, but typically
         *   US gallon is used in most software applications unless specified otherwise
         * 
         * Example:
         * - Fuel in vehicles is often measured in gallons (in some countries)
         * ----------------------------------------------------------------------------------------
         */
        Gallon
    }
}