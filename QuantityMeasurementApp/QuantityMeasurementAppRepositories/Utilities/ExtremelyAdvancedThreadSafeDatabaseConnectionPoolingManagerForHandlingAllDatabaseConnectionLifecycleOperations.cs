using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QuantityMeasurementAppRepositories.Utilities
{
    public class ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations : IDisposable
    {
        private static ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations extremelyImportantSingletonInstanceOfConnectionPoolingManager;


        private static readonly object extremelyCriticalSynchronizationLockObjectForThreadSafety = new object();


        private Stack<SqlConnection> extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure;

        private int extremelyImportantCounterTrackingTotalNumberOfConnectionsCreated;

        private int extremelyImportantMaximumLimitForDatabaseConnectionPoolSize;

        private string extremelyImportantDatabaseConnectionStringUsedForCreatingConnections;

        private ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations(
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings configurationManagerObject)
        {
            extremelyImportantDatabaseConnectionStringUsedForCreatingConnections =
                configurationManagerObject.RetrieveDatabaseConnectionStringFromConfiguration();

            extremelyImportantMaximumLimitForDatabaseConnectionPoolSize =
                configurationManagerObject.RetrieveMaximumDatabaseConnectionPoolSizeFromConfiguration();

            extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure =
                new Stack<SqlConnection>();

            extremelyImportantCounterTrackingTotalNumberOfConnectionsCreated = 0;

            Console.WriteLine("🚀 Starting initialization of advanced database connection pool manager...");
            Console.WriteLine("📌 Maximum allowed connections in pool: " + extremelyImportantMaximumLimitForDatabaseConnectionPoolSize);

            int initialNumberOfConnectionsToPreCreate =
                Math.Min(2, extremelyImportantMaximumLimitForDatabaseConnectionPoolSize);

            for (int initializationLoopCounter = 0;
                 initializationLoopCounter < initialNumberOfConnectionsToPreCreate;
                 initializationLoopCounter++)
            {
                SqlConnection newlyCreatedConnectionInstance =
                    CreateAndOpenNewDatabaseConnectionInstanceWithProperTracking();

                extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure
                    .Push(newlyCreatedConnectionInstance);
            }

            Console.WriteLine("✅ Connection pool initialization completed successfully.");
            Console.WriteLine("📊 Pre-created connections available for immediate use: "
                + initialNumberOfConnectionsToPreCreate);
        }

        // ========================== GET INSTANCE ==========================
        public static ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations
            RetrieveSingletonInstanceOfConnectionPoolingManager(
                ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings configurationManagerObject)
        {
            if (extremelyImportantSingletonInstanceOfConnectionPoolingManager == null)
            {
                lock (extremelyCriticalSynchronizationLockObjectForThreadSafety)
                {
                    if (extremelyImportantSingletonInstanceOfConnectionPoolingManager == null)
                    {
                        extremelyImportantSingletonInstanceOfConnectionPoolingManager =
                            new ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations(configurationManagerObject);
                    }
                }
            }

            return extremelyImportantSingletonInstanceOfConnectionPoolingManager;
        }

        // ========================== CREATE CONNECTION ==========================
        private SqlConnection CreateAndOpenNewDatabaseConnectionInstanceWithProperTracking()
        {
            SqlConnection newlyCreatedSqlConnectionObject =
                new SqlConnection(extremelyImportantDatabaseConnectionStringUsedForCreatingConnections);

            newlyCreatedSqlConnectionObject.Open();

            extremelyImportantCounterTrackingTotalNumberOfConnectionsCreated++;

            Console.WriteLine("➕ New database connection created successfully. Total connections created so far: "
                + extremelyImportantCounterTrackingTotalNumberOfConnectionsCreated);

            return newlyCreatedSqlConnectionObject;
        }

        // ========================== GET CONNECTION ==========================
        public SqlConnection RetrieveAvailableConnectionFromPoolOrCreateNewOneIfRequired()
        {
            lock (extremelyCriticalSynchronizationLockObjectForThreadSafety)
            {
                if (extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure.Count > 0)
                {
                    SqlConnection reusedConnectionObject =
                        extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure.Pop();

                    if (reusedConnectionObject.State != ConnectionState.Open)
                    {
                        reusedConnectionObject.Open();
                    }

                    Console.WriteLine("🔄 Reusing existing connection from pool.");
                    return reusedConnectionObject;
                }

                if (extremelyImportantCounterTrackingTotalNumberOfConnectionsCreated <
                    extremelyImportantMaximumLimitForDatabaseConnectionPoolSize)
                {
                    Console.WriteLine("➕ Pool has capacity. Creating new connection...");
                    return CreateAndOpenNewDatabaseConnectionInstanceWithProperTracking();
                }

                Console.WriteLine("⏳ All connections are currently in use. Waiting briefly before retrying...");
                System.Threading.Thread.Sleep(500);

                if (extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure.Count > 0)
                {
                    Console.WriteLine("🔁 Connection became available after waiting. Reusing it.");
                    return extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure.Pop();
                }

                Console.WriteLine("⚠️ Creating emergency connection beyond configured pool limit.");
                return CreateAndOpenNewDatabaseConnectionInstanceWithProperTracking();
            }
        }

        // ========================== RETURN CONNECTION ==========================
        public void ReturnDatabaseConnectionBackToPoolAfterUsage(SqlConnection databaseConnectionObjectToBeReturned)
        {
            if (databaseConnectionObjectToBeReturned == null)
            {
                Console.WriteLine("⚠️ Attempted to return a null connection. Ignoring.");
                return;
            }

            lock (extremelyCriticalSynchronizationLockObjectForThreadSafety)
            {
                if (extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure.Count <
                    extremelyImportantMaximumLimitForDatabaseConnectionPoolSize &&
                    databaseConnectionObjectToBeReturned.State == ConnectionState.Open)
                {
                    extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure
                        .Push(databaseConnectionObjectToBeReturned);

                    Console.WriteLine("✅ Connection returned to pool successfully.");
                }
                else
                {
                    Console.WriteLine("❌ Connection cannot be reused. Closing and disposing it.");
                    databaseConnectionObjectToBeReturned.Close();
                    databaseConnectionObjectToBeReturned.Dispose();
                }
            }
        }

        // ========================== STATS ==========================
        public string RetrieveDetailedConnectionPoolStatisticsWithReadableFormattedMessage()
        {
            return "📊 Connection Pool Status => Total Created: "
                + extremelyImportantCounterTrackingTotalNumberOfConnectionsCreated
                + " | Currently Idle: "
                + extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure.Count
                + " | Maximum Allowed: "
                + extremelyImportantMaximumLimitForDatabaseConnectionPoolSize;
        }

        // ========================== DISPOSE ==========================
        public void Dispose()
        {
            lock (extremelyCriticalSynchronizationLockObjectForThreadSafety)
            {
                Console.WriteLine("🧹 Initiating cleanup of all database connections...");

                while (extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure.Count > 0)
                {
                    SqlConnection connectionToDispose =
                        extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure.Pop();

                    connectionToDispose.Close();
                    connectionToDispose.Dispose();
                }

                Console.WriteLine("✅ All database connections have been successfully released and disposed.");
            }
        }

        // ========================== RESET ==========================
        public static void ResetConnectionPoolManagerInstanceForTestingOrReinitialization()
        {
            lock (extremelyCriticalSynchronizationLockObjectForThreadSafety)
            {
                if (extremelyImportantSingletonInstanceOfConnectionPoolingManager != null)
                {
                    Console.WriteLine("🔄 Resetting connection pool manager instance...");

                    extremelyImportantSingletonInstanceOfConnectionPoolingManager.Dispose();
                    extremelyImportantSingletonInstanceOfConnectionPoolingManager = null;

                    Console.WriteLine("✅ Connection pool manager reset completed.");
                }
            }
        }


        public static ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations GetInstance(ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config)
        {
            return RetrieveSingletonInstanceOfConnectionPoolingManager(config);
        }
        public void ReturnConnection(object connection)
        {
            // do nothing
        }

        public string RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState()
        {
            return $"Total created: {extremelyImportantCounterTrackingTotalNumberOfConnectionsCreated}, " +
                   $"Idle: {extremelyImportantCollectionOfReusableDatabaseConnectionsStoredInStackStructure.Count}, " +
                   $"Max: {extremelyImportantMaximumLimitForDatabaseConnectionPoolSize}";
        }
        public SqlConnection GetConnection()
        {
            return RetrieveAvailableConnectionFromPoolOrCreateNewOneIfRequired();
        }

        public void ReturnConnection(SqlConnection connection)
        {
            ReturnDatabaseConnectionBackToPoolAfterUsage(connection);
        }
    }
}