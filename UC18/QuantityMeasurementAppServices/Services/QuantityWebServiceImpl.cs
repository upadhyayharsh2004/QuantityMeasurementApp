using System.Collections.Generic;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppModels.Entities;
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
        public QuantityMeasurementDTOResponse ComparisonWeb(QuantityInputRequestDTOs quantityrequest,long personId)
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
            QuantityEntity entityDTOs = new QuantityEntity(personId,"Compare",quantityrequest.ThisQuantityDTO.ValueDTOs, quantityrequest.ThisQuantityDTO.UnitNameDTOs,quantityrequest.ThereQuantityDTO.ValueDTOs, quantityrequest.ThereQuantityDTO.UnitNameDTOs,WebResult ? 1 : 0,quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs);
            quantityLogRepositorys.SaveRecords(entityDTOs);

            return QuantityDtos;
        }
        public List<QuantityMeasurementDTOResponse> FetchWebHistoryByOperation(string OperationRequest,long personId)
        {
            return QuantityMeasurementDTOResponse.FromEntityListToDTOs(quantityLogRepositorys.GetRecordsByOperation(OperationRequest, personId));
        }
        public List<QuantityMeasurementDTOResponse> FetchWebHistoryByType(string OperationMeasurementType,long personId)
        {
            return QuantityMeasurementDTOResponse.FromEntityListToDTOs(quantityLogRepositorys.GetRecordsByMeasurementType(OperationMeasurementType,personId));
        }
        public QuantityMeasurementDTOResponse ConversionWeb(ConvertRequestDTOs quantityrequest,long personId)
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
            QuantityEntity entityDTOs = new QuantityEntity(
                personId,
                "Convert",
                quantityrequest.ThisQuantityDTO.ValueDTOs, quantityrequest.ThisQuantityDTO.UnitNameDTOs,
                WebResult.ValueDTOs,
                quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs
            );
            quantityLogRepositorys.SaveRecords(entityDTOs);
            return QuantityDtos;
        }
        public QuantityMeasurementDTOResponse CombineWeb(ArithmeticRequestDTOs quantityrequest,long personId)
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
            QuantityEntity entityDTOs = new QuantityEntity(
                personId,
                "Add",
                quantityrequest.ThisQuantityDTO.ValueDTOs, quantityrequest.ThisQuantityDTO.UnitNameDTOs,
                quantityrequest.ThereQuantityDTO.ValueDTOs, quantityrequest.ThereQuantityDTO.UnitNameDTOs,
                WebResult.ValueDTOs,
                quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs
            );
            quantityLogRepositorys.SaveRecords(entityDTOs);
            return QuantityDtos;
        }
        public QuantityMeasurementDTOResponse DifferenceWeb(ArithmeticRequestDTOs quantityrequest,long personId)
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
            QuantityEntity entityDTOs = new QuantityEntity(
                personId,
                "Subtract",
                quantityrequest.ThisQuantityDTO.ValueDTOs, quantityrequest.ThisQuantityDTO.UnitNameDTOs,
                quantityrequest.ThereQuantityDTO.ValueDTOs, quantityrequest.ThereQuantityDTO.UnitNameDTOs,
                WebResult.ValueDTOs,
                quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs
            );
            quantityLogRepositorys.SaveRecords(entityDTOs);

            return QuantityDtos;
        }
        public List<QuantityMeasurementDTOResponse> FetchWebErrorHistory(long personId)
        {
            return QuantityMeasurementDTOResponse.FromEntityListToDTOs(quantityLogRepositorys.GetRecordsErrorHistory(personId));
        }
        public QuantityMeasurementDTOResponse DivisonWeb(QuantityInputRequestDTOs quantityrequest,long personId)
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
            QuantityEntity entityDTOs = new QuantityEntity(
                personId,
                "Divide",
                quantityrequest.ThisQuantityDTO.ValueDTOs, quantityrequest.ThisQuantityDTO.UnitNameDTOs,
                quantityrequest.ThereQuantityDTO.ValueDTOs, quantityrequest.ThereQuantityDTO.UnitNameDTOs,
                WebResult,
                quantityrequest.ThisQuantityDTO.MeasurementTypeDTOs
            );
            quantityLogRepositorys.SaveRecords(entityDTOs);
            return QuantityDtos;
        }
        public int FetchWebsOperationCount(string QuantityOperation,long personId)
        {
            return quantityLogRepositorys.GetRecordsOperationCount(QuantityOperation,personId);
        }
    }
}