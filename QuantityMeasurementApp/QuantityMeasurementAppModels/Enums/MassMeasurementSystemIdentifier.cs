namespace QuantityMeasurementAppModels.Enums
{
    /*
     * This enumeration defines different mass measurement identifiers
     * used for handling weight-related computations within the system.
     * 
     * It provides a structured and type-safe way to represent various
     * mass units without relying on string literals.
     */
    public enum MassMeasurementSystemIdentifier
    {
        /*
         * Represents a globally accepted standard unit for measuring mass
         * typically used for general-purpose weight measurements.
         */
        GLOBAL_STANDARD_MASS_UNIT,

        /*
         * Represents a smaller subdivision of the standard mass unit
         * commonly used for lightweight or precision measurements.
         */
        MICRO_SCALE_MASS_UNIT,

        /*
         * Represents a non-metric mass unit widely used in certain regions
         * for measuring body weight and packaged goods.
         */
        REGIONAL_IMPERIAL_MASS_UNIT
    }
}