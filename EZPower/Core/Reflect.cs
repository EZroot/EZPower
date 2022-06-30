using System;
using System.Collections.Generic;
using System.Reflection;

namespace EZPower
{
    public struct ProgramFeatureHelpInfo
    {
        public char Key;
        public string Description;
        public string[] Parameters;
        public MethodInfo Function;
        public string FunctionParameterType;
    }
    public struct ProgramFeatureInfo
    {
        public string ProgramName;
        public string ProgramDescription;

        public ProgramFeatureHelpInfo[] HelpArgs;
    }

    public static class Reflect
    {
        public static ProgramFeatureInfo[] GetAllProgramFeatures()
        {
            List<ProgramFeatureInfo> featureInfoList = new List<ProgramFeatureInfo>();
            List<ProgramFeatureHelpInfo> featureHelpList = new List<ProgramFeatureHelpInfo>();

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in asm.GetTypes())
                {
                    featureHelpList.Clear();
                    if (t.IsDefined(typeof(ProgramFeatureAttribute)))
                    {
                        ProgramFeatureAttribute feature = (ProgramFeatureAttribute)t.GetCustomAttribute(typeof(ProgramFeatureAttribute));

                        MethodInfo[] membInfos = t.GetMethods();
                        foreach (MethodInfo member in membInfos)
                        {
                            ProgramFeatureArgsAttribute[] featureArgs = (ProgramFeatureArgsAttribute[])member.GetCustomAttributes(typeof(ProgramFeatureArgsAttribute));
                            foreach (ProgramFeatureArgsAttribute args in featureArgs)
                            {
                                ParameterInfo[] allparams = member.GetParameters();
                                string paramType = "null";
                                if (allparams.Length > 0)
                                {
                                    paramType = allparams[0].ParameterType.ToString();
                                }
                                ProgramFeatureHelpInfo helpinfo = new ProgramFeatureHelpInfo { 
                                    Key = args.Key,
                                    Description = args.Description,
                                    Function = member,
                                    Parameters = args.Parameters,
                                    FunctionParameterType = paramType
                                };

                                Debug.Error(member.Name + " - "+paramType);
                                featureHelpList.Add(helpinfo);
                            }
                        }

                        featureInfoList.Add(new ProgramFeatureInfo { ProgramName = feature.ProgramName, ProgramDescription = feature.Description, HelpArgs = featureHelpList.ToArray() });
                    }
                }
            }

            return featureInfoList.ToArray();
        }

        public static bool DoesFeatureExist(string featureName)
        {
            if (featureName.Length == 0) { return false; }

            ProgramFeatureInfo[] info = Reflect.GetAllProgramFeatures();
            foreach (ProgramFeatureInfo i in info)
            {
                if (i.ProgramName.ToLower().Contains(featureName.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetClassFullName(string featureName)
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in asm.GetTypes())
                {
                    if (t.IsDefined(typeof(ProgramFeatureAttribute)))
                    {
                        if (t.FullName.ToLower().Contains(featureName.ToLower()))
                        {
                            return t.FullName;
                        }
                    }
                }
            }
            return "n/a";
        }

        public static void GetClasses()
        {
            foreach(Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in asm.GetTypes())
                {
                    if (t.IsDefined(typeof(ProgramFeatureAttribute)))
                    {
                        foreach(ProgramFeatureArgsAttribute ap in t.GetCustomAttributes(typeof(ProgramFeatureArgsAttribute)))
                        {
                            Debug.Warn(ap.Key + " - "+ ap.Description);
                        }
                    }
                }
            }
        }

    }
}