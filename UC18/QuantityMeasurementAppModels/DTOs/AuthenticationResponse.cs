using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementAppModels.DTOs
{
    public class AuthenticationResponse
    {
        //Token as AuthorizationId
        public string AuthorizationId { get; set; } = string.Empty;

        //TokenType as AuthorizationType
        public string AuthorizationType { get; set; } = "Bearer";

        //ExpiredIn=ValidIn
        public int ValidIn { get; set; }

        //Id=PersonId
        public long PersonId { get; set; }

        //Email=PersonEmail
        public string PersonEmail { get; set; } = string.Empty;

        //Name=PersonName
        public string PersonName { get; set; } = string.Empty;

        //IssuedAt=GrantedAt
        public string GrantedAt { get; set; } = string.Empty;
    }
}
//shi hai file no change