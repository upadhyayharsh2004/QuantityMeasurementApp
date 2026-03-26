using System;

namespace QuantityMeasurementAppModels.DTOs
{
    /*
     * This class acts as a lightweight container used for transporting measurement data
     * across different layers of the application without exposing internal business logic.
     * 
     * It encapsulates essential attributes required to describe a measurable quantity.
     */
    public class UniversalMeasurementDataCarrierObject
    {
        // Represents the numeric magnitude associated with the measurement
        public readonly double numericalMagnitudeOfMeasurementValue;

        // Represents the unit in which the measurement value is expressed
        public readonly string descriptiveUnitIdentifierName;

        // Represents the classification/category of the measurement (e.g., Length, Volume)
        public readonly string categoricalMeasurementClassificationType;

        /*
         * Constructor responsible for initializing all required properties
         * at the time of object creation to ensure data consistency.
         */
        public UniversalMeasurementDataCarrierObject(
            double incomingNumericalMeasurementValue,
            string incomingUnitDescriptorName,
            string incomingMeasurementCategoryType)
        {
            numericalMagnitudeOfMeasurementValue = incomingNumericalMeasurementValue;
            descriptiveUnitIdentifierName = incomingUnitDescriptorName;
            categoricalMeasurementClassificationType = incomingMeasurementCategoryType;
        }

        /*
         * Provides a formatted string representation of the measurement data
         * which is useful for display, logging, and debugging purposes.
         */
        public override string ToString()
        {
            return $"📏 Measurement Value: {numericalMagnitudeOfMeasurementValue} | Unit: {descriptiveUnitIdentifierName}";
        }
    }
}