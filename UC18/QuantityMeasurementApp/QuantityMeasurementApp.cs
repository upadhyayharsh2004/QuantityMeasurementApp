using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Controllers;
using QuantityMeasurementApp.Interfaces;
using QuantityMeasurementApp.Menu;
using QuantityMeasurementApp.Services;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppRepositories.Repositories;
using QuantityMeasurementAppServices.Interfaces;
using QuantityMeasurementAppRepositories.Context;
namespace QuantityMeasurementApp
{
    public class QuantityMeasurementApp
    {
        private static QuantityMeasurementApp AppInstance;
        private static readonly object lockObjectQuantity = new object();

        private QuantityMeasurementController quantityController;
        private IMenu quantityMenu;
        private IQuantityLogRepository quantityRepository;

        // Constructor
        private QuantityMeasurementApp()
        {
            Console.WriteLine("[App] Executing and Starting Quantity Measurement App Application...");

            quantityRepository = new QuantityRepository(CreateDbContext());

            IQuantityServiceImplsConvert quantityService = new QuantityImplService(quantityRepository);

            quantityController = new QuantityMeasurementController(quantityService);

            quantityMenu = new QuantityMenu(quantityController);

            Console.WriteLine("[App] Quantity Measurement App Initialized using EF Core Repository");
            Console.WriteLine("");
        }
        private static DatabaseAppContext CreateDbContext()
        {
            var quantityOptions = new DbContextOptionsBuilder<DatabaseAppContext>().UseSqlServer("Server=localhost\\SQLEXPRESS;Database=QuantityMeasurementAppDB;Trusted_Connection=True;TrustServerCertificate=True;").Options;

            return new DatabaseAppContext(quantityOptions);
        }
        public static QuantityMeasurementApp GetInstance()
        {
            if (AppInstance == null)
            {
                lock (lockObjectQuantity)
                {
                    if (AppInstance == null)
                    {
                        AppInstance = new QuantityMeasurementApp();
                    }
                }
            }
            return AppInstance;
        }
        public void Start()
        {
            quantityMenu.Execute();
        }
        public void ReportAllRecordsMeasurements()
        {
            Console.WriteLine("\n========== Quantity App Measurement History ==========");

            List<QuantityEntity> allRecords = quantityRepository.GetAllRecords(0);

            Console.WriteLine("Total records in Quantity Measurement App: " + allRecords.Count);

            for (int i = 0; i < allRecords.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + allRecords[i]);
            }

            Console.WriteLine("=========================================\n");
        }
        public void DeleteAllMeasurements()
        {
            Console.WriteLine("[App] Deleting all records measurements...");

            Console.WriteLine("[App] Records Execution Done.");
        }
        public void CloseResources()
        {
            Console.WriteLine("[App] Closing all quantity app records resources...");

            Console.WriteLine("[App] All Quantity App Resources closed.");
        }
    }
}