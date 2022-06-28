using EZPower.Core.CLIParser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EZPower.ProgramFeatures.Shipateer
{
    //[ProgramHelper('b', "Buy a business")]
    //[ProgramHelper('o', "Run for office")]
    //[ProgramHelper('s', "Check player stats")]
    //[ProgramHelper('j', "Job")]
    //[ProgramHelper('p', "Properties")]
    //[ProgramHelper('c', "Crime")]
    [ProgramFeature("Shipateer", "Little minigame for fun.")]
    public class Shipateer : ProgramFeature, IProgramFeature
    {
        ShipateerData _programData;

        public override void Start()
        {
            _programData = new ShipateerData();
        }

        [ProgramFeatureArgs("ShowHelpText", 'h',"Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

        [ProgramFeatureArgs("SaveJsonData", 's', "Save data")]
        public void SaveJsonData()
        {
            base.SaveGameData(_programData, GetType().Name);
        }

        [ProgramFeatureArgs("LoadJsonData", 'l', "Load data")]
        public void LoadJsonData()
        {
            ShipateerData da = (ShipateerData)base.LoadGameData<ShipateerData>(GetType().Name);
            da.Player.ShowPlayerStats();
        }

        [ProgramFeatureArgs("BuyBusiness",'b',"Buy a local business", "businessName")]
        public void BuyBusiness(string businessName)
        {
            CLI.Print("Business bought! "+businessName);
        }

    }
}
