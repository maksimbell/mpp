using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator.Tests
{
    [TestClass]
    public class ParserTests
    {
        internal static CompilationUnitSyntax inputTree = null;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var code = @"
            namespace TestingNamespace {
                public class FirstClass
                {
                    public void Generate(int type) { }
                    public void GetGeneratingType() { }
                }
    
                public class SecondClass
                {
                    private void Hash(string password) { }
                    public void Calculate() { }
                }

                public class ThirdClass
                {
                    private static void Decipher(string password) { }
                    private void Encipher(string coded) { }
                }
            }";

            inputTree = SyntaxParser.Parse(code);
        }


        [TestMethod]
        public void TestMethod01()
        {
            TestParsedClassesDeclarations();
            TestParsedMethodsDeclarations();
        }

        public void TestParsedClassesDeclarations()
        {
            var classDeclarations = SyntaxParser.GetClassDeclarations(inputTree);

            Assert.AreEqual(3, classDeclarations.Count());
            Assert.AreEqual("FirstClass", classDeclarations.ElementAt(0).Identifier.ValueText);
            Assert.AreEqual("SecondClass", classDeclarations.ElementAt(1).Identifier.ValueText);
            Assert.AreEqual("ThirdClass", classDeclarations.ElementAt(2).Identifier.ValueText);
        }

        public void TestParsedMethodsDeclarations()
        {
            var methodsDeclarations = SyntaxParser.GetPublicMethodsDeclarations(
                SyntaxParser.GetClassDeclarations(inputTree).ElementAt(0));

            Assert.AreEqual(2, methodsDeclarations.Count());
            Assert.AreEqual("Generate", methodsDeclarations.ElementAt(0).Identifier.ValueText);
            Assert.AreEqual("GetGeneratingType", methodsDeclarations.ElementAt(1).Identifier.ValueText);

            methodsDeclarations = SyntaxParser.GetPublicMethodsDeclarations(
                SyntaxParser.GetClassDeclarations(inputTree).ElementAt(1));

            Assert.AreEqual(1, methodsDeclarations.Count());
            Assert.AreEqual("Calculate", methodsDeclarations.ElementAt(0).Identifier.ValueText);

            methodsDeclarations = SyntaxParser.GetPublicMethodsDeclarations(
                SyntaxParser.GetClassDeclarations(inputTree).ElementAt(2));
            Assert.AreEqual(0, methodsDeclarations.Count());
        }
    }

    [TestClass]
    public class GeneratorTetsts
    {
        internal static CompilationUnitSyntax outputTree = null;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            var generatedCode = TestsGenerator.GenerateTestCode(ParserTests.inputTree);
            outputTree = SyntaxParser.Parse(generatedCode.First());
        }

        [TestMethod]
        public void TestMethod06()
        {
            TestGeneratedClassesDeclarations();
            TestGeneratedMethodsDeclarations();
        }

        public void TestGeneratedClassesDeclarations()
        {
            var classDeclarations = SyntaxParser.GetClassDeclarations(outputTree);

            Assert.AreEqual(1, classDeclarations.Count());
        }

        public void TestGeneratedMethodsDeclarations()
        {
            var methodsDeclarations = SyntaxParser.GetPublicMethodsDeclarations(
               SyntaxParser.GetClassDeclarations(outputTree).First());
            Assert.AreEqual("Test_FirstClass_Generate_Method", methodsDeclarations.ElementAt(0).Identifier.ValueText);
            Assert.AreEqual("Test_FirstClass_GetGeneratingType_Method", methodsDeclarations.ElementAt(1).Identifier.ValueText);
            Assert.AreEqual("Test_SecondClass_Calculate_Method", methodsDeclarations.ElementAt(2).Identifier.ValueText);
        }
    }
}