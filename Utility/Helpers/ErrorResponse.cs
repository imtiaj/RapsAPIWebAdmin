using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Helpers
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string DevloperMessage { get; set; }
    }
}
