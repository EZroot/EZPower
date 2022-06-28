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
        public static string ToJson<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static object FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string LoadJson(string path)
        {
            if (!File.Exists(path))
            {
                Debug.Error("NO FILE EXISTS");
                return ""; 
            }

            string result = "";
            using (StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }

        public static object LoadJsonObject<T>(string path)
        {
            return FromJson<T>(LoadJson(path));
        }

        public static void SaveJson(string json, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(json);
            }
        }

        public static void ParseImmediateCommand(string command, string cliText, Action function)
        {
            string[] res = cliText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach(string s in res)
            {
                if(s.ToLower().Contains(command.ToLower()))
                {
                    function.Invoke();
                    break;
                }
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
