using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                    TranspileFile(sourceFile);
                }
            }
            else
            {
                Console.WriteLine("No source files found");
            }
        }

        //
        // Convert to nodes.
        //
        static List<Object> ConvertToNodes(String inputString)
        {
            return inputString
                .Select(character => new CharacterNode(character))
                .ToList<Object>();
        }

        //
        // Transpile the given file.
        //
        static void TranspileFile(string sourceFile)
        {
            Console.WriteLine("Transpiling - " + sourceFile);
            string sourceContent = System.IO.File.ReadAllText(sourceFile);
            string destinationFilename = sourceFile;
            destinationFilename = destinationFilename.Substring(0, destinationFilename.LastIndexOf("."));
            destinationFilename = destinationFilename + ".cs";
            List<Object> nodes = ConvertToNodes(sourceContent);
            if (nodes.Count > 0)
            {
                FileStream fileStream = new FileStream(destinationFilename, FileMode.Create);
                StreamWriter writer = new StreamWriter(fileStream);
                foreach (var node in nodes)
                {
                    writer.Write(node.ToString());
                }
                writer.Flush();
                fileStream.Flush();
                fileStream.Close();
            }
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
