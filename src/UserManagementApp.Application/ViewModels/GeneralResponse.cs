using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementApp.Application.ViewModels
{
    public class GeneralResponse
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = "Success";
        public object? Data { get; set; } = null;
        public Dictionary<string, string[]>? Errors { get; set; } = null;
    }
}