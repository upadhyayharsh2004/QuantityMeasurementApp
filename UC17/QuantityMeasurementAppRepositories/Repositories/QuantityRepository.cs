using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppRepositories.Context;

namespace QuantityMeasurementAppRepositories.Repositories
{
    public class QuantityRepository : IQuantityLogRepository
    {
        private readonly DatabaseAppContext DatabaseContext;

        public QuantityRepository(DatabaseAppContext EntityDataContext)
        {
            this.DatabaseContext = EntityDataContext;
        }
        public List<QuantityEntity> GetRecordsErrorHistory()
        {
            return DatabaseContext.MeasurementRecordsEntity.Where(e => e.IsEntityError == true).OrderByDescending(e => e.EntityCreatedAt).ToList();
        }
        public int GetRecordsOperationCount(string RecordsOperation)
        {
            return DatabaseContext.MeasurementRecordsEntity.Count(e => e.EntityOperation == RecordsOperation && e.IsEntityError == false);
        }
        public List<QuantityEntity> GetRecordsByCreatedAfter(DateTime DatabaseDate)
        {
            return DatabaseContext.MeasurementRecordsEntity.Where(e => e.EntityCreatedAt > DatabaseDate).OrderByDescending(e => e.EntityCreatedAt).ToList();
        }
        public void SaveRecords(QuantityEntity RecordsEntity)
        {
            Console.WriteLine("----- DEBUG ENTITY -----");
            Console.WriteLine("First Value: " + RecordsEntity.EntityFirstValue);
            Console.WriteLine("First Unit: " + RecordsEntity.EntityFirstUnit);
            Console.WriteLine("Second Value: " + RecordsEntity.EntitySecondValue);
            Console.WriteLine("Second Unit: " + RecordsEntity.EntitySecondUnit);
            Console.WriteLine("Result Value: " + RecordsEntity.EntityResultValue);
            Console.WriteLine("Operation: " + RecordsEntity.EntityOperation);
            Console.WriteLine("MeasurementType: " + RecordsEntity.EntityMeasurementType);
            Console.WriteLine("------------------------");
            try
            {
                DatabaseContext.MeasurementRecordsEntity.Add(RecordsEntity);
                DatabaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Main Error: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Error: " + ex.InnerException.Message);
                }
            }
        }
        public List<QuantityEntity> GetAllRecords()
        {
            return DatabaseContext.MeasurementRecordsEntity.OrderByDescending(e => e.EntityCreatedAt).ToList();
        }
        public List<QuantityEntity> GetRecordsByOperation(string RecordsOperation)
        {
            return DatabaseContext.MeasurementRecordsEntity.Where(e => e.EntityOperation == RecordsOperation).OrderByDescending(e => e.EntityCreatedAt).ToList();
        }
        public List<QuantityEntity> GetRecordsByMeasurementType(string RecordsMeasurementType)
        {
            return DatabaseContext.MeasurementRecordsEntity.Where(e => e.EntityMeasurementType == RecordsMeasurementType).OrderByDescending(e => e.EntityCreatedAt).ToList();
        }
    }
}