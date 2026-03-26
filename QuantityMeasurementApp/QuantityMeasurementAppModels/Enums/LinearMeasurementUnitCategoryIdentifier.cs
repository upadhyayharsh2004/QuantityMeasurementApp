namespace QuantityMeasurementAppModels.Enums
{
    /*
     * This enumeration represents a collection of supported linear measurement units
     * used within the application for handling distance-based calculations.
     * 
     * It ensures consistent usage of unit identifiers and avoids reliance on raw strings.
     */
    public enum LinearMeasurementUnitCategoryIdentifier
    {
        /*
         * Represents a standard unit used in imperial measurement systems
         * typically for measuring medium-sized distances such as room dimensions.
         */
        IMPERIAL_FOOT_BASED_UNIT,

        /*
         * Represents a smaller subdivision of imperial measurement
         * commonly used for precise or fine-grained measurements.
         */
        IMPERIAL_SMALL_SCALE_UNIT,

        /*
         * Represents a larger-scale imperial unit often used in outdoor
         * measurements such as sports fields or land distances.
         */
        IMPERIAL_EXTENDED_RANGE_UNIT,

        /*
         * Represents a metric-based unit commonly used worldwide
         * for scientific and everyday measurements.
         */
        METRIC_STANDARD_SUBDIVISION_UNIT
    }
}