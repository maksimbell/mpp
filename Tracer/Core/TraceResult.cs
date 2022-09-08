using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        public long Elapsed;

        public TraceResult(long elapsed)
        {
            Elapsed = elapsed;
        }
    }
}
