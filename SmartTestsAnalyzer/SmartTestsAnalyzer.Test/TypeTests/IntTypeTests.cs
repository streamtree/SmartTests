﻿using Microsoft.CodeAnalysis;

using NUnit.Framework;

using TestHelper;



namespace SmartTestsAnalyzer.Test.TypeTests
{
    [TestFixture]
    class IntTypeTests: CodeFixVerifier
    {
        [Test]
        public void FullRange()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static int Same(int i) => i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( int.MinValue, int.MaxValue, out var value ), 
                                  () => Class1.Same( value ) );

            Assert.That( result, Is.EqualTo(value) );
        }
    }
}";

            VerifyCSharpDiagnostic( test );
        }


        [Test]
        public void AlmostFullRangeMin()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static int Same(int i) => i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( int.MinValue + 1, int.MaxValue, out var value ), 
                                  () => Class1.Same( value ) );

            Assert.That( result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Same(int)' has some missing Test Cases: Int.Value(int.MinValue)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void AlmostFullRangeMax()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static int Same(int i) => i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( int.MinValue, int.MaxValue - 1, out var value ), 
                                  () => Class1.Same( value ) );

            Assert.That( result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Same(int)' has some missing Test Cases: Int.Value(int.MaxValue)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void BadRange()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( 10, 5, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var minMax = new DiagnosticResult
                         {
                             Id = "SmartTestsAnalyzer_MinShouldBeLessThanMax",
                             Message = "Min value (10) should be less than max value (5)",
                             Severity = DiagnosticSeverity.Error,
                             Locations = new[]
                                         {
                                             new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                         }
                         };
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Range(int.MinValue, int.MaxValue)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, minMax, expected );
        }


        [Test]
        public void OneChunkInOneRange()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( 1, int.MaxValue, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Below(1)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void OneChunkInOneRangeTypedRoot()
        {
            var test = @"
using NUnit.Framework;
using SmartTests;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( SmartTest.Int.Range( 1, int.MaxValue, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Below(1)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 19, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void OneChunkInOneRangeFullRoot()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( SmartTests.SmartTest.Int.Range( 1, int.MaxValue, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Below(1)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void TwoChunksInTwoRanges()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( 1, int.MaxValue, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }

        [Test]
        public void Test2Method()
        {
            var result = RunTest( Int.Range( int.MinValue, -1, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Value(0)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 ),
                                               new DiagnosticResultLocation( "Test0.cs", 27, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void TwoChunksInOneRange()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( int.MinValue, -1 ).Range( 1, int.MaxValue, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Value(0)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void ThreeChunksInOneRange()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( int.MinValue, -1 ).Range( 1, 10 ).Range( 11, int.MaxValue, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Value(0)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void OneChunkGetValueInOneRange()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( 1, int.MaxValue ).GetValidValue( out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Below(1)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void OneChunk_RangeMinNotAConstant()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var one = 1;
            var result = RunTest( Int.Range( one, int.MaxValue, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_NotAConstant",
                               Message = "A constant is expected",
                               Severity = DiagnosticSeverity.Error,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 19, 46 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, 0, expected );
        }


        [Test]
        public void OneChunk_RangeMaxNotAConstant()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var one = 1;
            var result = RunTest( Int.Range( int.MinValue, one, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_NotAConstant",
                               Message = "A constant is expected",
                               Severity = DiagnosticSeverity.Error,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 19, 60 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, 0, expected );
        }


        [Test]
        public void OneChunk_RangeAddMinNotAConstant()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var one = 1;
            var result = RunTest( Int.Range( int.MinValue, -1 ).Range( one, int.MaxValue, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_NotAConstant",
                               Message = "A constant is expected",
                               Severity = DiagnosticSeverity.Error,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 19, 72 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, 0, expected );
        }


        [Test]
        public void OneChunk_RangeAddMaxNotAConstant()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var one = 1;
            var result = RunTest( Int.Range( int.MinValue, -1 ).Range( 1, one, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_NotAConstant",
                               Message = "A constant is expected",
                               Severity = DiagnosticSeverity.Error,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 19, 75 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, 0, expected );
        }


        [Test]
        public void OneChunkInOneRangeMissingMinMax()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( 1, 10, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Below(1).Above(10)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void OneChunkInOneRangeMissingMinMiddleMax()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Range( -10, -1 ).Range( 1, 10, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Below(-10).Value(0).Above(10)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void Above()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Above( 0, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.BelowOrEqual(0)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void Above_GetValue()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Above( 0 ).GetValidValue( out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.BelowOrEqual(0)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void AboveOrEqual()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.AboveOrEqual( 1, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Below(1)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void AboveOrEqual_GetValue()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.AboveOrEqual( 1 ).GetValidValue( out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Below(1)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void Below()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Below( 0, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.AboveOrEqual(0)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void Below_GetValue()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.Below( 0 ).GetValidValue( out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.AboveOrEqual(0)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void BelowOrEqual()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.BelowOrEqual( -1, out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Above(-1)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void BelowOrEqual_GetValue()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Inverse(int i) => 1 / i;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Int.BelowOrEqual( -1 ).GetValidValue( out var value ), 
                                  () => Class1.Inverse( value ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }
    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Inverse(int)' has some missing Test Cases: Int.Above(-1)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void GetErrorValue_Error()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Computation(int i, int j) => 1 / i * j;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Case( ""i"", Int.Range( int.MinValue, -1 ).Range( 1, int.MaxValue, out var valueI ) ) &
                                  Case( ""j"", Int.Range( 0, int.MaxValue, out var valueJ ) ),
                                  () => Class1.Computation( valueI, valueJ ) );

            Assert.That( 1 / result, Is.EqualTo(valueI * valueJ ) );
        }

        [Test]
        public void Test2Method()
        {
            var result = RunTest( Case( ""i"", Int.Range( 0, 0 ).GetErrorValue( out var value ) ),
                                  () => Class1.Computation( value, 1 ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }

    }
}";
            var expected = new DiagnosticResult
                           {
                               Id = "SmartTestsAnalyzer_MissingCases",
                               Message = "Tests for 'TestingProject.Class1.Computation(int, int)' has some missing Test Cases: i:Int.BelowOrEqual(-1).AboveOrEqual(1) & j:Int.Below(0)",
                               Severity = DiagnosticSeverity.Warning,
                               Locations = new[]
                                           {
                                               new DiagnosticResultLocation( "Test0.cs", 18, 35 ),
                                               new DiagnosticResultLocation( "Test0.cs", 28, 35 )
                                           }
                           };

            VerifyCSharpDiagnostic( test, expected );
        }


        [Test]
        public void GetErrorValue_NoError()
        {
            var test = @"
using NUnit.Framework;
using static SmartTests.SmartTest;

namespace TestingProject
{
    class Class1
    {
        public static double Computation(int i, int j) => 1 / i * j;
    }

    [TestFixture]
    public class MyTestClass
    {
        [Test]
        public void TestMethod()
        {
            var result = RunTest( Case( ""i"", Int.Range( int.MinValue, -1 ).Range( 1, int.MaxValue, out var valueI ) ) &
                                  Case( ""j"", Int.Range( int.MinValue, int.MaxValue, out var valueJ ) ),
                                  () => Class1.Computation( valueI, valueJ ) );

            Assert.That( 1 / result, Is.EqualTo(valueI * valueJ ) );
        }

        [Test]
        public void Test2Method()
        {
            var result = RunTest( Case( ""i"", Int.Range( 0, 0 ).GetErrorValue( out var value ) ),
                                  () => Class1.Computation( value, 1 ) );

            Assert.That( 1 / result, Is.EqualTo(value) );
        }

    }
}";
            VerifyCSharpDiagnostic( test );
        }


        protected override SmartTestsAnalyzerAnalyzer GetCSharpDiagnosticAnalyzer() => new SmartTestsAnalyzerAnalyzer();
    }
}