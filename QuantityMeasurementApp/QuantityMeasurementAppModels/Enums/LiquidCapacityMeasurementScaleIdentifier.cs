namespace QuantityMeasurementAppModels.Enums
{
    /*
     * This enumeration represents different liquid capacity measurement categories
     * used within the system for handling volume-related calculations.
     * 
     * It ensures a structured and type-safe way to deal with various fluid measurement units.
     */
    public enum LiquidCapacityMeasurementScaleIdentifier
    {
        /*
         * Represents a standard metric-based liquid measurement unit
         * commonly used for medium-scale volume quantities.
         */
        METRIC_BASE_VOLUME_UNIT,

        /*
         * Represents a smaller subdivision of metric liquid measurement
         * typically used for precision and smaller quantities.
         */
        METRIC_MICRO_VOLUME_UNIT,

        /*
         * Represents a larger-scale volume measurement unit
         * generally used in non-metric systems for bulk liquid quantities.
         */
        NON_METRIC_LARGE_CAPACITY_UNIT
    }
}