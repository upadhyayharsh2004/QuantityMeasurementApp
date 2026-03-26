using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Interfaces;

namespace QuantityMeasurementAppRepositories.Repositories
{
    public class AdvancedLocalJsonStorageManagerForMeasurementRecords : IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations
    {
        private static AdvancedLocalJsonStorageManagerForMeasurementRecords uniqueStorageManagerInstance;

        private static readonly object synchronizationControlObject = new object();

        private List<ComprehensiveMeasurementOperationDataRecord> inMemoryMeasurementRecordsCollection;

        private readonly string dynamicStorageFileLocation;

        private AdvancedLocalJsonStorageManagerForMeasurementRecords()
        {
            inMemoryMeasurementRecordsCollection = new List<ComprehensiveMeasurementOperationDataRecord>();

            dynamicStorageFileLocation = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "MeasurementStorageData.json");

            LoadExistingRecordsFromPersistentStorage();

            Console.WriteLine("📦 Local JSON storage manager initialized successfully.");
            Console.WriteLine("📁 Storage file path: " + dynamicStorageFileLocation);
        }

        // ========================== INSTANCE ==========================
        public static AdvancedLocalJsonStorageManagerForMeasurementRecords CreateOrRetrieveStorageManagerInstance()
        {
            if (uniqueStorageManagerInstance == null)
            {
                lock (synchronizationControlObject)
                {
                    if (uniqueStorageManagerInstance == null)
                    {
                        uniqueStorageManagerInstance =
                            new AdvancedLocalJsonStorageManagerForMeasurementRecords();
                    }
                }
            }

            return uniqueStorageManagerInstance;
        }

        // ========================== STORE ==========================
        public void SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(ComprehensiveMeasurementOperationDataRecord incomingMeasurementEntityObject)
        {
            lock (synchronizationControlObject)
            {
                inMemoryMeasurementRecordsCollection.Add(incomingMeasurementEntityObject);
            }

            PersistAllRecordsToLocalJsonFile();

            Console.WriteLine("✅ Measurement record stored successfully in memory and disk.");
        }

        // ========================== FETCH ALL ==========================
        public List<ComprehensiveMeasurementOperationDataRecord> RetrieveAllStoredQuantityMeasurementEntitiesFromDataStorage()
        {
            return new List<ComprehensiveMeasurementOperationDataRecord>(inMemoryMeasurementRecordsCollection);
        }

        // ========================== FILTER OPERATION ==========================
        public List<ComprehensiveMeasurementOperationDataRecord> RetrieveAllMeasurementEntitiesBasedOnSpecificOperationType(string operationNameForFiltering)
        {
            List<ComprehensiveMeasurementOperationDataRecord> filteredResultsCollection =
                new List<ComprehensiveMeasurementOperationDataRecord>();

            foreach (var record in inMemoryMeasurementRecordsCollection)
            {
                if (!string.IsNullOrEmpty(record.descriptiveOperationIdentifierName) &&
                    record.descriptiveOperationIdentifierName.Equals(operationNameForFiltering, StringComparison.OrdinalIgnoreCase))
                {
                    filteredResultsCollection.Add(record);
                }
            }

            return filteredResultsCollection;
        }

        // ========================== FILTER TYPE ==========================
        public List<ComprehensiveMeasurementOperationDataRecord> RetrieveAllMeasurementEntitiesBasedOnMeasurementCategoryType(string measurementCategoryType)
        {
            List<ComprehensiveMeasurementOperationDataRecord> filteredResultsCollection =
                new List<ComprehensiveMeasurementOperationDataRecord>();

            foreach (var record in inMemoryMeasurementRecordsCollection)
            {
                if (!string.IsNullOrEmpty(record.measurementCategoryDescriptor) &&
                    record.measurementCategoryDescriptor.Equals(measurementCategoryType, StringComparison.OrdinalIgnoreCase))
                {
                    filteredResultsCollection.Add(record);
                }
            }

            return filteredResultsCollection;
        }

        // ========================== COUNT ==========================
        public int RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage()
        {
            return inMemoryMeasurementRecordsCollection.Count;
        }

        // ========================== DELETE ==========================
        public void DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem()
        {
            inMemoryMeasurementRecordsCollection.Clear();

            PersistAllRecordsToLocalJsonFile();

            Console.WriteLine("🗑️ All measurement records have been cleared successfully.");
        }

        // ========================== STATS ==========================
        public string RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState()
        {
            return "📊 Storage Summary → Mode: Local JSON Storage | Total Records Stored: "
                + inMemoryMeasurementRecordsCollection.Count;
        }

        // ========================== RELEASE ==========================
        public void ReleaseAndCleanupAllResourcesUsedByRepositoryImplementation()
        {
            Console.WriteLine("ℹ️ No external resources to release (local JSON storage only).");
        }

        // ========================== SAVE FILE ==========================
        private void PersistAllRecordsToLocalJsonFile()
        {
            try
            {
                string serializedJsonData =
                    JsonSerializer.Serialize(inMemoryMeasurementRecordsCollection);

                File.WriteAllText(dynamicStorageFileLocation, serializedJsonData);
            }
            catch (Exception storageException)
            {
                Console.WriteLine("⚠️ Failed to persist data to disk: " + storageException.Message);
            }
        }

        // ========================== LOAD FILE ==========================
        private void LoadExistingRecordsFromPersistentStorage()
        {
            try
            {
                if (File.Exists(dynamicStorageFileLocation))
                {
                    string jsonDataFromFile =
                        File.ReadAllText(dynamicStorageFileLocation);

                    var deserializedData =
                        JsonSerializer.Deserialize<List<ComprehensiveMeasurementOperationDataRecord>>(jsonDataFromFile);

                    if (deserializedData != null)
                    {
                        inMemoryMeasurementRecordsCollection = deserializedData;
                    }
                }
            }
            catch (Exception loadingException)
            {
                Console.WriteLine("⚠️ Failed to load existing data. Starting fresh: "
                    + loadingException.Message);

                inMemoryMeasurementRecordsCollection =
                    new List<ComprehensiveMeasurementOperationDataRecord>();
            }
        }

        public static IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations GetInstance()
        {
            throw new NotImplementedException();
        }
    }
}