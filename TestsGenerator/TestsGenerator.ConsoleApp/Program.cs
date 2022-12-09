using System.Threading.Tasks.Dataflow;

namespace TestsGenerator.ConsoleApp
{
    internal class Program
    {
        private const string srcDir = "./../../../../SourceFiles/";
        private const string outputDir = "./../../../../OutputFiles/";
        static void Main(string[] args)
        {
            var parser = new SyntaxParser();
            var srcFileNames = Directory.GetFiles(srcDir, "*.cs").Select(Path.GetFileName);

            var blockOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 8 };

            var loadSrcBlock = new TransformBlock<string, string>(async inputFilePath =>
            {
                return await File.ReadAllTextAsync(inputFilePath);
            }, blockOptions);

            var generateTestsBlock = new TransformBlock<string, string>(async classSrcCode =>
            {
                return await TestsGenerator.GetTestCode(classSrcCode);
            }, blockOptions);

            var writeTestsIntoFile = new ActionBlock<string>(async testClassSrcCode =>
            {
                await File.WriteAllTextAsync(string.Format("{0}/{1}.cs", outputDir, Guid.NewGuid()), testClassSrcCode);
            }, blockOptions);

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            loadSrcBlock.LinkTo(generateTestsBlock, linkOptions);
            generateTestsBlock.LinkTo(writeTestsIntoFile, linkOptions);

            foreach(var fileName in srcFileNames)
            {
                loadSrcBlock.SendAsync(string.Format("{0}/{1}", srcDir, fileName));
            }
            loadSrcBlock.Complete();

            try
            {
                writeTestsIntoFile.Completion.Wait();
            }
            catch(AggregateException ae)
            {
                foreach(var e in ae.Flatten().InnerExceptions)
                {
                    if(e is FileNotFoundException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else if(e is InvalidSyntaxException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}