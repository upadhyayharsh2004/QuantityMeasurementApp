// Importing System namespace which provides core functionalities 
// such as Console input/output, Exception handling, etc.
using System;

// Defining the main namespace of the application
namespace QuantityMeasurementApp
{
    // This Program class acts as the entry point of the application
    // It contains the Main method which is executed first when the program runs
    public class Program
    {
        // Main method is the starting point of execution in a C# application
        // 'args' allows passing command-line arguments (not used here but included by default)
        public static void Main(string[] args)
        {
            // Retrieving the single instance of the application using Singleton pattern
            // This ensures that only one instance of QuantityMeasurementApp exists
            // throughout the entire lifecycle of the program
            QuantityMeasurementApp app = QuantityMeasurementApp.GetInstance();

            try
            {
                // Starting the application
                // This will launch the menu and begin user interaction
                app.Start();

                // After the menu exits, display all stored measurement records
                // This gives a summary/report of all operations performed during execution
                app.ReportAllMeasurements();
            }
            catch (Exception ex)
            {
                // Catch block handles any unhandled exceptions that occur during execution
                // This prevents the application from crashing abruptly
                // and provides a meaningful error message to the user
                Console.WriteLine("A critical application error occurred: " + ex.Message);
            }
            finally
            {
                // Finally block is always executed regardless of success or failure
                // This ensures that important cleanup operations are performed

                // Releasing resources such as database connections or connection pool
                // This prevents memory leaks and ensures proper shutdown of the application
                app.CloseResources();
            }
        }
    }
}