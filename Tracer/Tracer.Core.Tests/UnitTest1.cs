using System.Threading;
using NUnit.Framework;
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
        public void Trace_SingleMethod_ElapsedOver100()
        {
            M1();
            Assert.That(_tracer.GetTraceResult().Threads.Count, Is.EqualTo(1));
            Assert.GreaterOrEqual(_tracer.GetTraceResult().Threads.First().Value.MethodsList[0].Elapsed, 100);
        }
    }
}