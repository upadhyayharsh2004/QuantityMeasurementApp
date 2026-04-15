using System.Collections.Generic;
using QuantityMeasurementAppModels.DTOs;

namespace QuantityMeasurementAppServices.Interfaces
{
    public interface IQuantityServiceImplWeb
    {
        QuantityMeasurementDTOResponse ComparisonWeb(QuantityInputRequestDTOs OperationRequest,long personId);
        QuantityMeasurementDTOResponse ConversionWeb(ConvertRequestDTOs OperationRequest,long personId);
        QuantityMeasurementDTOResponse CombineWeb(ArithmeticRequestDTOs OperationRequest,long personId);
        QuantityMeasurementDTOResponse DifferenceWeb(ArithmeticRequestDTOs OperationRequest,long personId);
        QuantityMeasurementDTOResponse DivisonWeb(QuantityInputRequestDTOs OperationRequest,long personId);

        List<QuantityMeasurementDTOResponse> FetchWebHistoryByOperation(string RequestedOperation,long personId);
        List<QuantityMeasurementDTOResponse> FetchWebHistoryByType(string OperationMeasurementType,long personId);
        List<QuantityMeasurementDTOResponse> FetchWebErrorHistory(long personId);
        int FetchWebsOperationCount(string operationMeasured,long personId);
    }
}