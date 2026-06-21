using System;
using System.Collections.Generic;
using System.Text;

namespace KasirKafe_Kel6.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
