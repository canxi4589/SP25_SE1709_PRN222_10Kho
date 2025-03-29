using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool IsPublished { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}

