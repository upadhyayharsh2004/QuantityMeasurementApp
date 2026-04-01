using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppServices.Interfaces;

namespace QuantityMeasurementAppWebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/quantities")]
    public class QuantityMeasurementControllerWebService : ControllerBase
    {
        private IQuantityServiceImplWeb serviceImpl;
        public QuantityMeasurementControllerWebService(IQuantityServiceImplWeb webService)
        {
            this.serviceImpl = webService;
        }
        [HttpPost("subtraction")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Subtraction([FromBody] ArithmeticRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.DifferenceWeb(request);
            return Ok(resultWeb);
        }

        [HttpPost("addition")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Addition([FromBody] ArithmeticRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.CombineWeb(request);
            return Ok(resultWeb);
        }
        [HttpGet("history/errored")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTOResponse>), 200)]
        public IActionResult FetchWebErrorHistory()
        {
            List<QuantityMeasurementDTOResponse> resultWeb = serviceImpl.FetchWebErrorHistory();
            return Ok(resultWeb);
        }

        [HttpGet("history/operation/{operation}")]
        public IActionResult FetchWebHistoryByOperation(string operation)
        {
            var resultWeb = serviceImpl.FetchWebHistoryByOperation(operation);
            return Ok(resultWeb);
        }


        [HttpGet("history/type/{type}")]
        public IActionResult FetchWebHistoryByType(string type)
        {
            var resultWeb = serviceImpl.FetchWebHistoryByType(type);
            return Ok(resultWeb);
        }

        [HttpPost("conversion")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Converison([FromBody] ConvertRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.ConversionWeb(request);
            return Ok(resultWeb);
        }
        
        [HttpPost("division")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Division([FromBody] QuantityInputRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.DivisonWeb(request);
            return Ok(resultWeb);
        }

        [HttpGet("count/{operation}")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult FetchWebsOperationCount(string operation)
        {
            int countServices = serviceImpl.FetchWebsOperationCount(operation);
            return Ok(countServices);
        }

        [HttpPost("comparison")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Comparison([FromBody] QuantityInputRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.ComparisonWeb(request);
            return Ok(resultWeb);
        }
    }
}