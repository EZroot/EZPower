﻿using System;
using System.Collections.Generic;
using System.Text;
using EZPower.Core.CLIParser;
using Newtonsoft.Json;

namespace EZPower.ProgramFeatures.Shipateer
{
    public class Player
    {
        PlayerData _playerData;
        public PlayerData PlayerData { get => _playerData; }

        public Player(PlayerProfileStats playerProfileStats, PlayerCombatStats playerCombatStats, PlayerHistoryStats playerHistoryStats, PlayerMoneyStats playerMoneyStats)
        {
            _playerData = new PlayerData(playerProfileStats, playerCombatStats,  playerHistoryStats, playerMoneyStats);
        }

        public PlayerData TrainPlayerStats(Gym gym)
        {
            GymTrainingExp xp = gym.CalculateXP(_playerData.PlayerCombatStats);
            _playerData.PlayerCombatStats.Strength += xp.StrengthXp;
            _playerData.PlayerCombatStats.Defense += xp.DefenseXp;
            _playerData.PlayerCombatStats.Speed += xp.SpeedXp;
            _playerData.PlayerCombatStats.Dexterity += xp.DexterityXp;
            CLI.Print("Trained player stats! Gained " + xp.StrengthXp + xp.DefenseXp + xp.SpeedXp + xp.DexterityXp+" xp!");
            return _playerData;
        }

        public void ShowPlayerStats()
        {
            CLI.Print(_playerData.PlayerProfileStats.Name);
            CLI.Print(_playerData.PlayerProfileStats.Energy);
            CLI.Print(_playerData.PlayerProfileStats.Nerve);

            CLI.Print(_playerData.PlayerHistoryStats.AgeInDays);
            CLI.Print(_playerData.PlayerHistoryStats.ActiveAge);
            CLI.Print(_playerData.PlayerHistoryStats.CrimesCommited);
            CLI.Print(_playerData.PlayerHistoryStats.TimesWorked);
            CLI.Print(_playerData.PlayerHistoryStats.TimesTrained);
            CLI.Print(_playerData.PlayerHistoryStats.BuildingsBought);

            CLI.Print(_playerData.PlayerCombatStats.Strength);
            CLI.Print(_playerData.PlayerCombatStats.Defense);
            CLI.Print(_playerData.PlayerCombatStats.Dexterity);
            CLI.Print(_playerData.PlayerCombatStats.Speed);

            CLI.Print(_playerData.PlayerMoneyStats.CurrentMoney);
            CLI.Print(_playerData.PlayerMoneyStats.TotalMoneyMade);

            /*
             * 
        string _name;
        int _ageInDays;
        string _activeAge;
        int _crimesCommited;
        int _timesWorked;
        int _timesTrained;
        int _buildingsBought;
             */
        }
    }

    public class PlayerData
    {
        PlayerProfileStats _playerProfileStats;
        PlayerCombatStats _playerCombatStats;
        PlayerHistoryStats _playerHistoryStats;
        PlayerMoneyStats _playerMoneyStats;

        public PlayerData(PlayerProfileStats playerProfileStats, PlayerCombatStats playerCombatStats, PlayerHistoryStats playerHistoryStats, PlayerMoneyStats playerMoneyStats)
        {
            _playerProfileStats = playerProfileStats;
            _playerCombatStats = playerCombatStats;
            _playerHistoryStats = playerHistoryStats;
            _playerMoneyStats = playerMoneyStats;
        }

        public PlayerProfileStats PlayerProfileStats { get => _playerProfileStats; set => _playerProfileStats = value; }
        public PlayerCombatStats PlayerCombatStats { get => _playerCombatStats; set => _playerCombatStats = value; }
        public PlayerHistoryStats PlayerHistoryStats { get => _playerHistoryStats; set => _playerHistoryStats = value; }
        public PlayerMoneyStats PlayerMoneyStats { get => _playerMoneyStats; set => _playerMoneyStats = value; }
    }

    public class PlayerProfileStats
    {
        string _name;
        int _energy;
        int _nerve;

        public PlayerProfileStats(string name, int energy, int nerve)
        {
            _name = name;
            _energy = energy;
            _nerve = nerve;
        }

        public string Name { get => _name; set => _name = value; }
        public int Energy { get => _energy; set => _energy = value; }
        public int Nerve { get => _nerve; set => _nerve = value; }
    }

    public class PlayerCombatStats
    {
        float _strength; //inc dmg
        float _defense; //lower dmg
        float _speed; //speed of att
        float _dexterity; //speed of dodge?

        public PlayerCombatStats(float strength, float defense, float speed, float dexterity)
        {
            _strength = strength;
            _defense = defense;
            _speed = speed;
            _dexterity = dexterity;
        }

        public float Strength { get => _strength; set => _strength = value; }
        public float Defense { get => _defense; set => _defense = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float Dexterity { get => _dexterity; set => _dexterity = value; }
    }

    public class PlayerHistoryStats
    {

        int _ageInDays;
        string _activeAge;
        int _crimesCommited;
        int _timesWorked;
        int _timesTrained;
        int _buildingsBought;

        public PlayerHistoryStats(int ageInDays, string activeAge, int crimesCommited, int timesWorked, int timesTrained, int buildingsBought)
        {
            _ageInDays = ageInDays;
            _activeAge = activeAge;
            _crimesCommited = crimesCommited;
            _timesWorked = timesWorked;
            _timesTrained = timesTrained;
            _buildingsBought = buildingsBought;
        }

        public int AgeInDays { get => _ageInDays; set => _ageInDays = value; }
        public string ActiveAge { get => _activeAge; set => _activeAge = value; }
        public int CrimesCommited { get => _crimesCommited; set => _crimesCommited = value; }
        public int TimesWorked { get => _timesWorked; set => _timesWorked = value; }
        public int TimesTrained { get => _timesTrained; set => _timesTrained = value; }
        public int BuildingsBought { get => _buildingsBought; set => _buildingsBought = value; }
    }

    public class PlayerMoneyStats
    {
        float _currentMoney;
        float _totalMoneyMade;

        public PlayerMoneyStats(float currentMoney, float totalMoneyMade)
        {
            _currentMoney = currentMoney;
            _totalMoneyMade = totalMoneyMade;
        }

        public float CurrentMoney { get => _currentMoney; set => _currentMoney = value; }
        public float TotalMoneyMade { get => _totalMoneyMade; set => _totalMoneyMade = value; }
    }
}