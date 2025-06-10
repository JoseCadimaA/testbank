using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Application.DTOs
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public dynamic? Data { get; set; }
        public string? Message { get; set; }
        public int? StatusCode { get; set; }

        public ApiResponse(bool success, dynamic? data, string? message, int? statusCode)
        {
            Success = success;
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }

        public static ApiResponse Ok(dynamic data, string? message = null) =>
            new ApiResponse(true, data, message, 200);

        public static ApiResponse Created(dynamic data, string? message = null) =>
            new ApiResponse(true, data, message, 201);

        public static ApiResponse BadRequest(string message) =>
            new ApiResponse(false, default, message, 400);

        public static ApiResponse NotFound(string message) =>
            new ApiResponse(false, default, message, 404);

        public static ApiResponse Error(string message, int? statusCode = 500) =>
            new ApiResponse(false, default, message, statusCode);
    }
}
