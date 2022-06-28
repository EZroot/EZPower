using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EZPower.ProgramFeatures; 
using Newtonsoft.Json;

namespace EZPower.Core.CLIParser
{
    public static class CLIParser
    {
        public static string ToJson(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static object FromJson(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

        public static async Task SaveJson(string json, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                await sw.WriteLineAsync(json);
            }
        }

        public static object ParseCreateFeature(string featureName)
        {
            return Activator.CreateInstance(Type.GetType(featureName));
        }

        public static string ParseFeatureName(string cliText)
        {
            return cliText.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
        }

        public static void ParseFeatureArgs(string cliText, dynamic feature)
        {
            string[] args = cliText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Dictionary<char, List<string>> keyAndParams = new Dictionary<char, List<string>>();

            //get the user keys and parameters
            for(int i =0; i < args.Length;i++)
            {
                //key
                if (args[i].Contains('-'))
                {
                    List<string> parameters = new List<string>();
                    //gather params
                    for(int j = i+1;j<args.Length;j++)
                    {
                        if(args[j].Contains('-'))
                        {
                            break;
                        }
                        parameters.Add(args[j]);
                    }
                    //2nd character in our 'key' (eg. -p will now be p)
                    keyAndParams.Add(args[i][1], parameters);
                }
            }

            //get method associated with key
            //execute it and its params

            List<FeatureArgumentTemplate> argList = new List<FeatureArgumentTemplate>();

            ProgramFeatureInfo[] info = Reflect.GetAllProgramFeatures();
            string featureName = ParseFeatureName(cliText);
            foreach(ProgramFeatureInfo i in info)
            {
                if (i.ProgramName.ToLower().Contains(featureName.ToLower()))
                {
                    //foreach method
                    foreach(ProgramFeatureHelpInfo hi in i.HelpArgs)
                    {
                        //foreach key and params
                        foreach(KeyValuePair<char,List<string>> kvp in keyAndParams)
                        {
                            //if keys match
                            if(hi.Key==kvp.Key)
                            {
                                hi.Function.Invoke(feature, kvp.Value.ToArray());
                                continue;
                            }
                        }
                    }
                }
            }

            
        }
    }
}
