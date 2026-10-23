using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SharedClasses
{
    public class Command
    {
        public HttpMethods Method { get; set; }
        public Car? Car { get; set; }
    }
}
