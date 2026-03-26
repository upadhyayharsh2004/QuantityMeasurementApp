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
        public static void Main(string[] args)
        {
            // Creating and retrieving the singleton instance of the application
            QuantityMeasurementApp extremelyImportantSingletonInstanceOfQuantityMeasurementApplicationUsedForEntireApplicationLifecycle 
                = QuantityMeasurementApp.GetInstance();

            try
            {
                // Starting the application execution which will initialize menu and user interaction flow
                extremelyImportantSingletonInstanceOfQuantityMeasurementApplicationUsedForEntireApplicationLifecycle
                    .StartTheEntireQuantityMeasurementApplicationExecutionProcess();

                // Displaying all stored measurement records after application execution completes
                extremelyImportantSingletonInstanceOfQuantityMeasurementApplicationUsedForEntireApplicationLifecycle
                    .GenerateAndDisplayCompleteMeasurementOperationsReportToUser();
            }
            catch (Exception extremelyCriticalExceptionOccurredDuringApplicationExecution)
            {
                // Handling unexpected critical errors gracefully
                Console.WriteLine("❌ A critical application error occurred during execution: " 
                    + extremelyCriticalExceptionOccurredDuringApplicationExecution.Message);
            }
            finally
            {
                // Ensuring that all resources such as database connections are properly released
                extremelyImportantSingletonInstanceOfQuantityMeasurementApplicationUsedForEntireApplicationLifecycle
                    .ReleaseAndCleanupAllApplicationResourcesBeforeShutdown();
            }
        }
    }
}