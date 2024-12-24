using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class GameDataManager : MonoBehaviour
{
    string fileLocation;
    GameData gameData = new GameData();
    public PlayableCarModel[] playableCars;
    public PowerUpContainer[] powerUpsGlobalList;


    public void InitializeData()
    {
        fileLocation = Application.persistentDataPath + "/gameData.json";
        LoadData();
        InitializeCarsData();
    }

    public string GetDataFileText()
    {
        if (File.Exists(fileLocation)) { 
         return File.ReadAllText(fileLocation);
        }
        else
        {
            return "No such file";
        }
    }

    public bool IsSavedData()
    {
        return gameData.savedGame;
    }

    public void LoadData()
    {
        if (File.Exists(fileLocation))
        {
            string fileContent = File.ReadAllText(fileLocation);
            gameData = JsonUtility.FromJson<GameData>(fileContent);
            


        }
        else
        {

           
        }

        
    }

    public void SaveData()
    {
        string jsonInfo = JsonUtility.ToJson(gameData);

        File.WriteAllText(fileLocation,jsonInfo);
    }

    public void InitializeCarsData()
    {
        //esta funcion se encarga de agregar los autos nuevos que existan en 
        //el data manager en forma de scriptable objects, pero que no esten en el fichero json
        if (File.Exists(fileLocation))
        {
           
            int savedCars = gameData.CarsRegistry.Length;
           
            gameData.CarsRegistry = playableCars.Select(c =>
            new PlayableCarRegistry
            {
                CarId = c.Id,
                price = c.price,
                carName = c.carName,
                unlocked = c.Id <= savedCars - 1 ? gameData.CarsRegistry[c.Id].unlocked : c.unlocked
            }).ToArray();
            foreach (var car in gameData.CarsRegistry)
            {
                Debug.Log(car.carName);
                Debug.Log(car.unlocked);
            }
        }
        else
        {
            
            gameData.CarsRegistry = playableCars.Select(c =>
            new PlayableCarRegistry
            {
                CarId = c.Id,
                price = c.price,
                carName = c.carName,
                unlocked = c.unlocked
            }).ToArray();
        }
            

        SaveData();
    }

    public void SaveCurrentGame(SavedGame gameToSave) 
    {
        gameData.SceneIndex = gameToSave.SceneIndex;
        gameData.BiomeIndex = gameToSave.BiomeIndex;
        gameData.powerUpIndex = gameToSave.powerUpIndex;
        gameData.metersRun = gameToSave.metersRun;
        gameData.carIndex = gameToSave.carIndex;
        gameData.savedGame = true;

        SaveData();
    }

    public int getHighScore()
    {
        
        return gameData.highScore;
    }

    public void setHighScore(int newHighScore)
    {
        gameData.highScore = newHighScore;
    }

    public int getTotalCoins()
    {
        return gameData.coins;
    }

    public void setTotalCoins(int totalCoins)
    {
        gameData.coins = totalCoins;
    }

    public PlayableCarRegistry[] GetUnlockedCars()
    {

        var unlockedCars = gameData.CarsRegistry.Where(c => c.unlocked);
        return unlockedCars.ToArray();
    }

    public PlayableCarRegistry[] GetAllCars()
    {
        return gameData.CarsRegistry;
    }

    public void BuyCar(PlayableCarModel car)
    {
        gameData.coins -= car.price;
        gameData.CarsRegistry[car.Id].unlocked = true;

        SaveData();
    }

    public void DeleteSavedGameData()
    {
        gameData.savedGame = false;
        gameData.SceneIndex = 0;
        gameData.BiomeIndex = 0;
        gameData.powerUpIndex = new int[0];
        gameData.metersRun = 0;
        gameData.carIndex = 0;

        SaveData();
    }

    public SavedGame GetSavedGame()
    {
        SavedGame savedGame = new SavedGame();

        savedGame.SceneIndex = gameData.SceneIndex;
        savedGame.BiomeIndex = gameData.BiomeIndex;
        savedGame.powerUpIndex = gameData.powerUpIndex;
        savedGame.metersRun = gameData.metersRun;
        savedGame.carIndex =  gameData.carIndex;

        return savedGame;
    }

    public List<PowerUpContainer> GetStoredPowerUps()
    {

        List<PowerUpContainer> powerups = new List<PowerUpContainer>();
        foreach(var powerUpIndex in gameData.powerUpIndex)
        {
            powerups.Add(powerUpsGlobalList[powerUpIndex]);
        }

        return powerups;
    }
}
