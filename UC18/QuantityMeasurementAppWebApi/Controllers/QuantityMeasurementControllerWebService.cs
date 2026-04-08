using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAppModels.DTOs;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using QuantityMeasurementAppServices.Interfaces;

namespace QuantityMeasurementAppWebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/quantities")]
    [Authorize]
    public class QuantityMeasurementControllerWebService : ControllerBase
    {
        private IQuantityServiceImplWeb serviceImpl;
        public QuantityMeasurementControllerWebService(IQuantityServiceImplWeb webService)
        {
            this.serviceImpl = webService;
        }
        private long FetchSessionUserId()
        {
            string? personIdClaim = User.FindFirstValue("personId");
            if (personIdClaim == null)
            {
                return 0;
            }
            return long.TryParse(personIdClaim, out long personId) ? personId : 0;
        }
        private string FetchCurrentUserEmail()
        {
            //unknown
            return User.FindFirstValue(JwtRegisteredClaimNames.Email) ?? "Not Recognized User";
        }

        [HttpPost("subtraction")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Subtraction([FromBody] ArithmeticRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.DifferenceWeb(request,FetchSessionUserId());
            return Ok(resultWeb);
        }

        [HttpPost("addition")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Addition([FromBody] ArithmeticRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.CombineWeb(request,FetchSessionUserId());
            return Ok(resultWeb);
        }

        [HttpGet("history/errored")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTOResponse>), 200)]
        [ProducesResponseType(401)]
        public IActionResult FetchWebErrorHistory()
        {
            List<QuantityMeasurementDTOResponse> resultWeb = serviceImpl.FetchWebErrorHistory(FetchSessionUserId());
            return Ok(resultWeb);//hogya
        }

        [HttpGet("history/operation/{operation}")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTOResponse>), 200)]
        [ProducesResponseType(401)]
        public IActionResult FetchWebHistoryByOperation(string operation)
        {
            List<QuantityMeasurementDTOResponse>  resultWeb = serviceImpl.FetchWebHistoryByOperation(operation,FetchSessionUserId());
            return Ok(resultWeb);
        }


        [HttpGet("history/type/{type}")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTOResponse>), 200)]
        [ProducesResponseType(401)]
        public IActionResult FetchWebHistoryByType(string type)
        {
            List<QuantityMeasurementDTOResponse> resultWeb= serviceImpl.FetchWebHistoryByType(type,FetchSessionUserId());
            return Ok(resultWeb);//hogya
        }

        [HttpPost("conversion")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Converison([FromBody] ConvertRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.ConversionWeb(request,FetchSessionUserId());
            return Ok(resultWeb);
        }

        [HttpPost("division")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Division([FromBody] QuantityInputRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.DivisonWeb(request,FetchSessionUserId());
            return Ok(resultWeb);
        }

        [HttpGet("count/{operation}")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(401)]
        public IActionResult FetchWebsOperationCount(string operation)
        {
            int countServices = serviceImpl.FetchWebsOperationCount(operation,FetchSessionUserId());
            return Ok(countServices);//hogya
        }

        [HttpPost("comparison")]
        [ProducesResponseType(typeof(QuantityMeasurementDTOResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Comparison([FromBody] QuantityInputRequestDTOs request)
        {
            QuantityMeasurementDTOResponse resultWeb = serviceImpl.ComparisonWeb(request,FetchSessionUserId());
            return Ok(resultWeb);
        }
        [HttpGet("me")]
        public IActionResult WhoAmI()
        {
            return Ok(new
            {
                UserId = FetchSessionUserId(),
                Email = FetchCurrentUserEmail(),
                Name = User.FindFirstValue("name") ?? "Not Recognized User"
            });
        }
    }
}