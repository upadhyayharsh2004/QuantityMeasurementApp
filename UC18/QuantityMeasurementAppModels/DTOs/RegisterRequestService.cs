using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementAppModels.DTOs
{
    public class RegisterRequestService
    {
        [Required(ErrorMessage = "User Register Name is required")]
        //Name as UserName
        public string UserName { get; set; }

        [Required(ErrorMessage = "User Register Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]

        //Email as UserEmail
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "User Register Password is required")]
        [MinLength(6, ErrorMessage = "For Registration User Password must be at least 6 characters")]

        //Password as UserPassword
        public string UserPassword { get; set; }

    }
}
