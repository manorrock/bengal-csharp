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
        static List<INode> ConvertToNodes(String inputString)
        {
            return inputString
                .Select(character => new CharacterNode(character))
                .ToList<INode>();
        }

        //
        // Phase 1 - convert "object" to KeywordNode
        //
        static List<INode> Phase1(List<INode> inputList)
        {
            List<INode> result = new List<INode>();
            KeywordNode keyword = new KeywordNode("object");
            String candidate = "";
            foreach (var node in inputList)
            {
                candidate = candidate + node.ToString();
                if (!keyword.GetKeyword().StartsWith(candidate))
                {
                    result.AddRange(ConvertToNodes(candidate));
                    candidate = "";
                }
                else if (candidate.Equals(keyword.GetKeyword()))
                {
                    result.Add(keyword);
                    candidate = "";
                }
            }
            if (candidate.Length > 0)
            {
                result.AddRange(ConvertToNodes(candidate));
            }
            return result;
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
            List<INode> nodes = ConvertToNodes(sourceContent);
            nodes = Phase1(nodes);
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
