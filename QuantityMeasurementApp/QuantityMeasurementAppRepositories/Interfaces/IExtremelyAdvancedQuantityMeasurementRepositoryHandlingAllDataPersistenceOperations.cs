using System.Collections.Generic;
using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppRepositories.Interfaces
{
    /*
     * Interface defining all repository operations for quantity measurement storage
     */
    public interface IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations
    {
        // ========================== SAVE ==========================
        void SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(
            ComprehensiveMeasurementOperationDataRecord quantityMeasurementEntityObjectContainingAllOperationDetails);

        // ========================== GET ALL ==========================
        List<ComprehensiveMeasurementOperationDataRecord> RetrieveAllStoredQuantityMeasurementEntitiesFromDataStorage();

        // ========================== GET BY OPERATION ==========================
        List<ComprehensiveMeasurementOperationDataRecord> RetrieveAllMeasurementEntitiesBasedOnSpecificOperationType(
            string operationTypeNameForFilteringResults);

        // ========================== GET BY MEASUREMENT TYPE ==========================
        List<ComprehensiveMeasurementOperationDataRecord> RetrieveAllMeasurementEntitiesBasedOnMeasurementCategoryType(
            string measurementCategoryTypeForFilteringResults);

        // ========================== COUNT ==========================
        int RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage();

        // ========================== DELETE ==========================
        void DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

        // ========================== STATS ==========================
        string RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState();

        // ========================== RELEASE ==========================
        void ReleaseAndCleanupAllResourcesUsedByRepositoryImplementation();

        List<ComprehensiveMeasurementOperationDataRecord>RetrieveMeasurementEntitiesFilteredByOperationType(string operationType);

        List<ComprehensiveMeasurementOperationDataRecord>RetrieveMeasurementEntitiesFilteredByMeasurementCategoryType(string measurementType);
    }
}