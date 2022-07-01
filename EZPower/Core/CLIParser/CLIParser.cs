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
            path = Directory.GetCurrentDirectory() + "\\" + path + ".ezdat";
            Debug.Warn(path);

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
            path = Directory.GetCurrentDirectory() + "\\"+path+".ezdat";
            Debug.Warn(path);

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
                if(s.ToLower()==(command.ToLower()))
                {
                    function.Invoke();
                    break;
                }
            }
        }
        public static object ParseCreateFeature(string featureName)
        {
            Debug.Error(featureName);
            return Activator.CreateInstance(Type.GetType(featureName));
        }

        public static string ParseFeatureName(string cliText)
        {
            string[] f = cliText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string result = "";
            if (f.Length > 0)
            {
                result = f[0];
            }
            return result;
        }

        public static string ParseFeatureArgs(string cliText, dynamic feature)
        {
            string[] args = cliText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Dictionary<char, List<string>> keyAndParams = new Dictionary<char, List<string>>();

            //get the user keys and parameters
            for(int i =0; i < args.Length;i++)
            {
                //key
                if (args[i].Contains('-'))
                {
                    Debug.Log("key: "+args[i]);
                    List<string> parameters = new List<string>();

                    //gather params
                    for (int j = i + 1; j < args.Length; j++)
                    {
                        Debug.Log("params: " + j);
                        if (args[j].Contains('-'))
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
            string res = "";
            ProgramFeatureInfo[] info = Reflect.GetAllProgramFeatures();
            string featureName = ParseFeatureName(cliText);
            foreach(ProgramFeatureInfo i in info)
            {
                Debug.Error("Count");
                if (i.ProgramName.ToLower().Contains(featureName.ToLower()))
                {
                    Debug.Log("Program : " + i.ProgramName);

                    //foreach method
                    foreach (ProgramFeatureHelpInfo hi in i.HelpArgs)
                    {
                        Debug.Log("Function: "+hi.Function.ToString());
                        //foreach key and params
                        foreach(KeyValuePair<char,List<string>> kvp in keyAndParams)
                        {
                            //if keys match
                            if (hi.Key==kvp.Key)
                            {
                                Debug.Log("matching key! invoking... " + kvp.Value.Count);
                                //no args
                                if (kvp.Value.Count >= 1)
                                {
                                    //if we dont need args, just run
                                    if (hi.FunctionParameterType.ToLower().Contains("string[]"))
                                    {
                                        Debug.Warn("trying to invoke multiple params");
                                        res += (string)hi.Function.Invoke(feature, new object[] { kvp.Value.ToArray() });
                                    }
                                    else
                                    {

                                        Debug.Warn("trying to invoke DEFAULT params");
                                        res += (string)hi.Function.Invoke(feature, new object[] { kvp.Value[0] });
                                    }
                                }
                                else
                                {

                                    if (hi.FunctionParameterType.ToLower().Contains("string[]"))
                                    {
                                        Debug.Warn("trying to invoke multiple params");
                                        res += (string)hi.Function.Invoke(feature, new object[] { kvp.Value.ToArray() });
                                    }
                                    else if (hi.FunctionParameterType.ToLower().Contains("string"))
                                    {

                                        Debug.Warn("trying to invoke DEFAULT params");
                                        res += (string)hi.Function.Invoke(feature, new object[] { null });
                                    }
                                    else
                                    {

                                        Debug.Warn("trying to invoke no params");
                                        res += (string)hi.Function.Invoke(feature, null);
                                    }

                                }
                                res += " ";
                                continue;
                            }
                        }
                    }
                }
            }
            return res;
        }
    }
}
