using System;
using QuantityMeasurementAppRepositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Services;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Repositories;
using QuantityMeasurementAppRepositories.Utilities;
using QuantityMeasurementAppBusiness.Exceptions;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    [DoNotParallelize]
    public class QuantityMeasurementDBTests
    {
        // -------------------------------------------------------
        // ApplicationConfig Tests For QuantityApp
        // -------------------------------------------------------

        //Test ApplicationConfig loads connection string from appsettings.json
        [TestMethod]
        public void TestDatabaseConfiguration_LoadedFromProperties()
        {
            //create config instance
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            //get connection string
            string connectionString = config.RetrieveDatabaseConnectionStringFromConfiguration();

            //verify connection string is not null or empty
            Assert.IsNotNull(connectionString);
            Assert.AreNotEqual("", connectionString);
            
        }

        //Test ApplicationConfig loads pool size from appsettings.json
        [TestMethod]
        public void TestDatabaseConfiguration_PoolSizeLoaded()
        {
            //create config instance
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            //get pool size
            int poolSize = config.RetrieveMaximumDatabaseConnectionPoolSizeFromConfiguration();

            //verify pool size is greater than zero
            Assert.IsTrue(poolSize > 0);
        }

        //Test ApplicationConfig falls back to default values when key is missing
        [TestMethod]
        public void TestDatabaseConfiguration_FallbackToDefaults()
        {
            //create a temp config file with no Database section
            string tempPath = Path.Combine(Path.GetTempPath(), "test_appsettings.json");
            File.WriteAllText(tempPath, "{ \"Logging\": { \"LogLevel\": \"Information\" } }");

            //create config from temp file
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings(tempPath);

            //verify default pool size returned
            Assert.AreEqual(5, config.RetrieveMaximumDatabaseConnectionPoolSizeFromConfiguration());

            //verify default timeout returned
            Assert.AreEqual(30, config.RetrieveDatabaseConnectionTimeoutDurationFromConfiguration());

            //verify default repo type is cache
            Assert.AreEqual("cache",config.RetrieveRepositoryImplementationTypeFromConfiguration());

            //cleanup temp file
            File.Delete(tempPath);
        }

        //Test ApplicationConfig loads repository type from appsettings.json
        [TestMethod]
        public void TestDatabaseConfiguration_RepositoryTypeLoaded()
        {
            //create config instance
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            //get repository type
            string repoType = config.RetrieveRepositoryImplementationTypeFromConfiguration();

            //verify repository type is database or cache
            bool isValid = repoType.Equals("database", StringComparison.OrdinalIgnoreCase)
                        || repoType.Equals("cache", StringComparison.OrdinalIgnoreCase);

            Assert.IsTrue(isValid);
        }

        // -------------------------------------------------------
        // ConnectionPool Tests
        // -------------------------------------------------------

        //Test ConnectionPool initializes and returns valid statistics
        [TestMethod]
        public void TestConnectionPool_Initialization()
        {
            //create config instance
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //get pool instance
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);

                //get pool statistics
                string stats = pool.RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState();


                //verify statistics are not null or empty
                Assert.IsNotNull(stats);
                Assert.IsTrue(stats.Contains("Total created"));
                Assert.IsTrue(stats.Contains("Idle"));
                Assert.IsTrue(stats.Contains("Max"));
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        //Test connection can be acquired and released back to pool
        [TestMethod]
        public void TestConnectionPool_Acquire_Release()
        {
            //create config instance
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //get pool instance
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);

                //acquire a connection from the pool
                SqlConnection conn = pool.GetConnection();

                //verify connection is open
                Assert.IsNotNull(conn);
                Assert.AreEqual(System.Data.ConnectionState.Open, conn.State);

                //return connection back to pool
                pool.ReturnConnection(conn);

                //verify pool statistics still available after return
                string stats = pool.RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState();
                Assert.IsNotNull(stats);
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        //Test pool statistics format from database repository
        [TestMethod]
        public void TestDatabaseRepositoryPoolStatistics()
        {
            //create config instance
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create pool and repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //get pool statistics from repository
                string stats = pool.RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState();

                //verify statistics contain required fields
                Assert.IsNotNull(stats);
                Assert.IsTrue(stats.Contains("Total created"));
                Assert.IsTrue(stats.Contains("Idle"));
                Assert.IsTrue(stats.Contains("Max"));
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // DatabaseRepository Tests
        // -------------------------------------------------------

        //Test saving an entity to the database
        [TestMethod]
        public void TestDatabaseRepository_SaveEntity()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //create and save entity
                ComprehensiveMeasurementOperationDataRecord entity = new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length");
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(entity);

                //verify count increased to 1
                Assert.AreEqual(1, repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage());

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        //Test retrieving all measurements returns correct number of records
        [TestMethod]
        public void TestDatabaseRepository_RetrieveAllMeasurements()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //save three entities with different operations and types
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Add", 1.0, "Kilogram", 2.0, "Kilogram", 3.0, "Weight"));
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Convert", 1.0, "Litre", 1000.0, "Volume"));

                //retrieve all measurements
                List<ComprehensiveMeasurementOperationDataRecord> all = repo.RetrieveAllStoredQuantityMeasurementEntitiesFromDataStorage();

                //verify correct number returned
                Assert.AreEqual(3, all.Count);

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        //Test querying measurements by operation type
        [TestMethod]
        public void TestDatabaseRepository_QueryByOperation()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //save entities with different operations
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 2.0, "Kilogram", 2.0, "Kilogram", 1.0, "Weight"));
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Add", 1.0, "Litre", 2.0, "Litre", 3.0, "Volume"));

                //query only Compare operations
                List<ComprehensiveMeasurementOperationDataRecord> results =
                    repo.RetrieveMeasurementEntitiesFilteredByOperationType("Compare");

                //verify only 2 Compare records returned
                Assert.AreEqual(2, results.Count);

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        //Test querying measurements by measurement type
        [TestMethod]
        public void TestDatabaseRepository_QueryByMeasurementType()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //save entities with different measurement types
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 2.0, "IMPERIAL_FOOT_BASED_UNIT", 0.0, "Length"));
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Add", 1.0, "Kilogram", 2.0, "Kilogram", 3.0, "Weight"));

                //query only Length measurements
                List<ComprehensiveMeasurementOperationDataRecord> results =
                    repo.RetrieveMeasurementEntitiesFilteredByMeasurementCategoryType("Length");

                //verify only 2 Length records returned
                Assert.AreEqual(2, results.Count);

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        //Test total count matches number of saved entities
        [TestMethod]
        public void TestDatabaseRepository_CountMeasurements()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //save three entities
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Add", 1.0, "Kilogram", 2.0, "Kilogram", 3.0, "Weight"));
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Convert", 1.0, "Litre", 1000.0, "Volume"));

                //verify count matches exactly 3
                Assert.AreEqual(3, repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage());

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        //Test delete all removes every record from database
        [TestMethod]
        public void TestDatabaseRepository_DeleteAll()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear first to start clean
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //save some entities
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Add", 1.0, "Kilogram", 2.0, "Kilogram", 3.0, "Weight"));

                //delete all records
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //verify count is zero
                Assert.AreEqual(0, repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage());
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // SQL Injection Prevention Test
        // -------------------------------------------------------

        //Test parameterized queries prevent SQL injection
        [TestMethod]
        public void TestSQLInjectionPrevention()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //save one normal entity
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));

                //attempt SQL injection in query parameter
                string injectionAttempt =
                    "Compare'; DROP TABLE quantity_measurements; --";

                //query with injection string - should return no results
                List<ComprehensiveMeasurementOperationDataRecord> results =
                    repo.RetrieveMeasurementEntitiesFilteredByOperationType(injectionAttempt);

                //verify injection treated as literal value - no results found
                Assert.AreEqual(0, results.Count);

                //verify original data still exists - table was not dropped
                Assert.AreEqual(1, repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage());

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // DatabaseException Test
        // -------------------------------------------------------

        //Test DatabaseException is thrown when database error occurs
        [TestMethod]
        public void TestDatabaseException_CustomException()
        {
            //create a temp config with invalid connection string
            string tempPath = Path.Combine(
                Path.GetTempPath(), "bad_appsettings.json");
            File.WriteAllText(tempPath,
                "{ \"Database\": { " +
                "\"ConnectionString\": \"Server=invalid;Database=invalid;" +
                "Trusted_Connection=True;TrustServerCertificate=True;\", " +
                "\"PoolSize\": \"5\", \"ConnectionTimeout\": \"1\" }, " +
                "\"Repository\": { \"Type\": \"database\" } }");

            //create config from bad temp file
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings badConfig = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings(tempPath);

            try
            {
                //attempt connection with bad config - should throw exception
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations badPool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations
    .RetrieveSingletonInstanceOfConnectionPoolingManager(badConfig);
                Assert.Fail("Expected exception was not thrown");
            }
            catch (Exception ex)
            {
                //verify exception has a meaningful message
                Assert.IsNotNull(ex.Message);
                Assert.AreNotEqual("", ex.Message);
            }
            finally
            {
                //cleanup temp file
                File.Delete(tempPath);
            }
        }

        // -------------------------------------------------------
        // Repository Factory Tests
        // -------------------------------------------------------

        //Test cache repository is created correctly
        [TestMethod]
        public void TestRepositoryFactory_CreateCacheRepository()
        {
            //create cache repository instance
            AdvancedLocalJsonStorageManagerForMeasurementRecords repo =
                AdvancedLocalJsonStorageManagerForMeasurementRecords.CreateOrRetrieveStorageManagerInstance();

            //verify correct type returned
            Assert.IsInstanceOfType(repo,
                typeof(AdvancedLocalJsonStorageManagerForMeasurementRecords));
        }

        //Test database repository is created correctly
        [TestMethod]
        public void TestRepositoryFactory_CreateDatabaseRepository()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create database repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //verify correct type returned
                Assert.IsInstanceOfType(repo,
                    typeof(AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations));
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // Service Integration Tests
        // -------------------------------------------------------

        //Test service with database repository persists data to database
        [TestMethod]
        public void TestServiceWithDatabaseRepository_Integration()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository and service
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //create service with database repository
                QuantityMeasurementServiceImpl service =
                    new QuantityMeasurementServiceImpl(repo);

                //perform compare operation
                UniversalMeasurementDataCarrierObject first = new UniversalMeasurementDataCarrierObject(1.0, "IMPERIAL_FOOT_BASED_UNIT", "Length");
                UniversalMeasurementDataCarrierObject second = new UniversalMeasurementDataCarrierObject(1.0, "IMPERIAL_FOOT_BASED_UNIT", "Length");
                bool result = service.PerformComparisonOperationBetweenTwoQuantityObjectsAndReturnBooleanResult(first, second);

                //verify result is correct
                Assert.IsTrue(result);

                //verify data was persisted to database
                Assert.AreEqual(1, repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage());

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        //Test service with cache repository stores data in cache
        //DoNotParallelize prevents other cache tests from interfering
        [TestMethod]
        [DoNotParallelize]
        public void TestServiceWithCacheRepository_Integration()
        {
            //get cache repository and clear all data before starting
            AdvancedLocalJsonStorageManagerForMeasurementRecords repo =
                AdvancedLocalJsonStorageManagerForMeasurementRecords.CreateOrRetrieveStorageManagerInstance();
            repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

            //create service with cache repository
            QuantityMeasurementServiceImpl service =
                new QuantityMeasurementServiceImpl(repo);

            //perform compare operation
            UniversalMeasurementDataCarrierObject first = new UniversalMeasurementDataCarrierObject(1.0, "IMPERIAL_FOOT_BASED_UNIT", "Length");
            UniversalMeasurementDataCarrierObject second = new UniversalMeasurementDataCarrierObject(1.0, "IMPERIAL_FOOT_BASED_UNIT", "Length");
            bool result = service.PerformComparisonOperationBetweenTwoQuantityObjectsAndReturnBooleanResult(first, second);

            //verify result is correct
            Assert.IsTrue(result);

            //verify exactly 1 record is in cache
            Assert.AreEqual(1, repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage());

            //cleanup
            repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
        }

        // -------------------------------------------------------
        // Resource Cleanup Tests
        // -------------------------------------------------------

        //Test connection is returned to pool after repository operation
        [TestMethod]
        public void TestResourceCleanup_ConnectionReleasedToPool()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //get stats before operation
                string statsBefore = repo.RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState();


                //perform a save operation
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord("Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));

                //get stats after operation
                string statsAfter = repo.RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState();

                //verify pool stats are same - connection was returned to pool
                Assert.AreEqual(statsBefore, statsAfter);

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        //Test release resources disposes pool correctly
        [TestMethod]
        public void TestResourceCleanup_ReleaseResources()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo = new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //release resources - should not throw any exception
                repo.ReleaseAndCleanupAllResourcesUsedByRepositoryImplementation();

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // Concurrent Access Test
        // -------------------------------------------------------

        //Test cache repository handles multiple threads without data corruption
        //DoNotParallelize prevents other cache tests from interfering
        [TestMethod]
        [DoNotParallelize]
        public void TestCacheRepository_ConcurrentAccess()
        {
            //get cache repository and clear all data before starting
            AdvancedLocalJsonStorageManagerForMeasurementRecords repo = AdvancedLocalJsonStorageManagerForMeasurementRecords.CreateOrRetrieveStorageManagerInstance();
            repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

            //create 5 threads each saving 10 records
            int threadCount = 5;
            int savesPerThread = 10;
            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                int id = i;
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < savesPerThread; j++)
                    {
                        repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                            "Compare", id, "IMPERIAL_FOOT_BASED_UNIT", id, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));
                    }
                });
            }

            //start all threads
            foreach (Thread t in threads) { t.Start(); }

            //wait for all threads to finish
            foreach (Thread t in threads) { t.Join(); }

            //verify all saves completed without data loss
            Assert.AreEqual(threadCount * savesPerThread, repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage());

            //cleanup
            repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
        }

        // -------------------------------------------------------
        // Large Data Set Test
        // -------------------------------------------------------

        //Test cache repository handles 1000 records efficiently
        //DoNotParallelize prevents other cache tests from interfering
        [TestMethod]
        [DoNotParallelize]
        public void TestCacheRepository_LargeDataSet()
        {
            //get cache repository and clear all data before starting
            AdvancedLocalJsonStorageManagerForMeasurementRecords repo =
                AdvancedLocalJsonStorageManagerForMeasurementRecords.CreateOrRetrieveStorageManagerInstance();
            repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

            //save exactly 1000 entities
            for (int i = 0; i < 1000; i++)
            {
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", i, "IMPERIAL_FOOT_BASED_UNIT", i, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));
            }

            //record start time before retrieval
            DateTime start = DateTime.Now;

            //retrieve all 1000 records
            List<ComprehensiveMeasurementOperationDataRecord> all = repo.RetrieveAllStoredQuantityMeasurementEntitiesFromDataStorage();

            //record end time after retrieval
            DateTime end = DateTime.Now;

            //verify exactly 1000 records retrieved
            Assert.AreEqual(1000, all.Count);

            //verify retrieval completed within 2 seconds
            Assert.IsTrue((end - start).TotalSeconds < 2);

            //cleanup
            repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
        }

        // -------------------------------------------------------
        // Package Structure Test
        // -------------------------------------------------------

        //Test all layer namespaces exist in the assembly
        [TestMethod]
        public void TestPackageStructure_AllLayersPresent()
        {
            //load each assembly explicitly by name
            System.Reflection.Assembly appAssembly = System.Reflection.Assembly.Load("QuantityMeasurementApp");
            System.Reflection.Assembly repoAssembly = System.Reflection.Assembly.Load("QuantityMeasurementAppRepositories");
            System.Reflection.Assembly modelAssembly = System.Reflection.Assembly.Load("QuantityMeasurementAppModels");
            System.Reflection.Assembly businessAssembly = System.Reflection.Assembly.Load("QuantityMeasurementAppBusiness");

            //check controller layer exists
            Type controllerType = appAssembly.GetType("QuantityMeasurementApp.Controllers.QuantityMeasurementController");
            Assert.IsNotNull(controllerType, "Controller layer missing");

            //check service layer exists
            System.Reflection.Assembly servicesAssembly = System.Reflection.Assembly.Load("QuantityMeasurementAppServices");
            Type serviceType = servicesAssembly.GetType("QuantityMeasurementApp.Services.QuantityMeasurementServiceImpl");
            Assert.IsNotNull(serviceType, "Service layer missing");

            //check repository layer exists
            Type repoType = repoAssembly.GetTypes().FirstOrDefault(t => t.Name == "AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations");
            Assert.IsNotNull(repoType, "Repository layer missing");

            //check entity layer exists
            Type entityType = modelAssembly.GetType("QuantityMeasurementAppModels.Entities.ComprehensiveMeasurementOperationDataRecord");
            Assert.IsNotNull(entityType, "Entity layer missing");

            //check exception layer exists
            Type exceptionType = businessAssembly.GetType("QuantityMeasurementAppBusiness.Exceptions.DatabaseException");
            Assert.IsNotNull(exceptionType, "Exception layer missing");
        }

        // -------------------------------------------------------
        // Connection Pool Exhausted Test
        // -------------------------------------------------------

        //Test error handling when all connections are exhausted
        [TestMethod]
        public void TestConnectionPool_AllConnectionsExhausted()
        {
            //create config instance
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //get pool instance
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);

                //get max pool size from config
                int maxSize = config.RetrieveMaximumDatabaseConnectionPoolSizeFromConfiguration();

                //acquire all connections from the pool
                List<SqlConnection> connections = new List<SqlConnection>();
                for (int i = 0; i < maxSize; i++)
                {
                    connections.Add(pool.GetConnection());
                }

                //verify all connections are open
                foreach (SqlConnection conn in connections)
                {
                    Assert.AreEqual(System.Data.ConnectionState.Open, conn.State);
                }

                //return all connections back to pool
                foreach (SqlConnection conn in connections)
                {
                    pool.ReturnConnection(conn);
                }

                //verify pool is functional after returning all connections
                string stats = pool.RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState();
                Assert.IsNotNull(stats);
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // Transaction Rollback Test
        // -------------------------------------------------------

        //Test that failed operations do not persist partial data
        [TestMethod]
        public void TestTransactionRollback_OnError()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //get count before attempting bad save
                int countBefore = repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage();

                //attempt to save a null entity - should throw and not persist
                try
                {
                    repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(null);
                }
                catch (Exception)
                {
                    //expected exception - swallow it
                }

                //verify count did not change - nothing was persisted
                int countAfter = repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage();
                Assert.AreEqual(countBefore, countAfter,
                    "Failed operation should not persist data");
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // Database Schema Test
        // -------------------------------------------------------

        //Test that required database table and stored procedures exist
        [TestMethod]
        public void TestDatabaseSchema_TablesCreated()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //get pool instance
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);

                //get a connection to query schema
                SqlConnection conn = pool.GetConnection();

                try
                {
                    //check quantity_measurements table exists
                    SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES " +
                        "WHERE TABLE_NAME = 'quantity_measurements'", conn);

                    int tableCount = (int)cmd.ExecuteScalar();

                    //verify table exists
                    Assert.AreEqual(1, tableCount,
                        "quantity_measurements table should exist");

                    //check stored procedures exist
                    SqlCommand spCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES " +
                        "WHERE ROUTINE_TYPE = 'PROCEDURE' " +
                        "AND ROUTINE_NAME IN (" +
                        "'sp_SaveMeasurement'," +
                        "'sp_GetAllMeasurements'," +
                        "'sp_GetMeasurementsByOperation'," +
                        "'sp_GetMeasurementsByType'," +
                        "'sp_GetTotalCount'," +
                        "'sp_DeleteAllMeasurements')", conn);

                    int spCount = (int)spCmd.ExecuteScalar();

                    //verify all 6 stored procedures exist
                    Assert.AreEqual(6, spCount,
                        "All 6 stored procedures should exist");
                }
                finally
                {
                    //always return connection to pool
                    pool.ReturnConnection(conn);
                }
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // Parameterized Query DateTime Test
        // -------------------------------------------------------

        //Test that saved entities can be retrieved correctly
        [TestMethod]
        public void TestParameterizedQuery_DateTimeHandling()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //save entity with all fields populated
                repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                    "Compare", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "IMPERIAL_FOOT_BASED_UNIT", 1.0, "Length"));

                //retrieve the saved entity
                List<ComprehensiveMeasurementOperationDataRecord> all = repo.RetrieveAllStoredQuantityMeasurementEntitiesFromDataStorage();

                //verify entity was saved and retrieved correctly
                Assert.AreEqual(1, all.Count);
                Assert.AreEqual("Compare", all[0].descriptiveOperationIdentifierName);
                Assert.AreEqual("IMPERIAL_FOOT_BASED_UNIT", all[0].primaryInputUnitDescriptor);
                Assert.AreEqual("Length", all[0].measurementCategoryDescriptor);
                Assert.AreEqual(1.0, all[0].primaryInputNumericValue);
                Assert.AreEqual(1.0, all[0].computedOutputResultValue);

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // Batch Insert Test
        // -------------------------------------------------------

        //Test multiple entities can be saved in succession without pool exhaustion
        [TestMethod]
        public void TestBatchInsert_MultipleEntities()
        {
            //create config and pool
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings config = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings();

            try
            {
                //create repository
                ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations pool = ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations.RetrieveSingletonInstanceOfConnectionPoolingManager(config);
                IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations repo =
                    new AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(pool);

                //clear existing data before test
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();

                //save 20 entities in rapid succession
                int batchSize = 20;
                for (int i = 0; i < batchSize; i++)
                {
                    repo.SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(new ComprehensiveMeasurementOperationDataRecord(
                        "Add", i, "IMPERIAL_FOOT_BASED_UNIT", i + 1.0, "IMPERIAL_FOOT_BASED_UNIT", i + i + 1.0, "Length"));
                }

                //verify all entities saved
                Assert.AreEqual(batchSize, repo.RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage(),
                    "All batch entities should be saved");

                //verify pool is still functional after batch
                string stats = pool.RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState();
                Assert.IsNotNull(stats,
                    "Pool should still be functional after batch insert");

                //cleanup
                repo.DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem();
            }
            catch (Exception ex)
            {
                //mark as inconclusive if database is not available
                Assert.Fail("Database error: " + ex.Message);
            }
        }

        // -------------------------------------------------------
        // Properties Configuration Override Test
        // -------------------------------------------------------

        //Test repository type can be overridden via config file
        [TestMethod]
        public void TestPropertiesConfiguration_EnvironmentOverride()
        {
            //create a temp config file with database type
            //Database section comes AFTER Repository so parser reads Repository:Type correctly
            string tempPathDb = Path.Combine(
                Path.GetTempPath(), "db_appsettings.json");
            File.WriteAllText(tempPathDb,
                "{\n" +
                "  \"Repository\": {\n" +
                "    \"Type\": \"database\"\n" +
                "  },\n" +
                "  \"Database\": {\n" +
                "    \"PoolSize\": 5,\n" +
                "    \"ConnectionTimeout\": 30\n" +
                "  }\n" +
                "}");

            //create a temp config file with cache type
            string tempPathCache = Path.Combine(
                Path.GetTempPath(), "cache_appsettings.json");
            File.WriteAllText(tempPathCache,
                "{\n" +
                "  \"Repository\": {\n" +
                "    \"Type\": \"cache\"\n" +
                "  },\n" +
                "  \"Database\": {\n" +
                "    \"PoolSize\": 5,\n" +
                "    \"ConnectionTimeout\": 30\n" +
                "  }\n" +
                "}");

            //verify database config reads correctly
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings dbConfig = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings(tempPathDb);
            Assert.AreEqual("database", dbConfig.RetrieveRepositoryImplementationTypeFromConfiguration(),
                StringComparer.OrdinalIgnoreCase);

            //verify cache config reads correctly
            ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings cacheConfig = new ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings(tempPathCache);
            Assert.AreEqual("cache", cacheConfig.RetrieveRepositoryImplementationTypeFromConfiguration(),
                StringComparer.OrdinalIgnoreCase);

            //cleanup temp files
            File.Delete(tempPathDb);
            File.Delete(tempPathCache);
        }
    }
}