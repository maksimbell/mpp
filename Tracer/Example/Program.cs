namespace Tracer
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomTracer tracer = new CustomTracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();

            Console.WriteLine("class: {0}\nmethod: {1}\n{2} ms", tracer.GetTraceResult().ClassName, 
                tracer.GetTraceResult().MethodName,
                tracer.GetTraceResult().Elapsed);
        }
    }
}