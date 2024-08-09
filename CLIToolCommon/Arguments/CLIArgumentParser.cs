using CLIToolCommon.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CLIToolCommon.Arguments
{
    /// <summary>
    /// Class that provides a function to parse arguments from command line string array
    /// </summary>
    public static class CLIArgumentParser
    {
        /// <summary>
        /// Function that parses the command line arguments into the T class based on the layout and variable names.
        /// </summary>
        /// <param name="arguments">Array of arguments of the command line application.</param>
        /// <returns>The class with the parsed arguments.</returns>
        public static T Parse<T>(params string[] arguments) where T : new()
        {
            // Gets the type of the T and its fields
            Type tType = typeof(T);
            FieldInfo[] typeFields = tType.GetFields();

            // Create a new T instance
            T result = new();
            
            // Iterate through all the arguments
            foreach (string argument in arguments)
            {
                // Verify if the argument starts with a slash (/Arg1=myvalue)
                if (argument.StartsWith('/'))
                {
                    // Trim the slash, split the argument parts and also check to see if the argument is valid
                    string[] argumentParts = argument.TrimStart('/').Split("=");
                    if (argumentParts.Length <= 0)
                        continue;

                    // Get the argument name
                    string argumentName = argumentParts[0];
                    
                    // Try to get the corresponding field for that argument name and check if it isn't null
                    FieldInfo? argumentField = typeFields.FirstOrDefault(info => info.Name == argumentName);
                    if (argumentField is null)
                    {
                        Log.Warn($"The argument '{argumentName}' is invalid.");
                        continue;
                    }

                    // Verify if the field is a boolean argument and also if it is set as a flag notation (/Flag)
                    bool isBoolArg = argumentParts.Length < 2;
                    if (isBoolArg && argumentField.FieldType.FullName != "System.Boolean")
                    {
                        Log.Warn($"The argument '{argumentName}' was set with the flag notation, but the type in the layout was a '{argumentField.FieldType.FullName ?? "Unknown"}'.");
                        continue;
                    }

                    // Try to set the argument value and also catch possible errors
                    try
                    {
                        argumentField.SetValue(result, isBoolArg ? true : Convert.ChangeType(argumentParts[1], argumentField.FieldType));
                    }
                    catch (Exception e)
                    {
                        Log.Warn($"Error trying to set argument '{argumentName}' with the value of '{(isBoolArg ? true : argumentParts[1])}':\n{e.Message}");
                    }
                }
            }

            // Return the instance of the T with the populated arguments
            return result;
        }
    }
}
