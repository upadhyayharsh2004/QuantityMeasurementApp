using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QuantityMeasurementAppRepositories.Utilities
{
    /*
     * ============================================================================================
     * CLASS: ConnectionPool
     * 
     * This class implements a custom connection pooling mechanism for managing SQL Server
     * database connections efficiently.
     * 
     * Purpose:
     * --------------------------------------------------------------------------------------------
     * - Reuse database connections instead of creating new ones repeatedly
     * - Improve application performance by reducing connection creation overhead
     * - Limit the number of active connections using a configurable pool size
     * 
     * Key Concepts:
     * --------------------------------------------------------------------------------------------
     * - Connection Pooling: Reusing existing database connections
     * - Singleton Pattern: Only one pool exists across the application
     * - Thread Safety: Ensures safe access in multi-threaded environments
     * 
     * How it works:
     * --------------------------------------------------------------------------------------------
     * 1. A fixed number of connections are maintained in a pool (Stack)
     * 2. When needed → a connection is borrowed
     * 3. After use → it is returned to the pool
     * 4. If pool is empty → new connection is created (within limit)
     * ============================================================================================
     */
    public class ConnectionPool : IDisposable
    {
        /*
         * ========================================================================================
         * SINGLETON IMPLEMENTATION
         * 
         * Ensures only one instance of ConnectionPool exists.
         * This is important because:
         * - Multiple pools would waste resources
         * - Connections should be centrally managed
         * ========================================================================================
         */

        // Holds the single instance of the pool
        private static ConnectionPool instance;

        // Lock object used for thread-safe operations
        private static readonly object lockObject = new object();

        /*
         * ========================================================================================
         * INTERNAL STATE VARIABLES
         * ========================================================================================
         */

        // Stack used to store available (idle) connections
        private Stack<SqlConnection> availableConnections;

        // Tracks total number of connections created so far
        private int totalCreated;

        // Maximum number of connections allowed in the pool
        private int maxPoolSize;

        // Database connection string
        private string connectionString;

        /*
         * ========================================================================================
         * CONSTRUCTOR (PRIVATE - USED BY SINGLETON)
         * 
         * Initializes the connection pool using configuration values.
         * Also pre-creates a small number of connections for faster startup.
         * ========================================================================================
         */
        private ConnectionPool(ApplicationConfig config)
        {
            connectionString = config.GetConnectionString();
            maxPoolSize = config.GetMaxPoolSize();
            availableConnections = new Stack<SqlConnection>();
            totalCreated = 0;

            Console.WriteLine("[ConnectionPool] Initializing. Max size: " + maxPoolSize);

            /*
             * Pre-create a few connections (up to 2 or maxPoolSize)
             * 
             * Why?
             * - Reduces latency for initial database operations
             * - Avoids delay caused by opening connections at runtime
             */
            int initialSize = Math.Min(2, maxPoolSize);
            for (int i = 0; i < initialSize; i++)
            {
                SqlConnection conn = CreateNewConnection();
                availableConnections.Push(conn);
            }

            Console.WriteLine("[ConnectionPool] Ready. Pre-created Connection Pool: " + initialSize);
        }

        /*
         * ========================================================================================
         * METHOD: GetInstance
         * 
         * Provides access to the singleton instance using double-checked locking.
         * ========================================================================================
         */
        public static ConnectionPool GetInstance(ApplicationConfig config)
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ConnectionPool(config);
                    }
                }
            }
            return instance;
        }

        /*
         * ========================================================================================
         * METHOD: CreateNewConnection (PRIVATE)
         * 
         * Creates and opens a brand new SqlConnection.
         * Also updates the totalCreated counter.
         * ========================================================================================
         */
        private SqlConnection CreateNewConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            totalCreated++;
            return conn;
        }

        /*
         * ========================================================================================
         * METHOD: GetConnection
         * 
         * Borrows a connection from the pool.
         * 
         * Behavior:
         * ----------------------------------------------------------------------------------------
         * 1. If pool has available connections → reuse one
         * 2. If pool is empty but under limit → create new connection
         * 3. If pool is exhausted → wait briefly and retry
         * 4. If still unavailable → create emergency connection
         * ========================================================================================
         */
        public SqlConnection GetConnection()
        {
            lock (lockObject)
            {
                if (availableConnections.Count > 0)
                {
                    SqlConnection conn = availableConnections.Pop();

                    // Ensure connection is open before returning From The Database
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    return conn;
                }

                if (totalCreated < maxPoolSize)
                {
                    Console.WriteLine("[ConnectionPool] Creating new connection. Total Connection: "
                        + (totalCreated + 1));
                    return CreateNewConnection();
                }

                /*
                 * Pool exhausted scenario:
                 * 
                 * - Wait briefly to allow other threads to return connections
                 * - Retry once before creating emergency connection
                 */
                Console.WriteLine("[ConnectionPool] Pool exhausted. Waiting 500ms For Cooling Pool...");
                System.Threading.Thread.Sleep(500);

                if (availableConnections.Count > 0)
                {
                    return availableConnections.Pop();
                }

                // Emergency fallback: create connection beyond pool limit
                Console.WriteLine("[ConnectionPool] Creating emergency connection.");
                return CreateNewConnection();
            }
        }

        /*
         * ========================================================================================
         * METHOD: ReturnConnection
         * 
         * Returns a borrowed connection back to the pool.
         * 
         * Behavior:
         * ----------------------------------------------------------------------------------------
         * - If pool has space and connection is valid → store for reuse
         * - Otherwise → close and dispose the connection
         * ========================================================================================
         */
        public void ReturnConnection(SqlConnection conn)
        {
            if (conn == null)
            {
                return;
            }

            lock (lockObject)
            {
                if (availableConnections.Count < maxPoolSize
                    && conn.State == ConnectionState.Open)
                {
                    // Return connection to pool
                    availableConnections.Push(conn);
                }
                else
                {
                    // Dispose connection if pool is full or connection is invalid and Close Connection Also
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /*
         * ========================================================================================
         * METHOD: GetPoolStatistics
         * 
         * Returns a summary of the pool state for monitoring/debugging.
         * ========================================================================================
         */
        public string GetPoolStatistics()
        {
            return "Total created Statistics: " + totalCreated
                + " | Idle Statistics: " + availableConnections.Count
                + " | Max Statistics: " + maxPoolSize;
        }

        /*
         * ========================================================================================
         * METHOD: Dispose
         * 
         * Closes and disposes all connections currently in the pool.
         * 
         * Purpose:
         * - Free resources when application shuts down
         * - Prevent memory leaks
         * ========================================================================================
         */
        public void Dispose()
        {
            lock (lockObject)
            {
                Console.WriteLine("[ConnectionPool] Disposing all connections For Database...");
                while (availableConnections.Count > 0)
                {
                    SqlConnection conn = availableConnections.Pop();
                    conn.Close();
                    conn.Dispose();
                }
                Console.WriteLine("[ConnectionPool] All connections disposed For Database.");
            }
        }

        /*
         * ========================================================================================
         * METHOD: Reset
         * 
         * Resets the singleton instance (mainly for testing purposes).
         * 
         * Behavior:
         * - Disposes existing pool
         * - Sets instance to null so a new one can be created
         * ========================================================================================
         */
        public static void Reset()
        {
            lock (lockObject)
            {
                if (instance != null)
                {
                    instance.Dispose();
                    instance = null;
                }
            }
        }
    }
}