using System;
using TextInlineSorter.Helpers;

namespace TextInlineSorter
{
    class Program
    {
        // Properties
        private const char DEFAULT_DELIMITER = ',';
        private const char DEFAULT_QUALIFIER = '"';
        private static string filepath { get; set; }
        private static char delimiter { get; set; }
        private static bool isTextQualifierWrapped { get; set; }
        private static char qualifier { get; set; }


        static int Main(string[] args)
        {
            // Checking arguments were actually set.
            if (args.Length == 0)
            {
                Console.WriteLine($"Error: No input file was provided.");
                return 1;
            }


            // Handling the command line arguments.
            // Action: Unit Test
            filepath = (args.Length >= 1) ? args[0] : null;
            delimiter = (args.Length >= 2) ? args[1][0] : DEFAULT_DELIMITER;
            isTextQualifierWrapped = (args.Length >= 3) ? bool.Parse(args[2]) : false;
            qualifier = (args.Length >= 4) ? args[3][0] : DEFAULT_QUALIFIER;
            

            // Checking the input is valid.
            var lines = FileHandler.GetFileLines(filepath);
            if (lines == null)
            {
                Console.WriteLine($"Error: The file: {filepath} appears incorrectly formatted, please double check.");
                return 1;
            }


            // Sorting lines.
            var textHandler = new TextHandler(delimiter, isTextQualifierWrapped, qualifier);
            var sortedLines = textHandler.SortLines(lines);
    

            // Outputting lines.
            foreach(var sortedLine in sortedLines)
            {
                Console.WriteLine(sortedLine);
            }


            return 0;
        }
    }
}
