using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request.Response.Middleware.FileLogger.Library.Models
{
    public class FileLoggingOptions
    {
        public string FileDirectory { get; set; }

        public string FileName { get; set; } = "logs";

        public string Extension { get; set; } = "txt";

        public bool ForceCreateDirectory { get; set; } = true;

        public bool UseJsonFormat { get; set; } = false;

        internal string GetFullFileName() => $"{FileName}.{Extension}";

        internal string GetFullFilePath() => Path.Combine(FileDirectory, GetFullFileName());

        internal void ValidatePath()
        {
            try
            {
                if (ForceCreateDirectory)
                    Directory.CreateDirectory(FileDirectory);
                else
                {
                    if (Directory.Exists(FileDirectory))
                        throw new Exception($"{FileDirectory} not found");
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
        }
    }
}
