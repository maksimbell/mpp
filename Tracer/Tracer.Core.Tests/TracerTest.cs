using Core;


namespace Tracer.Core.Tests
{
    public class Tests
    {
        private ITracer _tracer;

        [SetUp]
        public void Setup()
        {
            _tracer = new CustomTracer();
        }

        private void M1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
        private void M2()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            M1();
            _tracer.StopTrace();
        }

        [Test]
        public void Trace_SingleM1_ElapsedOver100()
        {
            M1();
            Assert.That(_tracer.GetTraceResult().Threads.Count, Is.EqualTo(1));
            Assert.GreaterOrEqual(_tracer.GetTraceResult().Threads.First().Value.MethodsList[0].Elapsed, 100);
        }

        [Test]
        public void Trace_SingleM2_ElapsedOver300()
        {
            M2();
            Assert.That(_tracer.GetTraceResult().Threads.Count, Is.EqualTo(1));
            Assert.GreaterOrEqual(_tracer.GetTraceResult().Threads.First().Value.MethodsList[0].Elapsed, 300);
            Assert.That(_tracer.GetTraceResult().Threads.First().Value.MethodsList[0].MethodName, Is.EqualTo(nameof(M2)));
            Assert.That(_tracer.GetTraceResult().Threads.First().Value.MethodsList[0].ChildMethods[0].MethodName, Is.EqualTo(nameof(M1)));
        }

        [Test]
        public void Trace_DoubleM1()
        {
            M1();
            M1();
            Assert.That(_tracer.GetTraceResult().Threads.Count, Is.EqualTo(1));
            Assert.That(_tracer.GetTraceResult().Threads.First().Value.MethodsList.Count, Is.EqualTo(2));
            Assert.GreaterOrEqual(_tracer.GetTraceResult().Threads.First().Value.MethodsList[0].Elapsed, 100);
            Assert.GreaterOrEqual(_tracer.GetTraceResult().Threads.First().Value.MethodsList[1].Elapsed, 100);
        }

        [Test]
        public void Trace_TwoThreads()
        {
            var thread1 = new Thread(M1);
            var thread2 = new Thread(M2);
            thread1.Start();
            thread1.Join();
            thread2.Start();
            thread2.Join();

            Assert.That(_tracer.GetTraceResult().Threads.Count, Is.EqualTo(2));
            Assert.That(_tracer.GetTraceResult().Threads.First().Value.MethodsList.Count, Is.EqualTo(1));
            Assert.That(_tracer.GetTraceResult().Threads.Last().Value.MethodsList.Count, Is.EqualTo(1));
            Assert.GreaterOrEqual(_tracer.GetTraceResult().Threads.First().Value.MethodsList[0].Elapsed, 100);
            Assert.GreaterOrEqual(_tracer.GetTraceResult().Threads.Last().Value.MethodsList[0].Elapsed, 200);
            Assert.GreaterOrEqual(_tracer.GetTraceResult().Threads.Last().Value.MethodsList[0].ChildMethods[0].Elapsed, 
                100);
        }
    }
}