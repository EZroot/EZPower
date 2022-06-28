using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EZPower.ProgramFeatures
{
    public class FeatureArgumentTemplate
    {
        char _key;
        string[] _parameters;
        MemberInfo _method;

        public FeatureArgumentTemplate(char key, string[] parameters, MemberInfo method)
        {
            _key = key;
            _parameters = parameters;
            _method = method;
        }

        public char Key { get => _key; set => _key = value; }
        public string[] Parameters { get => _parameters; set => _parameters = value; }
        public MemberInfo Method { get => _method; set => _method = value; }
    }
}
