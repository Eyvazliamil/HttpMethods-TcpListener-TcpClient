using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SharedClasses
{
    public class Car
    {
        public int Id { get; set; }
        public string? Make { get; set; } = null;
        public string? Model { get; set; } = null;
        public ushort? Year { get; set; } = null;
        public string? VIN { get; set; } = null;
        public string? Color { get; set; } = null;
    }
}
