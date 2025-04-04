﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class BlogDTO
    {
        public string Title { get; set; } 
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string? ImageUrl { get; set; }
    }
}
