using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuantityMeasurementAppRepositories.Exceptions;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppRepositories.Utilities;

namespace QuantityMeasurementAppRepositories.Repositories
{
    /*
     * ============================================================================================
     * CLASS: QuantityMeasurementDatabaseRepository
     * 
     * This class is a concrete implementation of the IQuantityMeasurementRepository interface.
     * It interacts with a SQL Server database using ADO.NET to perform CRUD operations.
     * 
     * Key Responsibilities:
     * --------------------------------------------------------------------------------------------
     * - Save measurement records into the database
     * - Retrieve records using stored procedures
     * - Handle filtering (by operation / measurement type)
     * - Manage database connections using ConnectionPool
     * - Convert database rows into Entity objects
     * - Handle and wrap exceptions using custom DatabaseException
     * 
     * Technologies Used:
     * --------------------------------------------------------------------------------------------
     * - ADO.NET (SqlConnection, SqlCommand, SqlDataReader)
     * - SQL Server Stored Procedures
     * - Custom Connection Pooling
     * ============================================================================================
     */
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        /*
         * ========================================================================================
         * CONNECTION POOL DEPENDENCY
         * 
         * This repository uses a ConnectionPool instead of creating connections directly.
         * This improves performance and ensures efficient resource usage.
         * ========================================================================================
         */
        private ConnectionPool pool;

        /*
         * ========================================================================================
         * CONSTRUCTOR (DEPENDENCY INJECTION)
         * 
         * - Receives ConnectionPool as dependency
         * - Validates input to prevent null reference issues
         * ========================================================================================
         */
        public QuantityMeasurementDatabaseRepository(ConnectionPool connectionPool)
        {
            if (connectionPool == null)
            {
                throw new ArgumentException("ConnectionPool cannot be null");
            }

            pool = connectionPool;
            Console.WriteLine("[DatabaseRepository] Initialized with ADO.NET + SQL Server.");
        }

        /*
         * ========================================================================================
         * METHOD: Save
         * 
         * Saves a QuantityMeasurementEntity into the database using a stored procedure.
         * 
         * Steps:
         * ----------------------------------------------------------------------------------------
         * 1. Validate input
         * 2. Get connection from pool
         * 3. Create SqlCommand with stored procedure
         * 4. Add parameters safely
         * 5. Execute command
         * 6. Handle exceptions and return connection
         * ========================================================================================
         */
        public void Save(QuantityMeasurementEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Entity cannot be null");
            }

            SqlConnection conn = pool.GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_SaveMeasurement", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                /*
                 * Parameter mapping:
                 * Each property of the entity is mapped to a SQL parameter
                 * DBNull.Value is used for null values to avoid SQL errors
                 */

                SqlParameter opParam = new SqlParameter("@operation", SqlDbType.NVarChar, 50);
                opParam.Value = entity.Operation;
                cmd.Parameters.Add(opParam);

                SqlParameter fvParam = new SqlParameter("@first_value", SqlDbType.Float);
                fvParam.Value = entity.FirstValue;
                cmd.Parameters.Add(fvParam);

                SqlParameter fuParam = new SqlParameter("@first_unit", SqlDbType.NVarChar, 50);
                fuParam.Value = (object)entity.FirstUnit ?? DBNull.Value;
                cmd.Parameters.Add(fuParam);

                SqlParameter svParam = new SqlParameter("@second_value", SqlDbType.Float);
                svParam.Value = entity.SecondValue;
                cmd.Parameters.Add(svParam);

                SqlParameter suParam = new SqlParameter("@second_unit", SqlDbType.NVarChar, 50);
                suParam.Value = (object)entity.SecondUnit ?? DBNull.Value;
                cmd.Parameters.Add(suParam);

                SqlParameter rvParam = new SqlParameter("@result_value", SqlDbType.Float);
                rvParam.Value = entity.ResultValue;
                cmd.Parameters.Add(rvParam);

                SqlParameter mtParam = new SqlParameter("@measurement_type", SqlDbType.NVarChar, 50);
                mtParam.Value = (object)entity.MeasurementType ?? DBNull.Value;
                cmd.Parameters.Add(mtParam);

                SqlParameter ieParam = new SqlParameter("@is_error", SqlDbType.Bit);
                ieParam.Value = entity.IsError;
                cmd.Parameters.Add(ieParam);

                SqlParameter emParam = new SqlParameter("@error_message", SqlDbType.NVarChar, 500);
                emParam.Value = (object)entity.ErrorMessage ?? DBNull.Value;
                cmd.Parameters.Add(emParam);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

                Console.WriteLine("[DatabaseRepository] Saved: " + entity.Operation);
            }
            catch (SqlException ex)
            {
                // Wrap SQL-specific exception
                throw new DatabaseException(
                    "Save failed (SQL error " + ex.Number + "): " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Wrap general exception
                throw new DatabaseException("Save failed: " + ex.Message, ex);
            }
            finally
            {
                // Always return connection to pool
                pool.ReturnConnection(conn);
            }
        }

        /*
         * ========================================================================================
         * METHOD: GetAll
         * 
         * Retrieves all measurement records from database.
         * ========================================================================================
         */
        public List<QuantityMeasurementEntity> GetAll()
        {
            SqlConnection conn = pool.GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllMeasurements", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                List<QuantityMeasurementEntity> results = ReadEntities(cmd);
                cmd.Dispose();

                return results;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("GetAll failed: " + ex.Message, ex);
            }
            finally
            {
                pool.ReturnConnection(conn);
            }
        }

        /*
         * ========================================================================================
         * METHOD: GetByOperation
         * 
         * Retrieves records filtered by operation type.
         * ========================================================================================
         */
        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            if (operation == null)
            {
                throw new ArgumentException("Operation cannot be null");
            }

            SqlConnection conn = pool.GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_GetMeasurementsByOperation", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@operation", SqlDbType.NVarChar, 50) { Value = operation });

                List<QuantityMeasurementEntity> results = ReadEntities(cmd);
                cmd.Dispose();

                return results;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("GetByOperation failed: " + ex.Message, ex);
            }
            finally
            {
                pool.ReturnConnection(conn);
            }
        }

        /*
         * ========================================================================================
         * METHOD: GetByMeasurementType
         * ========================================================================================
         */
        public List<QuantityMeasurementEntity> GetByMeasurementType(string measurementType)
        {
            if (measurementType == null)
            {
                throw new ArgumentException("MeasurementType cannot be null");
            }

            SqlConnection conn = pool.GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_GetMeasurementsByType", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@measurement_type", SqlDbType.NVarChar, 50) { Value = measurementType });

                List<QuantityMeasurementEntity> results = ReadEntities(cmd);
                cmd.Dispose();

                return results;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("GetByMeasurementType failed: " + ex.Message, ex);
            }
            finally
            {
                pool.ReturnConnection(conn);
            }
        }

        /*
         * ========================================================================================
         * METHOD: GetTotalCount
         * ========================================================================================
         */
        public int GetTotalCount()
        {
            SqlConnection conn = pool.GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_GetTotalCount", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                int count = 0;
                if (reader.Read())
                {
                    count = reader.GetInt32(reader.GetOrdinal("TotalCount"));
                }

                reader.Close();
                cmd.Dispose();

                return count;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("GetTotalCount failed: " + ex.Message, ex);
            }
            finally
            {
                pool.ReturnConnection(conn);
            }
        }

        /*
         * ========================================================================================
         * METHOD: DeleteAll
         * ========================================================================================
         */
        public void DeleteAll()
        {
            SqlConnection conn = pool.GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteAllMeasurements", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
                cmd.Dispose();

                Console.WriteLine("[DatabaseRepository] All measurements deleted.");
            }
            catch (Exception ex)
            {
                throw new DatabaseException("DeleteAll failed: " + ex.Message, ex);
            }
            finally
            {
                pool.ReturnConnection(conn);
            }
        }

        /*
         * ========================================================================================
         * POOL + RESOURCE MANAGEMENT
         * ========================================================================================
         */
        public string GetPoolStatistics()
        {
            return pool.GetPoolStatistics();
        }

        public void ReleaseResources()
        {
            pool.Dispose();
        }

        /*
         * ========================================================================================
         * METHOD: ReadEntities (CORE MAPPING LOGIC)
         * 
         * Converts database rows into QuantityMeasurementEntity objects.
         * Handles:
         * - Error records
         * - Two-operand operations
         * - Single-operand conversions
         * ========================================================================================
         */
        private List<QuantityMeasurementEntity> ReadEntities(SqlCommand cmd)
        {
            List<QuantityMeasurementEntity> list = new List<QuantityMeasurementEntity>();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string operation = reader.GetString(reader.GetOrdinal("operation"));
                bool isError = reader.GetBoolean(reader.GetOrdinal("is_error"));

                QuantityMeasurementEntity entity;

                if (isError)
                {
                    string errorMsg = "";
                    int errCol = reader.GetOrdinal("error_message");

                    if (!reader.IsDBNull(errCol))
                    {
                        errorMsg = reader.GetString(errCol);
                    }

                    entity = new QuantityMeasurementEntity(operation, errorMsg);
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

                    if (secondUnit != "")
                    {
                        entity = new QuantityMeasurementEntity(
                            operation,
                            firstValue, firstUnit,
                            secondValue, secondUnit,
                            resultValue,
                            measurementType);
                    }
                    else
                    {
                        entity = new QuantityMeasurementEntity(
                            operation,
                            firstValue, firstUnit,
                            resultValue,
                            measurementType);
                    }
                }

                list.Add(entity);
            }

            reader.Close();
            return list;
        }
    }
}