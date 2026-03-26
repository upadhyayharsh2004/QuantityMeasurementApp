// Importing base system functionalities like Console operations, Exception handling, etc.
using System;

// Importing generic collections such as List<T> to store multiple measurement records
using System.Collections.Generic;

// Importing controller layer which handles communication between UI and service layer
using QuantityMeasurementApp.Controllers;

// Importing interfaces to ensure loose coupling and abstraction
using QuantityMeasurementApp.Interfaces;

// Importing menu implementation which provides console-based UI interaction
using QuantityMeasurementApp.Menu;

// Importing service layer implementation where business logic resides
using QuantityMeasurementApp.Services;

// Importing entity models which represent stored data structure
using QuantityMeasurementAppModels.Entities;

// Importing repository interface for abstraction of data storage
using QuantityMeasurementAppRepositories.Interfaces;

// Importing repository implementations (cache and database)
using QuantityMeasurementAppRepositories.Repositories;

// Importing utility classes like ApplicationConfig and ConnectionPool
using QuantityMeasurementAppRepositories.Utilities;

// Importing service interfaces
using QuantityMeasurementAppServices.Interfaces;

// Defining main namespace of the application
namespace QuantityMeasurementApp
{
    // Main application class responsible for initializing and running the system
    public class QuantityMeasurementApp
    {
        // Static instance variable for Singleton pattern (ensures only one instance exists)
        private static QuantityMeasurementApp instance;

        // Lock object used for thread-safe Singleton initialization
        private static readonly object lockObject = new object();

        // Controller that connects UI (menu) with business logic (service)
        private QuantityMeasurementController controller;

        // Menu interface used for user interaction via console
        private IMenu menu;

        // Repository interface used for storing and retrieving measurement data
        private IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repository;

        // Stores the currently active repository type (Database or Cache)
        private string activeRepositoryType;

        // Private constructor to enforce Singleton pattern (object cannot be created externally)
        private QuantityMeasurementApp()
        {
            // Displaying application startup message
            // This informs the user that the application initialization process has started
            Console.WriteLine("=== Initializing Quantity Measurement Application on the system ===");

            // Creating configuration object to read settings from appsettings.json file
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            // Fetching repository type configuration (either "database" or "cache")
            string repoType = config.RetrieveRepositoryImplementationTypeFromConfiguration();

            // Displaying configured data source type for debugging and verification purposes
            Console.WriteLine("Configured Data Source on the system: " + repoType);

            // Checking if configuration specifies database usage
            if (repoType.Equals("sql", StringComparison.OrdinalIgnoreCase) ||
                repoType.Equals("database", StringComparison.OrdinalIgnoreCase))
            {
                // Attempt to initialize database repository
                // If database is not available, fallback will be handled automatically
                TryInitializeDatabaseRepository(config);
            }
            else
            {
                // Informing user that cache repository will be used instead of database
                // This ensures clarity about system behavior
                Console.WriteLine("Using in-memory cache as the data source on the system (database skipped).");

                // Initialize cache repository
                InitializeCacheRepository();
            }

            // Creating service layer and injecting repository dependency
            // Service layer handles business logic operations
            IQuantityMeasurementService service =
                new QuantityMeasurementServiceImpl(repository);

            // Creating controller and passing service layer
            // Controller acts as mediator between UI and service
            controller = new QuantityMeasurementController(service);

            // Creating menu UI and passing controller
            // Menu will interact with user via console
            menu = new QuantityMenu(controller);

            // Informing user that application setup is complete
            Console.WriteLine("Application setup completed successfully on the system.");

            // Displaying which repository is currently active
            Console.WriteLine("Active Data Source on the system: " + activeRepositoryType);

            // Adding empty line for better console readability
            Console.WriteLine("");
        }

        // Method to initialize database repository with fallback mechanism
        private void TryInitializeDatabaseRepository(ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config)
        {
            // Informing user that system is attempting to connect to SQL Server
            Console.WriteLine("Attempting to establish connection with SQL Server on the system...");

            try
            {
                // Getting connection pool instance using configuration settings
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);

                // Creating database repository using connection pool
                repository = new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                // Setting active repository type
                activeRepositoryType = "Database (SQL Server)";

                // Informing user that database connection was successful
                Console.WriteLine("Connection successful. Using database-backed repository on the system.");
            }
            catch (Exception ex)
            {
                // Printing blank line for better formatting
                Console.WriteLine("");

                // Informing user that SQL Server is not reachable
                Console.WriteLine("WARNING: Unable to connect to SQL Server.");

                // Displaying actual error message for debugging purposes
                Console.WriteLine("Details: " + ex.Message);

                // Informing user that system will switch to fallback mode
                Console.WriteLine("Switching to fallback mode from sql memory to (in-memory cache).");

                // Adding spacing for readability
                Console.WriteLine("");

                // Initialize cache repository as fallback
                InitializeCacheRepository();
            }
        }

        // Method to initialize cache repository
        private void InitializeCacheRepository()
        {
            // Getting singleton instance of cache repository
            repository = AdvancedLocalJsonStorageManagerForMeasurementRecords.CreateOrRetrieveStorageManagerInstance();

            // Setting repository type description
            activeRepositoryType =
                "Cache (offline mode - data stored in JSON file instead of cache Repository)";

            // Informing user that cache repository is ready
            Console.WriteLine("Cache repository initialized successfully on the System.");
        }

        // Singleton method to get application instance
        public static QuantityMeasurementApp GetInstance()
        {
            // Check if instance is not created yet
            if (instance == null)
            {
                // Locking to ensure thread safety
                lock (lockObject)
                {
                    // Double-check to prevent duplicate creation
                    if (instance == null)
                    {
                        instance = new QuantityMeasurementApp();
                    }
                }
            }

            // Returning the single instance of application
            return instance;
        }

        // Method to start application
        public void StartTheEntireQuantityMeasurementApplicationExecutionProcess()
        {
            // Informing user that application is starting main menu
            Console.WriteLine("Launching application menu of QuantityApp...");

            // Running menu loop (user interaction begins here)
            menu.Run();
        }

        // Method to display all stored measurement records
        public void GenerateAndDisplayCompleteMeasurementOperationsReportToUser()
        {
            // Printing header section
            Console.WriteLine("\n========== Measurement History Of Quantity App ==========");

            // Displaying active repository type
            Console.WriteLine("Data Source on the system: " + activeRepositoryType);

            // Fetching all records from repository
            List<ComprehensiveMeasurementOperationDataRecord> all = repository.RetrieveAllStoredQuantityMeasurementEntitiesFromDataStorage();

            // Displaying total number of records
            Console.WriteLine("Total Records Found on the system: " + all.Count);

            // Iterating through all records and printing each one
            for (int i = 0; i < all.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + all[i].ToString());
            }

            // Separator for readability
            Console.WriteLine("-----------------------------------------");

            // Displaying pool or cache statistics
            Console.WriteLine("Resource Info: " + repository.RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState());

            // Closing section
            Console.WriteLine("=========================================\n");
        }

        // Method to delete all measurement records
        public void DeleteAllMeasurements()
        {
            // Informing user that deletion process has started
            Console.WriteLine("Clearing all stored measurement records from the system...");

            // Deleting all records from repository
            repository.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

            // Displaying updated record count
            Console.WriteLine("Operation completed. Remaining records on the system: " + repository.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage());
        }

        // Method to release resources (mainly database connections)
        public void ReleaseAndCleanupAllApplicationResourcesBeforeShutdown()
        {
            // Informing user that resource cleanup is starting
            Console.WriteLine("Releasing system resources of the system...");

            // Calling repository to release resources
            repository.ReleaseAndCleanupAllResourcesUsedByRepositoryImplementation();

            // Confirming resource cleanup completion
            Console.WriteLine("All resources have been successfully released from the system.");
        }
    }
}