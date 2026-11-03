using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore03.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Bio { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
