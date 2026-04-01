using System.Collections.Generic;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppServices.Interfaces;

namespace QuantityMeasurementAppServices.Services
{
    public class QuantityWebServiceImpl : IQuantityServiceImplWeb
    {
        private readonly IQuantityServiceImplsConvert QuantityWebservice;
        private readonly IQuantityLogRepository quantityLogRepositorys;
        public QuantityWebServiceImpl(IQuantityServiceImplsConvert quantityservice, IQuantityLogRepository quantityrepository)
        {
            this.QuantityWebservice = quantityservice;
            this.quantityLogRepositorys = quantityrepository;
        }
        public QuantityMeasurementDTOResponse ComparisonWeb(QuantityInputRequestDTOs quantityrequest)
        {
            bool WebResult = QuantityWebservice.Comparison(quantityrequest.ThisQuantityDTO, quantityrequest.ThereQuantityDTO);

            QuantityMeasurementDTOResponse QuantityDtos = new QuantityMeasurementDTOResponse
            {
                ThisValueDTOs = quantityrequest.ThisQuantityDTO.ValueDTOs,
                ThisUnitDTOs = quantityrequest.ThisQuantityDTO.UnitNameDTOs,
                ThisMeasurementTypeDTOs = quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs,
                ThereValueDTOs = quantityrequest.ThereQuantityDTO.ValueDTOs,
                ThereUnitDTOs = quantityrequest.ThereQuantityDTO.UnitNameDTOs,
                ThereMeasurementTypeDTOs = quantityrequest.ThereQuantityDTO.MeasurementTypeDTOs,
                OperationDTOs = "Compare",
                ResultStringDTOs = WebResult.ToString(),
                IsThereErrorDTOs = false
            };
            return QuantityDtos;
        }
        public List<QuantityMeasurementDTOResponse> FetchWebHistoryByOperation(string OperationRequest)
        {
            return QuantityMeasurementDTOResponse.FromEntityListToDTOs(quantityLogRepositorys.GetRecordsByOperation(OperationRequest));
        }
        public List<QuantityMeasurementDTOResponse> FetchWebHistoryByType(string OperationMeasurementType)
        {
            return QuantityMeasurementDTOResponse.FromEntityListToDTOs(quantityLogRepositorys.GetRecordsByMeasurementType(OperationMeasurementType));
        }
        public QuantityMeasurementDTOResponse ConversionWeb(ConvertRequestDTOs quantityrequest)
        {
            QuantityDTOs WebResult = QuantityWebservice.Conversion(quantityrequest.ThisQuantityDTO, quantityrequest.TargetUnitDTOs);

            QuantityMeasurementDTOResponse QuantityDtos = new QuantityMeasurementDTOResponse
            {
                ThisValueDTOs = quantityrequest.ThisQuantityDTO.ValueDTOs,
                ThisUnitDTOs = quantityrequest.ThisQuantityDTO.UnitNameDTOs,
                ThisMeasurementTypeDTOs = quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs,
                OperationDTOs = "Convert",
                ResultValueDTOs = WebResult.ValueDTOs,
                ResultUnitDTOs = quantityrequest.TargetUnitDTOs,
                IsThereErrorDTOs = false
            };
            return QuantityDtos;
        }
        public QuantityMeasurementDTOResponse CombineWeb(ArithmeticRequestDTOs quantityrequest)
        {
            QuantityDTOs WebResult = QuantityWebservice.Combine(quantityrequest.ThisQuantityDTO, quantityrequest.ThereQuantityDTO, quantityrequest.TargetUnitDTOs);

            QuantityMeasurementDTOResponse QuantityDtos = new QuantityMeasurementDTOResponse
            {
                ThisValueDTOs = quantityrequest.ThisQuantityDTO.ValueDTOs,
                ThisUnitDTOs = quantityrequest.ThisQuantityDTO.UnitNameDTOs,
                ThisMeasurementTypeDTOs = quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs,
                ThereValueDTOs = quantityrequest.ThereQuantityDTO.ValueDTOs,
                ThereUnitDTOs = quantityrequest.ThereQuantityDTO.UnitNameDTOs,
                ThereMeasurementTypeDTOs = quantityrequest.ThereQuantityDTO.MeasurementTypeDTOs,
                OperationDTOs = "Add",
                ResultValueDTOs = WebResult.ValueDTOs,
                ResultUnitDTOs = quantityrequest.TargetUnitDTOs,
                ResultMeasurementTypeDTOs = quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs,
                IsThereErrorDTOs = false
            };
            return QuantityDtos;
        }
        public QuantityMeasurementDTOResponse DifferenceWeb(ArithmeticRequestDTOs quantityrequest)
        {
            QuantityDTOs WebResult = QuantityWebservice.Difference(quantityrequest.ThisQuantityDTO, quantityrequest.ThereQuantityDTO, quantityrequest.TargetUnitDTOs);

            QuantityMeasurementDTOResponse QuantityDtos = new QuantityMeasurementDTOResponse
            {
                ThisValueDTOs = quantityrequest.ThisQuantityDTO.ValueDTOs,
                ThisUnitDTOs = quantityrequest.ThisQuantityDTO.UnitNameDTOs,
                ThisMeasurementTypeDTOs = quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs,
                ThereValueDTOs = quantityrequest.ThereQuantityDTO.ValueDTOs,
                ThereUnitDTOs = quantityrequest.ThereQuantityDTO.UnitNameDTOs,
                ThereMeasurementTypeDTOs = quantityrequest.ThereQuantityDTO.MeasurementTypeDTOs,
                OperationDTOs = "Subtract",
                ResultValueDTOs = WebResult.ValueDTOs,
                ResultUnitDTOs = quantityrequest.TargetUnitDTOs,
                ResultMeasurementTypeDTOs = quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs,
                IsThereErrorDTOs = false
            };
            return QuantityDtos;
        }
        public List<QuantityMeasurementDTOResponse> FetchWebErrorHistory()
        {
            return QuantityMeasurementDTOResponse.FromEntityListToDTOs(quantityLogRepositorys.GetRecordsErrorHistory());
        }
        public QuantityMeasurementDTOResponse DivisonWeb(QuantityInputRequestDTOs quantityrequest)
        {
            double WebResult = QuantityWebservice.Divison(quantityrequest.ThisQuantityDTO, quantityrequest.ThereQuantityDTO);

            QuantityMeasurementDTOResponse QuantityDtos = new QuantityMeasurementDTOResponse
            {
                ThisValueDTOs = quantityrequest.ThisQuantityDTO.ValueDTOs,
                ThisUnitDTOs = quantityrequest.ThisQuantityDTO.UnitNameDTOs,
                ThisMeasurementTypeDTOs = quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs,
                ThereValueDTOs = quantityrequest.ThereQuantityDTO.ValueDTOs,
                ThereUnitDTOs = quantityrequest.ThereQuantityDTO.UnitNameDTOs,
                ThereMeasurementTypeDTOs = quantityrequest.ThereQuantityDTO.MeasurementTypeDTOs,
                OperationDTOs = "Divide",
                ResultValueDTOs = WebResult,
                IsThereErrorDTOs = false
            };
            return QuantityDtos;
        }
        public int FetchWebsOperationCount(string QuantityOperation)
        {
            return quantityLogRepositorys.GetRecordsOperationCount(QuantityOperation);
        }
    }
}