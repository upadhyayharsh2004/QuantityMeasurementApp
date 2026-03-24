using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Interfaces;

namespace QuantityMeasurementAppRepositories.Repositories
{
    /*
     * ============================================================================================
     * CLASS: QuantityMeasurementCacheRepository
     * 
     * This class is a concrete implementation of the IQuantityMeasurementRepository interface.
     * It uses an in-memory list (cache) to store quantity measurement records and persists
     * the data to a JSON file on disk.
     * 
     * Key Responsibilities:
     * --------------------------------------------------------------------------------------------
     * - Store measurement records in memory for fast access
     * - Persist data to disk using JSON serialization
     * - Provide filtering and analytics methods (UC16 requirements)
     * - Ensure thread safety during concurrent access
     * - Implement Singleton pattern to maintain a single shared instance
     * 
     * UC16 Enhancements:
     * --------------------------------------------------------------------------------------------
     * - Removed hardcoded file path and replaced it with dynamic path resolution
     * - Implemented advanced repository methods like filtering, statistics, and cleanup
     * ============================================================================================
     */
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        /*
         * ========================================================================================
         * SINGLETON PATTERN IMPLEMENTATION
         * 
         * This ensures that only one instance of the repository exists throughout the application.
         * 
         * Why Singleton?
         * ----------------------------------------------------------------------------------------
         * - Prevents multiple copies of the cache
         * - Ensures consistency of stored data
         * - Saves memory and avoids duplication
         * ========================================================================================
         */

        // Holds the single instance of the repository
        private static QuantityMeasurementCacheRepository instance;

        // Lock object used to ensure thread-safe initialization and operations
        private static readonly object lockObject = new object();

        /*
         * ========================================================================================
         * DATA STORAGE
         * ========================================================================================
         */

        // In-memory list that stores all quantity measurement records (acts as cache)
        private List<QuantityMeasurementEntity> history;

        /*
         * ========================================================================================
         * FILE STORAGE CONFIGURATION
         * 
         * The file path is dynamically generated using the application's base directory.
         * This avoids hardcoding paths and improves portability across environments.
         * ========================================================================================
         */
        private static readonly string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "QuantityMeasurement.json");

        /*
         * ========================================================================================
         * CONSTRUCTOR (PRIVATE - REQUIRED FOR SINGLETON)
         * 
         * - Initializes the in-memory list
         * - Loads existing data from disk (if available)
         * - Logs initialization details
         * ========================================================================================
         */
        private QuantityMeasurementCacheRepository()
        {
            history = new List<QuantityMeasurementEntity>();
            LoadFromDisk();
            Console.WriteLine("[CacheRepository] Initialized. File: " + filePath);
        }

        /*
         * ========================================================================================
         * METHOD: GetInstance()
         * 
         * Provides global access to the single instance of the repository.
         * Uses "double-checked locking" for thread-safe lazy initialization.
         * 
         * Steps:
         * ----------------------------------------------------------------------------------------
         * 1. Check if instance is null
         * 2. Lock the critical section
         * 3. Check again to avoid duplicate creation
         * 4. Create instance if still null
         * ========================================================================================
         */
        public static QuantityMeasurementCacheRepository GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new QuantityMeasurementCacheRepository();
                    }
                }
            }
            return instance;
        }

        /*
         * ========================================================================================
         * METHOD: Save
         * 
         * Adds a new measurement entity to the in-memory cache and persists it to disk.
         * 
         * Thread Safety:
         * ----------------------------------------------------------------------------------------
         * - Only the list modification (Add) is locked to avoid race conditions
         * - File saving is done outside the lock to reduce blocking
         * ========================================================================================
         */
        public void Save(QuantityMeasurementEntity entity)
        {
            lock (lockObject)
            {
                history.Add(entity);
            }

            SaveToDisk();
        }

        /*
         * ========================================================================================
         * METHOD: GetAll
         * 
         * Returns a copy of all stored measurement records.
         * 
         * Important:
         * ----------------------------------------------------------------------------------------
         * - Returns a new list instead of the original list
         * - Prevents external modification of internal data (encapsulation)
         * ========================================================================================
         */
        public List<QuantityMeasurementEntity> GetAll()
        {
            return new List<QuantityMeasurementEntity>(history);
        }

        /*
         * ========================================================================================
         * METHOD: GetByOperation
         * 
         * Filters and returns all records matching a specific operation type.
         * 
         * Comparison:
         * ----------------------------------------------------------------------------------------
         * - Case-insensitive comparison using StringComparison.OrdinalIgnoreCase
         * ========================================================================================
         */
        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            List<QuantityMeasurementEntity> result = new List<QuantityMeasurementEntity>();

            foreach (QuantityMeasurementEntity entity in history)
            {
                if (entity.Operation != null && entity.Operation.Equals(operation, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(entity);
                }
            }
            return result;
        }

        /*
         * ========================================================================================
         * METHOD: GetByMeasurementType
         * 
         * Filters and returns records based on measurement category (Length, Volume, etc.)
         * ========================================================================================
         */
        public List<QuantityMeasurementEntity> GetByMeasurementType(string measurementType)
        {
            List<QuantityMeasurementEntity> result = new List<QuantityMeasurementEntity>();

            foreach (QuantityMeasurementEntity entity in history)
            {
                if (entity.MeasurementType != null && entity.MeasurementType.Equals(measurementType, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(entity);
                }
            }
            return result;
        }

        /*
         * ========================================================================================
         * METHOD: GetTotalCount
         * 
         * Returns the total number of records currently stored in memory.
         * ========================================================================================
         */
        public int GetTotalCount()
        {
            return history.Count;
        }

        /*
         * ========================================================================================
         * METHOD: DeleteAll
         * 
         * Clears all records from memory and updates the disk file accordingly.
         * ========================================================================================
         */
        public void DeleteAll()
        {
            history.Clear();
            SaveToDisk();
            Console.WriteLine("[CacheRepository] All measurements deleted.");
        }

        /*
         * ========================================================================================
         * METHOD: GetPoolStatistics
         * 
         * Since this is an in-memory repository (not a database), there is no connection pool.
         * This method returns a simple summary of stored records.
         * ========================================================================================
         */
        public string GetPoolStatistics()
        {
            return "In-memory cache. No connection pool. Records: " + history.Count;
        }

        /*
         * ========================================================================================
         * METHOD: ReleaseResources
         * 
         * No external resources (like DB connections) are used here,
         * so this method simply logs a message.
         * ========================================================================================
         */
        public void ReleaseResources()
        {
            Console.WriteLine("[CacheRepository] No resources to release.");
        }

        /*
         * ========================================================================================
         * METHOD: SaveToDisk (PRIVATE)
         * 
         * Serializes the in-memory list into JSON format and writes it to a file.
         * 
         * Error Handling:
         * ----------------------------------------------------------------------------------------
         * - Any exception during file writing is caught and logged
         * ========================================================================================
         */
        private void SaveToDisk()
        {
            try
            {
                string json = JsonSerializer.Serialize(history);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CacheRepository] Warning: could not save: " + ex.Message);
            }
        }

        /*
         * ========================================================================================
         * METHOD: LoadFromDisk (PRIVATE)
         * 
         * Reads the JSON file from disk and deserializes it into the in-memory list.
         * 
         * Behavior:
         * ----------------------------------------------------------------------------------------
         * - If file exists → load data
         * - If error occurs → initialize empty list
         * ========================================================================================
         */
        private void LoadFromDisk()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    history = JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CacheRepository] Warning: could not load: " + ex.Message);
                history = new List<QuantityMeasurementEntity>();
            }
        }
    }
}