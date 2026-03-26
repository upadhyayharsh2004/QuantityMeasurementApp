namespace QuantityMeasurementAppModels.Enums
{
    /*
     * This enumeration defines different categories of mathematical processing actions
     * that can be performed on measurable quantities within the system.
     * 
     * It replaces the need for string-based operation identifiers and ensures
     * safer and more structured control flow across the application.
     */
    public enum MathematicalComputationActionIdentifier
    {
        /*
         * Represents a combining operation where two numerical values
         * are merged to produce a cumulative result.
         */
        COMBINE_VALUES_OPERATION,

        /*
         * Represents a reduction operation where one numerical value
         * is decreased by another to produce a difference.
         */
        DIFFERENCE_CALCULATION_OPERATION,

        /*
         * Represents a proportional evaluation where one value
         * is divided by another to produce a ratio-based result.
         */
        RATIO_EVALUATION_OPERATION
    }
}