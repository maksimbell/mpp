using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Example
{
    public class TestClass
    {
        private ITracer _tracer;

        public TestClass(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void Method1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            Method2();
            _tracer.StopTrace();
        }

        public void Method2()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            Method3();
            _tracer.StopTrace(); 
        }

        public void Method3()
        {
            Method4();
        }

        public void Method4()
        {
            _tracer.StartTrace();
            Thread.Sleep(400);
            Method5();
            _tracer.StopTrace();
        }

        public void Method5()
        {
            _tracer.StartTrace();
            Thread.Sleep(500);
            _tracer.StopTrace();
        }
    }
}
