using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures.Shipateer
{
    [System.Serializable]
    public class ShipateerData
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

        Building shack = new Building(0, 1, 0.1f, 0f, 0, 100f, 5000);

        public Player Player { get => player; set => player = value; }
        public Gym BasicGym { get => basicGym; set => basicGym = value; }
        public Gym IntermediateGym { get => intermediateGym; set => intermediateGym = value; }
        public Gym AdvancedGym { get => advancedGym; set => advancedGym = value; }
        public Building Shack { get => shack; set => shack = value; }
    }
}
