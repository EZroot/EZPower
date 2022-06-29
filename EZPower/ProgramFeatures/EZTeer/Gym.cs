using EZPower.Core.CLIParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures.Shipateer
{
    public class Gym
    {
        GymData _gymData;

        public Gym(GymData gymData)
        {
            _gymData = gymData;
        }

        public GymData GymData { get => _gymData; }

        public GymTrainingExp CalculateXP(PlayerCombatStats stats)
        {
            return new GymTrainingExp
            {
                StrengthXp = stats.Strength * _gymData.StrengthXpModifier,
                DefenseXp = stats.Defense * _gymData.DefenseXpModifier,
                SpeedXp = stats.Speed * _gymData.SpeedXpModifier,
                DexterityXp = stats.Dexterity * _gymData.DexterityXpModifier
            };
        }

        public void ShowGymStats()
        {
            CLI.Print(_gymData.StrengthXpModifier);
            CLI.Print(_gymData.DefenseXpModifier);
            CLI.Print(_gymData.SpeedXpModifier);
            CLI.Print(_gymData.DexterityXpModifier);
        }
    }

    [System.Serializable]
    public class GymData
    {
        float _strengthXpModifier;
        float _defenseXpModifier;
        float _speedXpModifier;
        float _dexterityXpModifier;

        public GymData(float strengthXpModifier, float defenseXpModifier, float speedXpModifier, float dexterityXpModifier)
        {
            _strengthXpModifier = strengthXpModifier;
            _defenseXpModifier = defenseXpModifier;
            _speedXpModifier = speedXpModifier;
            _dexterityXpModifier = dexterityXpModifier;
        }

        public float StrengthXpModifier { get => _strengthXpModifier; set => _strengthXpModifier = value; }
        public float DefenseXpModifier { get => _defenseXpModifier; set => _defenseXpModifier = value; }
        public float SpeedXpModifier { get => _speedXpModifier; set => _speedXpModifier = value; }
        public float DexterityXpModifier { get => _dexterityXpModifier; set => _dexterityXpModifier = value; }
    }

    public struct GymTrainingExp
    {
        public float StrengthXp;
        public float DefenseXp;
        public float SpeedXp;
        public float DexterityXp;
    }
}
