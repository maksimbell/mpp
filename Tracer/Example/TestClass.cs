using Core;
using System.Diagnostics;
using System.Reflection;

namespace Example
{
    public class TestClass
    {
        private ITracer _tracer;

        public TestClass(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void StartTest()
        {
            List<Thread> threads = new List<Thread>();

            for(int i = 0; i < 3; i++)
            {
                Thread thread;
                switch (i)
                {
                    case 0:
                        thread = new Thread(Method1);
                        break;
                    case 1:
                        thread = new Thread(Method2);
                        break;
                    default:
                        thread = new Thread(Method3);
                        break;
                }

                threads.Add(thread);    
                thread.Start(); 
            }

            foreach(Thread thread in threads)
            {
                thread.Join();
            }
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
            Method3();
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

        public void Method6()
        {
            _tracer.StartTrace();
            Thread.Sleep(500);
            Method7();
            _tracer.StopTrace();
        }

        public void Method7()
        {
            _tracer.StartTrace();
            Thread.Sleep(500);
            _tracer.StopTrace();
        }

        public void Method8()
        {
            Method6();
            Thread.Sleep(100);
            Method6();
        }

    }
}
