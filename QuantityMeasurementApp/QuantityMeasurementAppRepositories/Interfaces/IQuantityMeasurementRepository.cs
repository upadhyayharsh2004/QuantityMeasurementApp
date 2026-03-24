using System.Collections.Generic;
using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppRepositories.Interfaces
{
    /*
     * ============================================================================================
     * INTERFACE: IQuantityMeasurementRepository
     * 
     * This interface defines the contract for a repository that handles storage and retrieval
     * of QuantityMeasurementEntity objects.
     * 
     * What is a Repository?
     * --------------------------------------------------------------------------------------------
     * - A repository is a design pattern used to abstract data access logic.
     * - It acts as a bridge between the application and the data source (e.g., database, file).
     * - It ensures that business logic does not directly interact with the database layer.
     * 
     * Why use an interface here?
     * --------------------------------------------------------------------------------------------
     * - Promotes loose coupling between layers (Service layer does not depend on implementation)
     * - Makes the system more flexible and testable (e.g., mock repository for unit testing)
     * - Allows multiple implementations (e.g., File-based, Database-based, In-memory)
     * 
     * This interface defines all operations that can be performed on quantity measurement data.
     * Any class implementing this interface MUST provide concrete implementations for all methods.
     * ============================================================================================
     */
    public interface IQuantityMeasurementRepository
    {
        /*
         * ========================================================================================
         * UC15 ORIGINAL METHODS
         * 
         * These methods represent the basic functionality for storing and retrieving data.
         * ========================================================================================
         */

        /*
         * ----------------------------------------------------------------------------------------
         * METHOD: Save
         * 
         * Purpose:
         * - Stores a QuantityMeasurementEntity object into the data source.
         * 
         * Parameter:
         * - entity : The measurement entity containing operation details, values, units, etc.
         * 
         * Behavior:
         * - The implementation may save data into a file, database, or memory collection.
         * - Typically used after performing an operation (e.g., Add, Convert).
         * 
         * Example:
         * - Save(new QuantityMeasurementEntity(...))
         * ----------------------------------------------------------------------------------------
         */
        void Save(QuantityMeasurementEntity entity);

        /*
         * ----------------------------------------------------------------------------------------
         * METHOD: GetAll
         * 
         * Purpose:
         * - Retrieves all stored quantity measurement records.
         * 
         * Return Type:
         * - List<QuantityMeasurementEntity> : A list containing all saved entities.
         * 
         * Behavior:
         * - Returns complete history of operations performed.
         * - Useful for displaying logs or reports.
         * ----------------------------------------------------------------------------------------
         */
        List<QuantityMeasurementEntity> GetAll();


        /*
         * ========================================================================================
         * UC16 METHODS (ADVANCED FUNCTIONALITIES)
         * 
         * These methods extend the repository functionality by adding filtering,
         * analytics, and resource management capabilities.
         * ========================================================================================
         */

        /*
         * ----------------------------------------------------------------------------------------
         * METHOD: GetByOperation
         * 
         * Purpose:
         * - Retrieves all records that match a specific operation type.
         * 
         * Parameter:
         * - operation : The name of the operation (e.g., "Add", "Convert")
         * 
         * Return:
         * - A filtered list of entities that match the given operation
         * 
         * Use Case:
         * - To view all addition operations or all conversion operations
         * ----------------------------------------------------------------------------------------
         */
        List<QuantityMeasurementEntity> GetByOperation(string operation);

        /*
         * ----------------------------------------------------------------------------------------
         * METHOD: GetByMeasurementType
         * 
         * Purpose:
         * - Retrieves records based on the type of measurement (e.g., Length, Volume, Weight)
         * 
         * Parameter:
         * - measurementType : The category of measurement
         * 
         * Return:
         * - A filtered list of entities matching the specified measurement type
         * 
         * Use Case:
         * - To analyze only length-related operations or temperature conversions
         * ----------------------------------------------------------------------------------------
         */
        List<QuantityMeasurementEntity> GetByMeasurementType(string measurementType);

        /*
         * ----------------------------------------------------------------------------------------
         * METHOD: GetTotalCount
         * 
         * Purpose:
         * - Returns the total number of stored measurement records.
         * 
         * Return:
         * - int : Total count of entities
         * 
         * Use Case:
         * - Useful for statistics, dashboards, or monitoring usage
         * ----------------------------------------------------------------------------------------
         */
        int GetTotalCount();

        /*
         * ----------------------------------------------------------------------------------------
         * METHOD: DeleteAll
         * 
         * Purpose:
         * - Removes all stored measurement records from the data source.
         * 
         * Behavior:
         * - Clears the entire dataset
         * - Should be used carefully as it deletes all historical data
         * 
         * Use Case:
         * - Resetting the system or clearing test data
         * ----------------------------------------------------------------------------------------
         */
        void DeleteAll();

        /*
         * ----------------------------------------------------------------------------------------
         * METHOD: GetPoolStatistics
         * 
         * Purpose:
         * - Provides information about the internal state or usage statistics of the repository.
         * 
         * Return:
         * - string : A formatted summary of statistics
         * 
         * Possible Information:
         * - Number of records stored
         * - Memory usage
         * - Resource pool status
         * 
         * Use Case:
         * - Monitoring performance or debugging
         * ----------------------------------------------------------------------------------------
         */
        string GetPoolStatistics();

        /*
         * ----------------------------------------------------------------------------------------
         * METHOD: ReleaseResources
         * 
         * Purpose:
         * - Frees up any resources being used by the repository.
         * 
         * Behavior:
         * - May close file streams, database connections, or clear memory
         * 
         * Importance:
         * - Prevents memory leaks
         * - Ensures efficient resource management
         * 
         * Use Case:
         * - Called when the application is shutting down or repository is no longer needed
         * ----------------------------------------------------------------------------------------
         */
        void ReleaseResources();
    }
}