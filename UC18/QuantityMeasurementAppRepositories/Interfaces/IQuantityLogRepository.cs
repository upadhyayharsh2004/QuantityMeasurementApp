using System;
using System.Collections.Generic;
using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppRepositories.Interfaces
{
    public interface IQuantityLogRepository
    {
        List<QuantityEntity> GetRecordsByOperation(string RecordsOperation,long personId);
        List<QuantityEntity> GetRecordsByMeasurementType(string RecordsMeasurementType,long personId);
        List<QuantityEntity> GetRecordsErrorHistory(long personId);
        int GetRecordsOperationCount(string EntityOperation,long personId);
        void SaveRecords(QuantityEntity MeasurementEntity);
        List<QuantityEntity> GetAllRecords(long personId);
        List<QuantityEntity> GetRecordsByCreatedAfter(DateTime RecordDate,long personId);
    }
}