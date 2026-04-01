using System.Collections.Generic;
using QuantityMeasurementAppModels.DTOs;

namespace QuantityMeasurementAppServices.Interfaces
{
    public interface IQuantityServiceImplWeb
    {
        QuantityMeasurementDTOResponse ComparisonWeb(QuantityInputRequestDTOs OperationRequest);
        QuantityMeasurementDTOResponse ConversionWeb(ConvertRequestDTOs OperationRequest);
        QuantityMeasurementDTOResponse CombineWeb(ArithmeticRequestDTOs OperationRequest);
        QuantityMeasurementDTOResponse DifferenceWeb(ArithmeticRequestDTOs OperationRequest);
        QuantityMeasurementDTOResponse DivisonWeb(QuantityInputRequestDTOs OperationRequest);

        List<QuantityMeasurementDTOResponse> FetchWebHistoryByOperation(string RequestedOperation);
        List<QuantityMeasurementDTOResponse> FetchWebHistoryByType(string OperationMeasurementType);
        List<QuantityMeasurementDTOResponse> FetchWebErrorHistory();
        int FetchWebsOperationCount(string operationMeasured);
    }
}