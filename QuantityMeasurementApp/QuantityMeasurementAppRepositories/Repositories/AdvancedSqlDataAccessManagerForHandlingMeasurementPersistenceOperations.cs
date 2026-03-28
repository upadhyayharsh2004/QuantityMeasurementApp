using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppRepositories.Exceptions;
using QuantityMeasurementAppRepositories.Utilities;

namespace QuantityMeasurementAppRepositories.Repositories
{
    public class AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations : IExtremelyAdvancedQuantityMeasurementRepositoryHandlingAllDataPersistenceOperations
    {
        private readonly ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations
            databaseConnectionLifecycleManagerInstance;

        public AdvancedSqlDataAccessManagerForHandlingMeasurementPersistenceOperations(
            ExtremelyAdvancedThreadSafeDatabaseConnectionPoolingManagerForHandlingAllDatabaseConnectionLifecycleOperations incomingConnectionManagerDependency)
        {
            if (incomingConnectionManagerDependency == null)
            {
                throw new ArgumentException("❌ Critical error: Connection manager dependency cannot be null.");
            }

            databaseConnectionLifecycleManagerInstance = incomingConnectionManagerDependency;

            Console.WriteLine("🚀 SQL Data Access Manager initialized successfully using advanced connection pooling.");
        }

        // ========================== INSERT ==========================
        public void SaveQuantityMeasurementEntityIntoUnderlyingDataStorageSystem(ComprehensiveMeasurementOperationDataRecord incomingMeasurementEntityObject)
        {
            if (incomingMeasurementEntityObject == null)
            {
                throw new ArgumentException("❌ Cannot process null measurement entity.");
            }

            SqlConnection activeDatabaseConnection =
                databaseConnectionLifecycleManagerInstance.RetrieveAvailableConnectionFromPoolOrCreateNewOneIfRequired();

            try
            {
                SqlCommand databaseCommandObject =
                    new SqlCommand("sp_SaveMeasurement", activeDatabaseConnection);

                databaseCommandObject.CommandType = CommandType.StoredProcedure;

                databaseCommandObject.Parameters.Add(new SqlParameter("@operation", incomingMeasurementEntityObject.descriptiveOperationIdentifierName));
                databaseCommandObject.Parameters.Add(new SqlParameter("@first_value", incomingMeasurementEntityObject.primaryInputNumericValue));
                databaseCommandObject.Parameters.Add(new SqlParameter("@first_unit", (object)incomingMeasurementEntityObject.primaryInputUnitDescriptor ?? DBNull.Value));
                databaseCommandObject.Parameters.Add(new SqlParameter("@second_value", incomingMeasurementEntityObject.secondaryInputNumericValue));
                databaseCommandObject.Parameters.Add(new SqlParameter("@second_unit", (object)incomingMeasurementEntityObject.secondaryInputUnitDescriptor ?? DBNull.Value));
                databaseCommandObject.Parameters.Add(new SqlParameter("@result_value", incomingMeasurementEntityObject.computedOutputResultValue));
                databaseCommandObject.Parameters.Add(new SqlParameter("@measurement_type", (object)incomingMeasurementEntityObject.measurementCategoryDescriptor ?? DBNull.Value));
                databaseCommandObject.Parameters.Add(new SqlParameter("@is_error", incomingMeasurementEntityObject.isOperationMarkedAsError));
                databaseCommandObject.Parameters.Add(new SqlParameter("@error_message", (object)incomingMeasurementEntityObject.detailedErrorDescriptionMessage ?? DBNull.Value));

                databaseCommandObject.ExecuteNonQuery();
                databaseCommandObject.Dispose();

                Console.WriteLine("✅ Measurement data successfully inserted into database for operation: "
                    + incomingMeasurementEntityObject.descriptiveOperationIdentifierName);
            }
            catch (SqlException sqlSpecificException)
            {
                throw new ExtremelyCriticalDatabaseOperationFailureException(
                    "❌ SQL execution failure while inserting measurement data. Code: "
                    + sqlSpecificException.Number + " | Message: " + sqlSpecificException.Message,
                    sqlSpecificException);
            }
            catch (Exception generalDatabaseException)
            {
                throw new ExtremelyCriticalDatabaseOperationFailureException(
                    "❌ Unexpected error occurred during database insert operation: "
                    + generalDatabaseException.Message,
                    generalDatabaseException);
            }
            finally
            {
                databaseConnectionLifecycleManagerInstance
                    .ReturnDatabaseConnectionBackToPoolAfterUsage(activeDatabaseConnection);
            }
        }
        // ===== FIX FOR INTERFACE METHODS =====

        public List<ComprehensiveMeasurementOperationDataRecord>
            RetrieveMeasurementEntitiesFilteredByOperationType(string operationType)
        {
            return RetrieveAllMeasurementEntitiesBasedOnSpecificOperationType(operationType);
        }

        public List<ComprehensiveMeasurementOperationDataRecord>
            RetrieveMeasurementEntitiesFilteredByMeasurementCategoryType(string measurementType)
        {
            return RetrieveAllMeasurementEntitiesBasedOnMeasurementCategoryType(measurementType);
        }

        // ========================== FETCH ALL ==========================
        public List<ComprehensiveMeasurementOperationDataRecord> RetrieveAllStoredQuantityMeasurementEntitiesFromDataStorage()
        {
            SqlConnection activeConnection =
                databaseConnectionLifecycleManagerInstance.RetrieveAvailableConnectionFromPoolOrCreateNewOneIfRequired();

            try
            {
                SqlCommand command = new SqlCommand("sp_GetAllMeasurements", activeConnection);
                command.CommandType = CommandType.StoredProcedure;

                List<ComprehensiveMeasurementOperationDataRecord> retrievedRecords =
                    ConvertDatabaseRowsToEntityObjects(command);

                command.Dispose();

                Console.WriteLine("📥 Successfully retrieved all measurement records from database.");

                return retrievedRecords;
            }
            catch (Exception ex)
            {
                throw new ExtremelyCriticalDatabaseOperationFailureException("❌ Failed to retrieve all records: " + ex.Message, ex);
            }
            finally
            {
                databaseConnectionLifecycleManagerInstance
                    .ReturnDatabaseConnectionBackToPoolAfterUsage(activeConnection);
            }
        }

        // ========================== FILTER OPERATION ==========================
        public List<ComprehensiveMeasurementOperationDataRecord> RetrieveAllMeasurementEntitiesBasedOnSpecificOperationType(string operationName)
        {
            if (operationName == null)
            {
                throw new ArgumentException("❌ Operation filter value cannot be null.");
            }

            SqlConnection activeConnection =
                databaseConnectionLifecycleManagerInstance.RetrieveAvailableConnectionFromPoolOrCreateNewOneIfRequired();

            try
            {
                SqlCommand command =
                    new SqlCommand("sp_GetMeasurementsByOperation", activeConnection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@operation", operationName));

                return ConvertDatabaseRowsToEntityObjects(command);
            }
            catch (Exception ex)
            {
                throw new ExtremelyCriticalDatabaseOperationFailureException("❌ Failed to filter by operation: " + ex.Message, ex);
            }
            finally
            {
                databaseConnectionLifecycleManagerInstance
                    .ReturnDatabaseConnectionBackToPoolAfterUsage(activeConnection);
            }
        }

        // ========================== FILTER TYPE ==========================
        public List<ComprehensiveMeasurementOperationDataRecord> RetrieveAllMeasurementEntitiesBasedOnMeasurementCategoryType(string measurementCategory)
        {
            if (measurementCategory == null)
            {
                throw new ArgumentException("❌ Measurement type cannot be null.");
            }

            SqlConnection activeConnection =
                databaseConnectionLifecycleManagerInstance.RetrieveAvailableConnectionFromPoolOrCreateNewOneIfRequired();

            try
            {
                SqlCommand command =
                    new SqlCommand("sp_GetMeasurementsByType", activeConnection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@measurement_type", measurementCategory));

                return ConvertDatabaseRowsToEntityObjects(command);
            }
            catch (Exception ex)
            {
                throw new ExtremelyCriticalDatabaseOperationFailureException("❌ Failed to filter by measurement type: " + ex.Message, ex);
            }
            finally
            {
                databaseConnectionLifecycleManagerInstance
                    .ReturnDatabaseConnectionBackToPoolAfterUsage(activeConnection);
            }
        }

        // ========================== COUNT ==========================
        public int RetrieveTotalCountOfAllStoredMeasurementEntitiesFromDataStorage()
        {
            SqlConnection activeConnection =
                databaseConnectionLifecycleManagerInstance.RetrieveAvailableConnectionFromPoolOrCreateNewOneIfRequired();

            try
            {
                SqlCommand command = new SqlCommand("sp_GetTotalCount", activeConnection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                int totalRecordsCount = 0;

                if (reader.Read())
                {
                    totalRecordsCount = reader.GetInt32(reader.GetOrdinal("TotalCount"));
                }

                reader.Close();
                command.Dispose();

                Console.WriteLine("📊 Total measurement records count retrieved: " + totalRecordsCount);

                return totalRecordsCount;
            }
            catch (Exception ex)
            {
                throw new ExtremelyCriticalDatabaseOperationFailureException("❌ Failed to retrieve record count: " + ex.Message, ex);
            }
            finally
            {
                databaseConnectionLifecycleManagerInstance
                    .ReturnDatabaseConnectionBackToPoolAfterUsage(activeConnection);
            }
        }

        // ========================== DELETE ==========================
        public void DeleteAllStoredMeasurementEntitiesFromUnderlyingDataStorageSystem()
        {
            SqlConnection activeConnection =
                databaseConnectionLifecycleManagerInstance.RetrieveAvailableConnectionFromPoolOrCreateNewOneIfRequired();

            try
            {
                SqlCommand command =
                    new SqlCommand("sp_DeleteAllMeasurements", activeConnection);

                command.CommandType = CommandType.StoredProcedure;

                command.ExecuteNonQuery();
                command.Dispose();

                Console.WriteLine("🗑️ All database measurement records have been deleted successfully.");
            }
            catch (Exception ex)
            {
                throw new ExtremelyCriticalDatabaseOperationFailureException("❌ Failed to delete records: " + ex.Message, ex);
            }
            finally
            {
                databaseConnectionLifecycleManagerInstance
                    .ReturnDatabaseConnectionBackToPoolAfterUsage(activeConnection);
            }
        }

        // ========================== STATS ==========================
        public string RetrieveDetailedStatisticsInformationAboutRepositoryResourceUsageAndStorageState()
        {
            return "📡 Database Connection Pool Info → "
                + databaseConnectionLifecycleManagerInstance
                    .RetrieveDetailedConnectionPoolStatisticsWithReadableFormattedMessage();
        }

        public void ReleaseAndCleanupAllResourcesUsedByRepositoryImplementation()
        {
            Console.WriteLine("🧹 Releasing all database-related resources...");
            databaseConnectionLifecycleManagerInstance.Dispose();
        }

        // ========================== INTERNAL MAPPER ==========================
        private List<ComprehensiveMeasurementOperationDataRecord> ConvertDatabaseRowsToEntityObjects(SqlCommand command)
        {
            List<ComprehensiveMeasurementOperationDataRecord> entityCollection = new List<ComprehensiveMeasurementOperationDataRecord>();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string operationName = reader.GetString(reader.GetOrdinal("operation"));
                bool isError = reader.GetBoolean(reader.GetOrdinal("is_error"));

                ComprehensiveMeasurementOperationDataRecord entity;

                if (isError)
                {
                    string errorMessage = reader.IsDBNull(reader.GetOrdinal("error_message"))
                        ? ""
                        : reader.GetString(reader.GetOrdinal("error_message"));

                    entity = new ComprehensiveMeasurementOperationDataRecord(operationName, errorMessage);
                }
                else
                {
                    double firstValue = reader.GetDouble(reader.GetOrdinal("first_value"));
                    double secondValue = reader.GetDouble(reader.GetOrdinal("second_value"));
                    double resultValue = reader.GetDouble(reader.GetOrdinal("result_value"));

                    string firstUnit = reader.IsDBNull(reader.GetOrdinal("first_unit")) ? "" :
                        reader.GetString(reader.GetOrdinal("first_unit"));

                    string secondUnit = reader.IsDBNull(reader.GetOrdinal("second_unit")) ? "" :
                        reader.GetString(reader.GetOrdinal("second_unit"));

                    string measurementType = reader.IsDBNull(reader.GetOrdinal("measurement_type")) ? "" :
                        reader.GetString(reader.GetOrdinal("measurement_type"));

                    if (!string.IsNullOrEmpty(secondUnit))
                    {
                        entity = new ComprehensiveMeasurementOperationDataRecord(
                            operationName,
                            firstValue, firstUnit,
                            secondValue, secondUnit,
                            resultValue,
                            measurementType);
                    }
                    else
                    {
                        entity = new ComprehensiveMeasurementOperationDataRecord(
                            operationName,
                            firstValue, firstUnit,
                            resultValue,
                            measurementType);
                    }
                }

                entityCollection.Add(entity);
            }

            reader.Close();
            return entityCollection;
        }
    }
}