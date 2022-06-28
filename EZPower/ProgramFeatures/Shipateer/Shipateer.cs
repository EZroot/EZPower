using EZPower.Core.CLIParser;
using System;
using System.Collections.Generic;
using System.Text;

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
        public override void Start()
        {
            Player player = new Player(
                new PlayerProfileStats("Dillon", 100, 10),
                new PlayerCombatStats(1f, 1f, 1f, 1f),
                new PlayerHistoryStats(0, "", 0, 0, 0, 0),
                new PlayerMoneyStats(0f, 0f)
                );

            Gym basicGym = new Gym(new GymData(1.1f, 1.1f, 1.1f, 1.1f));
            Gym intermediateGym = new Gym(new GymData(1.5f, 1.5f, 1.5f, 1.5f));
            Gym advancedGym = new Gym(new GymData(2f, 2f, 2f, 2f));

            Building shack = new Building(0,1,0.1f,0f,0,100f,5000);
        }

        [ProgramFeatureArgs("HelpText", 'h',"Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

        [ProgramFeatureArgs("BuyBusiness",'b',"Buy a local business", "businessName")]
        public void BuyBusiness(string businessName)
        {
            CLI.Print("Business bought! "+businessName);
        }

    }
}
