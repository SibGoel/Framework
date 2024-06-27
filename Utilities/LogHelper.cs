using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utilities
{
    public class LogHelper
    {
        private static readonly string logDirectory = Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.Parent?.FullName + "\\Logs";
        
        public static string CreateLogFile(string logFileName)
        {
            // Create log directory if it doesn't exist
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Generate log file path with timestamp
            string logFilePath = Path.Combine(logDirectory, $"{logFileName}_{DateTime.Now:dd.MM.yyyy_HHmmss}.log");

            try
            {
                // Create new log file
                using (StreamWriter writer = File.CreateText(logFilePath))
                {
                    writer.WriteLine($"Log file created at: {DateTime.Now}");
                    writer.WriteLine("------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating log file: {ex.Message}");
                throw; // Rethrow exception to propagate it
            }

            return logFilePath; // Return the path of the created log file
        }

        public static void DeleteLogFile(IWebDriver driver)
        {
            try
            {
                // Check if log file exists
                if (Directory.Exists(logDirectory))
                {
                    string[] logFiles = Directory.GetFiles(logDirectory);

                    // Delete each file
                    foreach (string file in logFiles)
                    {
                        File.Delete(logDirectory);
                        Console.WriteLine($"Deleted file: {file}");
                    }
                    Console.WriteLine("Deleted all log file");
                }
                else
                {
                    Console.WriteLine("Log files not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting log file: {ex.Message}");
                throw; // Rethrow exception to propagate it
            }
        }

        public static void WriteLog(string logFilePath, string message)
        {
            try
            {
                // Append message to the log file
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
                throw; // Rethrow exception to propagate it
            }
        }
    }
}
