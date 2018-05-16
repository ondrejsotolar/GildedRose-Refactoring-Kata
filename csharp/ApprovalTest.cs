using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace csharp
{
    [TestFixture]
    [UseReporter(typeof(NUnitReporter))]
    public class ApprovalTest
    {
        [Test]
        public void ThirtyDays()
        {
            StringBuilder fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));
            Console.SetIn(new StringReader("a\n"));

            Program.Main(new string[] { });
            String originalOutput = fakeoutput.ToString();

            fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));

            Program.NewMain();
            string newOutput = fakeoutput.ToString();

            Approvals.AssertEquals(originalOutput, newOutput);
        }
    }
}
