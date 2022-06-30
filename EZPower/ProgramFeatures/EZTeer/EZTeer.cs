using EZPower.Core.CLIParser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EZPower.ProgramFeatures
{
    //[ProgramHelper('b', "Buy a business")]
    //[ProgramHelper('o', "Run for office")]
    //[ProgramHelper('s', "Check player stats")]
    //[ProgramHelper('j', "Job")]
    //[ProgramHelper('p', "Properties")]
    //[ProgramHelper('c', "Crime")]
    [ProgramFeature("EZTeer", "Little minigame for fun.")]
    public class EZTeer : ProgramFeature, IProgramFeature
    {
        EZTeerData _programData;

        public override void Start()
        {
            _programData = new EZTeerData();
        }

        [ProgramFeatureArgs("ShowHelpText", 'h',"Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

        [ProgramFeatureArgs("BuyBusiness",'b',"Buy a local business", "businessName")]
        public void BuyBusiness(string businessName)
        {
            CLI.Print("Business bought! "+businessName);
        }

        public void Dispose()
        {
            Debug.Warn("Disposted of " + GetType().Name);
            GC.Collect();
        }

    }
}
