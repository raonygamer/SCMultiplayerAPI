using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CLIToolCommon.IO
{
    /// <summary>
    /// Class that provides functions for logging info on the console.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Function that writes a message on the console with a specified color.
        /// </summary>
        /// <param name="message">The message to write to the console.</param>
        /// <param name="color">The color of the message.</param>
        public static void Write(string message, ConsoleColor color)
        {
            // Save the old console color and set the new color on the foreground
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            // Write the message to Standard Output
            Console.WriteLine(message);

            // Reset the color of the foreground to the old one
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Function that writes the message to the console with a white color as a trace.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public static void Info(string message) => Write($"[Info from {Assembly.GetCallingAssembly().GetName().Name ?? "Unknown"}] {message}", ConsoleColor.White);

        /// <summary>
        /// Function that writes the message to the console with a yellow color as a warning.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public static void Warn(string message) => Write($"[Warn from {Assembly.GetCallingAssembly().GetName().Name ?? "Unknown"}] {message}", ConsoleColor.Yellow);

        /// <summary>
        /// Function that writes the message to the console with a red color as a error.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public static void Error(string message) => Write($"[Error from {Assembly.GetCallingAssembly().GetName().Name ?? "Unknown"}] {message}", ConsoleColor.Red);
    }
}
