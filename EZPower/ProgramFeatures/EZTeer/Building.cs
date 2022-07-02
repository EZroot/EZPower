using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures
{
    public class Building
    {
        BuildingData _buildingData;
        public BuildingData BuildingData { get => _buildingData; }

        public Building(float popularity, float brandLevel, float brandMultiplier, float dailyIncome, int dailyCustomers, float dailyUtilitiesCost, float buildingCost)
        {
            _buildingData = new BuildingData(popularity,brandLevel,brandMultiplier,dailyIncome,dailyCustomers,dailyUtilitiesCost,buildingCost);
        }

        public BuildingData CalculateDailyWork()
        {
            return _buildingData;
        }

        public void ShowBuildingStats()
        {
            CLI.Print("Popularity: " + _buildingData.Popularity);
            CLI.Print("Brand: " + _buildingData.BrandLevel);
            CLI.Print("%: " + _buildingData.BrandMultiplier);
            CLI.Print("Daily Income: " + _buildingData.DailyIncome);
            CLI.Print("Daily Customers: " + _buildingData.DailyCustomers);
            CLI.Print("Daily Utility Cost: " + _buildingData.DailyUtilitiesCost);
            CLI.Print("Building Cost: " + _buildingData.BuildingCost);
        }
    }

    [System.Serializable]
    public class BuildingData
    {
        string _name;

        float _popularity;
        float _brandLevel;
        float _brandMultiplier;

        float _dailyIncome;
        int _dailyCustomers;

        float _dailyUtilitiesCost;
        float _buildingCost;

        public BuildingData(float popularity, float brandLevel, float brandMultiplier, float dailyIncome, int dailyCustomers, float dailyUtilitiesCost, float buildingCost)
        {
            _popularity = popularity;
            _brandLevel = brandLevel;
            _brandMultiplier = brandMultiplier;
            _dailyIncome = dailyIncome;
            _dailyCustomers = dailyCustomers;
            _dailyUtilitiesCost = dailyUtilitiesCost;
            _buildingCost = buildingCost;
        }

        public string Name { get => _name; set => _name = value; }
        public float Popularity { get => _popularity; set => _popularity = value; }
        public float BrandLevel { get => _brandLevel; set => _brandLevel = value; }
        public float BrandMultiplier { get => _brandMultiplier; set => _brandMultiplier = value; }
        public float DailyIncome { get => _dailyIncome; set => _dailyIncome = value; }
        public int DailyCustomers { get => _dailyCustomers; set => _dailyCustomers = value; }
        public float DailyUtilitiesCost { get => _dailyUtilitiesCost; set => _dailyUtilitiesCost = value; }
        public float BuildingCost { get => _buildingCost; set => _buildingCost = value; }
    }
}
