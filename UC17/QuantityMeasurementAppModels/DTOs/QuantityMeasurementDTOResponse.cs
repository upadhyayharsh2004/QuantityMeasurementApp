using System.Collections.Generic;
using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppModels.DTOs
{
    //QuantityMeasurementResponseDTO as QuantityMeasurementDTOResponse
    public class QuantityMeasurementDTOResponse
    {
        // Properties
        public double ThisValueDTOs { get; set; }
        public string ThisUnitDTOs { get; set; }
        public string ThisMeasurementTypeDTOs { get; set; }

        public double ResultValueDTOs { get; set; }
        public string ResultUnitDTOs { get; set; }
        public string ResultMeasurementTypeDTOs { get; set; }
        public string ErrorMessageDTOs { get; set; }
        public bool IsThereErrorDTOs { get; set; }
        public double ThereValueDTOs { get; set; }
        public string ThereUnitDTOs { get; set; }
        public string ThereMeasurementTypeDTOs { get; set; }
        public string OperationDTOs { get; set; }
        public string ResultStringDTOs { get; set; }


        // Transforms an entity into its corresponding DTO in QuantityMeasurementAppModels/Entities
        public static QuantityMeasurementDTOResponse FromEntityToDTOs(QuantityEntity entity)
        {
            QuantityMeasurementDTOResponse dto = new QuantityMeasurementDTOResponse();

            dto.ThisValueDTOs = entity.EntityFirstValue;
            dto.ThisUnitDTOs = entity.EntityFirstUnit;
            dto.ResultValueDTOs = entity.EntityResultValue;
            dto.ErrorMessageDTOs = entity.EntityErrorMessage;
            dto.IsThereErrorDTOs = entity.IsEntityError;
            dto.ThisMeasurementTypeDTOs = entity.EntityMeasurementType;
            dto.ThereValueDTOs = entity.EntitySecondValue;
            dto.ThereUnitDTOs = entity.EntitySecondUnit;
            dto.ThereMeasurementTypeDTOs = entity.EntityMeasurementType;
            dto.OperationDTOs = entity.EntityOperation;

            return dto;
        }

        // Transforming and converting a list of entities from Quantity Entity to a list of DTOs
        public static List<QuantityMeasurementDTOResponse> FromEntityListToDTOs(List<QuantityEntity> entities)
        {
            List<QuantityMeasurementDTOResponse> list = new List<QuantityMeasurementDTOResponse>();

            for (int i = 0; i < entities.Count; i++)
            {
                list.Add(FromEntityToDTOs(entities[i]));
            }

            return list;
        }

        //Conversion of DTO to an Quantity Measurement Entity
        public QuantityEntity ToEntityDTOs()
        {
            if (IsThereErrorDTOs)
            {
                return new QuantityEntity(OperationDTOs, ErrorMessageDTOs);
            }

            return new QuantityEntity(
                OperationDTOs,
                ThisValueDTOs, ThisUnitDTOs,
                ThereValueDTOs, ThereUnitDTOs,
                ResultValueDTOs,
                ThisMeasurementTypeDTOs);
        }

        //Conversion of list of DTOs from Quantity Measurement App Models / DTOs to a list of entities in QuantityMeasurementAppModels/Entities
        public static List<QuantityEntity> ToEntityList(List<QuantityMeasurementDTOResponse> dtos)
        {
            List<QuantityEntity> list = new List<QuantityEntity>();

            for (int i = 0; i < dtos.Count; i++)
            {
                list.Add(dtos[i].ToEntityDTOs());
            }

            return list;
        }
    }
}