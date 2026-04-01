using System;
using System.Collections.Generic;
using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppRepositories.Interfaces
{
    public interface IQuantityLogRepository
    {
        List<QuantityEntity> GetRecordsByOperation(string RecordsOperation);
        List<QuantityEntity> GetRecordsByMeasurementType(string RecordsMeasurementType);
        List<QuantityEntity> GetRecordsErrorHistory();
        int GetRecordsOperationCount(string EntityOperation);
        void SaveRecords(QuantityEntity MeasurementEntity);
        List<QuantityEntity> GetAllRecords();
        List<QuantityEntity> GetRecordsByCreatedAfter(DateTime RecordDate);
    }
}