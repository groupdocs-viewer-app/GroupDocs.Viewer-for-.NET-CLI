using GroupDocs.Viewer.Cli.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GroupDocs.Viewer.Cli.Tests.Utils
{
    public class ConsoleOutput : IDisposable
    {
        private StringWriter outputStringWriter;
        private TextWriter originalOutput;

        private StringWriter errorStringWriter;
        private TextWriter originalErrorOutput;

        public ConsoleOutput()
        {
            outputStringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(outputStringWriter);

            errorStringWriter = new StringWriter();
            originalErrorOutput = Console.Error;
            Console.SetError(errorStringWriter);
        }

        public string GetOuput()
        {
            return outputStringWriter.ToString();
        }

        public string GetError()
        {
            return errorStringWriter.ToString();
        }

        public void Dispose()
        {

            
            Console.SetOut(originalOutput);
            Console.SetError(originalErrorOutput);

            outputStringWriter.Dispose();
            errorStringWriter.Dispose();
            //Reporter.Reset();
        }
    }
}
