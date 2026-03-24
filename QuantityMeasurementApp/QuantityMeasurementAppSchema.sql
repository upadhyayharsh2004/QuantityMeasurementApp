/*
============================================================================================
DATABASE SETUP: QuantityMeasurementAppDB

This script is responsible for:
1. Creating the database
2. Creating the main table to store measurement operations
3. Defining stored procedures for CRUD and query operations

This database acts as the persistent storage layer for the application.
============================================================================================
*/

-- Step 0: Create and select the database
CREATE DATABASE QuantityMeasurementAppDB;

USE QuantityMeasurementAppDB;


/*
============================================================================================
STEP 1: TABLE CREATION - quantity_measurements

This table stores all operations performed in the application such as:
- Add
- Subtract
- Divide
- Compare
- Convert

Each row represents a single operation performed by the user.
============================================================================================
*/
CREATE TABLE quantity_measurements
(
    /*
     * Primary Key:
     * - Auto-incrementing unique identifier for each record
     */
    id               INT           IDENTITY(1,1) PRIMARY KEY,

    /*
     * Operation performed (e.g., Add, Subtract, Convert)
     */
    operation        NVARCHAR(50)  NOT NULL,

    /*
     * First operand details
     * Default value = 0 ensures no NULL values
     */
    first_value      FLOAT         NOT NULL DEFAULT 0,
    first_unit       NVARCHAR(50)  NULL,

    /*
     * Second operand details (used for binary operations)
     * Can be NULL for single-operand operations like Convert
     */
    second_value     FLOAT         NOT NULL DEFAULT 0,
    second_unit      NVARCHAR(50)  NULL,

    /*
     * Result of the operation
     */
    result_value     FLOAT         NOT NULL DEFAULT 0,

    /*
     * Measurement category (Length, Weight, Volume, Temperature)
     */
    measurement_type NVARCHAR(50)  NULL,

    /*
     * Error handling fields:
     * - is_error = 1 means operation failed
     * - error_message stores details of failure
     */
    is_error         BIT           NOT NULL DEFAULT 0,
    error_message    NVARCHAR(500) NULL,

    /*
     * Timestamp of when the record was created
     * Automatically set to current date/time
     */
    created_at       DATETIME      NOT NULL DEFAULT GETDATE()
);
GO


/*
============================================================================================
STEP 2: STORED PROCEDURE - sp_SaveMeasurement

Purpose:
- Inserts a new measurement record into the table

Why use a stored procedure?
--------------------------------------------------------------------------------------------
- Improves performance (precompiled execution)
- Enhances security (avoids SQL injection)
- Centralizes database logic
============================================================================================
*/
CREATE PROCEDURE sp_SaveMeasurement
    @operation        NVARCHAR(50),
    @first_value      FLOAT,
    @first_unit       NVARCHAR(50),
    @second_value     FLOAT,
    @second_unit      NVARCHAR(50),
    @result_value     FLOAT,
    @measurement_type NVARCHAR(50),
    @is_error         BIT,
    @error_message    NVARCHAR(500)
AS
BEGIN
    INSERT INTO quantity_measurements
    (
        operation,
        first_value,
        first_unit,
        second_value,
        second_unit,
        result_value,
        measurement_type,
        is_error,
        error_message
    )
    VALUES
    (
        @operation,
        @first_value,
        @first_unit,
        @second_value,
        @second_unit,
        @result_value,
        @measurement_type,
        @is_error,
        @error_message
    );
END
GO


/*
============================================================================================
STEP 3: STORED PROCEDURE - sp_GetAllMeasurements

Purpose:
- Retrieves all records from the table

Behavior:
- Orders results by created_at in descending order
- Ensures newest operations appear first
============================================================================================
*/
CREATE PROCEDURE sp_GetAllMeasurements
AS
BEGIN
    SELECT
        id,
        operation,
        first_value,
        first_unit,
        second_value,
        second_unit,
        result_value,
        measurement_type,
        is_error,
        error_message,
        created_at
    FROM quantity_measurements
    ORDER BY created_at DESC;
END
GO


/*
============================================================================================
STEP 4: STORED PROCEDURE - sp_GetMeasurementsByOperation

Purpose:
- Retrieves records filtered by operation type

Example:
- Get all "Add" operations
- Get all "Convert" operations
============================================================================================
*/
CREATE PROCEDURE sp_GetMeasurementsByOperation
    @operation NVARCHAR(50)
AS
BEGIN
    SELECT
        id,
        operation,
        first_value,
        first_unit,
        second_value,
        second_unit,
        result_value,
        measurement_type,
        is_error,
        error_message,
        created_at
    FROM quantity_measurements
    WHERE operation = @operation
    ORDER BY created_at DESC;
END
GO


/*
============================================================================================
STEP 5: STORED PROCEDURE - sp_GetMeasurementsByType

Purpose:
- Retrieves records filtered by measurement type

Example:
- Get all "Length" operations
- Get all "Temperature" conversions
============================================================================================
*/
CREATE PROCEDURE sp_GetMeasurementsByType
    @measurement_type NVARCHAR(50)
AS
BEGIN
    SELECT
        id,
        operation,
        first_value,
        first_unit,
        second_value,
        second_unit,
        result_value,
        measurement_type,
        is_error,
        error_message,
        created_at
    FROM quantity_measurements
    WHERE measurement_type = @measurement_type
    ORDER BY created_at DESC;
END
GO


/*
============================================================================================
STEP 6: STORED PROCEDURE - sp_GetTotalCount

Purpose:
- Returns total number of records in the table

Use Cases:
- Dashboard statistics
- Monitoring application usage
============================================================================================
*/
CREATE PROCEDURE sp_GetTotalCount
AS
BEGIN
    SELECT COUNT(*) AS TotalCount
    FROM quantity_measurements;
END
GO


/*
============================================================================================
STEP 7: STORED PROCEDURE - sp_DeleteAllMeasurements

Purpose:
- Deletes all records from the table

IMPORTANT:
--------------------------------------------------------------------------------------------
- This operation is irreversible
- Should be used carefully (e.g., for testing or reset)
============================================================================================
*/
CREATE PROCEDURE sp_DeleteAllMeasurements
AS
BEGIN
    DELETE FROM quantity_measurements;
END
GO