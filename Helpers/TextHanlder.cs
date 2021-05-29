using System.Collections.Generic;
using System.Linq;

namespace TextInlineSorter.Helpers
{
    class TextHandler
    {
        // Properties
        public char Delimiter { get; set; }
        public bool IsTextQualifierWrapped { get; set; }
        public char Qualifier { get; set; }


        // Constructor
        public TextHandler(char delimiter = ',', bool isTextQualifierWrapped = false, char qualifier = '"')
        {
            Delimiter = delimiter;
            IsTextQualifierWrapped = isTextQualifierWrapped;
            Qualifier = qualifier;
        }


        // Methods
        #region IsLineWellFormed
        /// <summary>
        /// Determins if a given string is well formed with respect to Qualifiers etc.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool IsLineWellFormed(string line)
        {
            // null, we know if is bad.
            if (line == null)
            {
                return false;
            }

            // Blank, we know it is good (silly, but trues).
            if (line == string.Empty)
            {
                return true;
            }

            // If the text should be qualified, we know we have some extra checking.
            if (IsTextQualifierWrapped)
            {
                // Qualifier count is uneven, something is up.
                if (line.Where(x => x == Qualifier).Count() % 2 != 0)
                {
                    return false;
                }

                // Action: Think about nested Qualifiers, you would need to check the qualifiers are bordered by delimiters.
                // Action: Think about nested Delimeter, Qualifier combos :D.
            }


            return true;
        }
        #endregion
        #region LineParser
        /// <summary>
        /// Parses a single string into its deliminated counterpart.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private IEnumerable<string> LineParser(string line)
        {
            if (IsLineWellFormed(line))
            {
                // Dropout white space around Qualifiers to make it easier.
                // Action: Implement the Qualifier parsing.
                var output = line.Split(Delimiter).Select(x => x.Trim());
                return output;
            }

            
            return new List<string>();
        }
        #endregion
        #region SortLine
        /// <summary>
        /// Sorts a single string alphabetically.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public string SortLine(string line)
        {
            if (!string.IsNullOrEmpty(line))
            {
                var lineParsed = LineParser(line);
                var output = lineParsed.OrderBy(x => x).ToList();

                // Drop the Qualifier back in if it is needed.
                if (IsTextQualifierWrapped)
                {
                    output = output.Select(x => $"{Qualifier}{x}{Qualifier}").ToList();
                }

                
                return string.Join(Delimiter, output);
            }

            // Just hand back what you got.
            return line;
        }
        #endregion
        #region SortLines
        /// <summary>
        /// Sorts each string in an array of string alphabetically..
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public IEnumerable<string> SortLines(IEnumerable<string> lines)
        {
            if (lines != null && lines.Count() > 0)
            {
                var sortedLines = lines.Select(x => SortLine(x));
                return sortedLines;
            }

            // Just hand back what you got.
            return lines;
        }
        #endregion
    }
}
