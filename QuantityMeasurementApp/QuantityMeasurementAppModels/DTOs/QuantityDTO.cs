using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementAppModels.DTOs
{
    /*
     * ============================================================================================
     * This class represents a Data Transfer Object (DTO) for handling quantity-related data.
     * 
     * A DTO is typically used to transfer data between different layers of an application,
     * such as between the backend and frontend, without exposing internal business logic.
     * 
     * In this specific case, the QuantityDTO class is designed to encapsulate three important
     * properties related to a measurable quantity:
     * 
     * 1. Value            -> The numerical magnitude of the quantity (e.g., 10, 5.5, etc.)
     * 2. UnitName         -> The unit in which the value is expressed (e.g., meters, liters)
     * 3. MeasurementType  -> The category of measurement (e.g., length, volume, weight)
     * 
     * This structure ensures clean data handling and helps maintain separation of concerns
     * within the application architecture.
     * ============================================================================================
     */
    public class QuantityDTO
    {

        /*
         * ----------------------------------------------------------------------------------------
         * The following fields are declared as 'readonly', which means:
         * 
         * - Their values can only be assigned once, either at the time of declaration
         *   or inside the constructor.
         * - After initialization, these values cannot be modified, ensuring immutability.
         * 
         * This is important for DTOs because:
         * - It prevents accidental changes to the data after creation.
         * - It makes the object more predictable and safer to use across different layers.
         * ----------------------------------------------------------------------------------------
         */

        // Stores the numerical value of the quantity (e.g., 10, 25.7, etc.)
        public readonly double Value;

        // Stores the name of the unit associated with the value (e.g., "meters", "kg", "liters")
        public readonly string UnitName;

        // Stores the type/category of measurement (e.g., "Length", "Mass", "Volume")
        public readonly string MeasurementType;


        /*
         * ----------------------------------------------------------------------------------------
         * Constructor: QuantityDTO
         * 
         * Purpose:
         * This constructor is used to initialize a new instance of the QuantityDTO class.
         * It ensures that all required fields (Value, UnitName, MeasurementType) are provided
         * at the time of object creation.
         * 
         * Parameters:
         * - value            : The numeric quantity value.
         * - unitName         : The unit in which the value is expressed.
         * - measurementType  : The category/type of measurement.
         * 
         * Why this is important:
         * - Enforces that the object is always created with complete and valid data.
         * - Supports immutability due to readonly fields.
         * ----------------------------------------------------------------------------------------
         */
        public QuantityDTO(double value, string unitName, string measurementType)
        {
            Value = value;
            UnitName = unitName;
            MeasurementType = measurementType;
        }

        /*
         * ----------------------------------------------------------------------------------------
         * Method: ToString()
         * 
         * Purpose:
         * This method overrides the default ToString() method inherited from the base Object class.
         * It provides a human-readable string representation of the QuantityDTO object.
         * 
         * Functionality:
         * - Combines the Value and UnitName into a single formatted string.
         * - Example Output: "10 meters", "5 kg", etc.
         * 
         * Why override ToString():
         * - Makes debugging easier by providing meaningful output.
         * - Useful when logging or displaying the object in UI.
         * 
         * Note:
         * - MeasurementType is not included here, as this method focuses on concise display.
         * ----------------------------------------------------------------------------------------
         */
        public override string ToString()
        {
            return $"{Value} {UnitName}";
        }
    }
}