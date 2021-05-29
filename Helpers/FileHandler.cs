using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextInlineSorter.Helpers
{
    class FileHandler
    {
        // Properties
        private string[] supportedExtensions { get; set; }


        // Constructor
        public FileHandler()
        {
            supportedExtensions = new string[] { "txt", "csv" };
        }

        
        // Methods
        #region FileExists
        /// <summary>
        /// Determines if the file is present at the provided path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool FileExists(string path)
        {
            var invalidpathChars = Path.GetInvalidPathChars();

            if (!string.IsNullOrEmpty(path) && !path.Any(x => invalidpathChars.Contains(x)))
            {
                return File.Exists(path);
            }

            return false;
        }
        #endregion

        #region GetFileLines
        /// <summary>
        /// Returns a string array of all the lines in a given file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEnumerable<string> GetFileLines(string path)
        {
            if (FileExists(path) && isValidFileExtension(Path.GetExtension(path)))
            {
                return File.ReadLines(path);
            }

            return null;
        }
        #endregion

        #region GetFileLine
        /// <summary>
        /// Reterives the n'th line of a file if it exists.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public string GetFileLine(string path, int line = 0)
        {
            if (FileExists(path) && isValidFileExtension(Path.GetExtension(path)))
            {
                var lines = File.ReadLines(path);

                return (lines.Count() <= line) ? lines.ElementAt(line) : null;
            }

            return null;
        }
        #endregion

        #region isValidFileExtension
        /// <summary>
        /// Determines if the file extension passed to the parser is supported by the parser.
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        private bool isValidFileExtension(string extension)
        {
            if (!string.IsNullOrEmpty(extension))
            {
                // Check for a leading '.' and drop it.
                // Action: Unit test.
                extension = (extension.First() == '.') ? extension.Substring(1) : extension;

                var output = supportedExtensions.Contains(extension);
                return output;
            }

            // You straight-up gave me bad input.
            return false;
        }
        #endregion
    }
}
