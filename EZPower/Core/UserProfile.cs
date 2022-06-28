using EZPower.Core.CLIParser;
using System;
using System.Collections.Generic;
using System.Text;
namespace EZPower
{
    [Serializable]
    public class UserProfile
    {
        string _userName;
        string _currentPath;
        string _consolePrefix;
        string _currentIP;

        public UserProfile(string userName, string currentPath, string consolePrefix, string currentIP)
        {
            _userName = userName;
            _currentPath = currentPath;
            _consolePrefix = consolePrefix;
            _currentIP = currentIP;
        }

        public string UserName { get => _userName; set => _userName = value; }
        public string CurrentPath { get => _currentPath; set => _currentPath = value; }
        public string ConsolePrefix { get => _consolePrefix; set => _consolePrefix = value; }
        public string CurrentIP { get => _currentIP; set => _currentIP = value; }

        public object LoadGameData<T>(string userProfileName)
        {
            return CLIParser.LoadJsonObject<T>( userProfileName + ".ezdat");
        }

        public void SaveGameData<T>(T data, string userProfileName)
        {
            CLIParser.SaveJson(CLIParser.ToJson(data), userProfileName + ".ezdat");
        }
    }
}
