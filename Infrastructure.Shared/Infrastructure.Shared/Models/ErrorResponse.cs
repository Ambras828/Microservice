﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Models
{
    public class ErrorResponse
    { 
    
            public int StatusCode { get; set; }
            public string? Message { get; set; }
            public string? TraceId { get; set; }
            public string? Details { get; set; }
        
    }
}
