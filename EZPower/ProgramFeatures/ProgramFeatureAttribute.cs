using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ProgramFeatureAttribute : System.Attribute
    {
        string _description;
        string _programName;

        public string ProgramName { get => _programName; }
        public string Description { get => _description; }

        public ProgramFeatureAttribute(string programName, string description)
        {
            _description = description;
            _programName = programName;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ProgramFeatureArgsAttribute : System.Attribute
    {
        string _functionName;
        char _key;
        string[] _parameters;
        string _description;

        public ProgramFeatureArgsAttribute(string functionName, char key, string description,params string[] parameters)
        {
            _functionName = functionName;
            _key = key;
            _parameters = parameters;
            _description = description;
        }

        public string FunctionName { get => _functionName; }
        public char Key { get => _key; }
        public string Description { get => _description; }
        public string[] Parameters { get => _parameters; set => _parameters = value; }

    }

    public struct ProgramArgs
    {
        public char Key;
        public string Description;
    }
}
