using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuantityMeasurementAppBusiness.Exceptions;
using QuantityMeasurementAppModels.DTOs;

namespace QuantityMeasurementWebApi.Middleware
{
    public class ApplicationErrorHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext ExceptionContext)
        {
            int statusCode = GetStatusErrorCode(ExceptionContext.Exception);
            string errorCode = GetErrorCodeMessage(statusCode);

            // Build the error response
            ErrorResponseDTOs responseStatus = new ErrorResponseDTOs
            {
                TimestampDTOs = DateTime.UtcNow.ToString(),
                StatusDTOs = statusCode,
                ErrorDTOs = errorCode,
                MessageDTOs = ExceptionContext.Exception.Message,
                PathDTOs = ExceptionContext.HttpContext.Request.Path.ToString()
            };
            ExceptionContext.Result = new ObjectResult(responseStatus)
            {
                StatusCode = statusCode
            };
            ExceptionContext.ExceptionHandled = true;
        }
        private int GetStatusErrorCode(Exception exceptionMessage)
        {
            if (exceptionMessage is QuantityException)
            {
                return 400;
            }

            if (exceptionMessage is ArgumentException)
            {
                return 400;
            }

            if (exceptionMessage is NotSupportedException)
            {
                return 400;
            }
            if (exceptionMessage is UnauthorizedAccessException)
            {
                return 401;
            }

            if (exceptionMessage is InvalidOperationException)
            {
                return 409;
            }

            return 500;
        }
        private string GetErrorCodeMessage(int statusCode)
        {
            if (statusCode == 400)
            {
                return "Bad Request Server";
            }
            if (statusCode == 401)
            {
                return "Unauthorized";
            }

            if (statusCode == 409)
            {
                return "Conflict";
            }

            return "Internal Request Server Error";
        }
    }
}