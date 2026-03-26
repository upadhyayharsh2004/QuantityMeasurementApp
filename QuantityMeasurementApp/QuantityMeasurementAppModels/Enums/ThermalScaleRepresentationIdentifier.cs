namespace QuantityMeasurementAppModels.Enums
{
    /*
     * This enumeration defines different thermodynamic scale identifiers
     * used for representing heat intensity levels within the system.
     * 
     * It ensures consistent handling of temperature-related operations
     * without relying on raw string values.
     */
    public enum ThermalScaleRepresentationIdentifier
    {
        /*
         * Represents a commonly used metric-based temperature scale
         * where freezing and boiling points of water are well defined.
         */
        STANDARD_METRIC_HEAT_SCALE,

        /*
         * Represents a region-specific temperature scale
         * primarily used in certain countries for daily weather reporting.
         */
        REGIONAL_HEAT_MEASUREMENT_SCALE,

        /*
         * Represents an absolute thermodynamic scale
         * widely used in scientific and physics-based calculations.
         */
        ABSOLUTE_ENERGY_SCALE_SYSTEM
    }
}