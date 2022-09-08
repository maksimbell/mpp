namespace Tracer
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomTracer tracer = new CustomTracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();

            Console.WriteLine(tracer.GetTraceResult().Elapsed + " ms");
        }
    }
}