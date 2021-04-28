using System;
using System.Collections.Generic;
using System.IO;

namespace bmesg2csharp
{
    class Program
    {
        //
        // Call
        //
        static void Call(string[] sourceFiles)
        {
            if (sourceFiles != null && sourceFiles.Length > 0) 
            {
                foreach (string sourceFile in sourceFiles)
                {
                    CompileFile(sourceFile);
                }
            }
            else
            {
                Console.WriteLine("No source files found");
            }
        }

        //
        // Compile the given file.
        //
        static void CompileFile(string sourceFile)
        {
            Console.WriteLine("Compiling - " + sourceFile);
            string sourceContent = System.IO.File.ReadAllText(sourceFile);
            string destinationFilename = sourceFile;
            destinationFilename = destinationFilename.Substring(0, destinationFilename.LastIndexOf("."));
            destinationFilename = destinationFilename + ".cs";
            File.WriteAllText(destinationFilename, sourceContent);
        }

        //
        // Main method.
        //
        static void Main(string[] args)
        {
            Call(args);
        }
    }
}
