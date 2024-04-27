using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Entities.DTOs
{
    public class MoveEmailDto
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public string fileName { get; set; }
    }
}
