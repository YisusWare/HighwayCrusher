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
                unlocked =  c.Id <= savedCars - 1 ? gameData.CarsRegistry[c.Id].unlocked : c.unlocked,
                canBuyIt = c.Id < 4 ? true : c.Id <= savedCars - 1 ? gameData.CarsRegistry[c.Id].canBuyIt : c.canBuyIt
            }).ToArray();

        }
        else
        {
          
            gameData.CarsRegistry = playableCars.Select(c =>
            new PlayableCarRegistry
            {
                CarId = c.Id,
                price = c.price,
                carName = c.carName,
                unlocked = c.Id < 1 ? true : false,
                canBuyIt = c.Id <= 3 ? true : false
            }).ToArray();


            gameData.musicVolume = 0.5f;
            gameData.sfxVolume = 0.5f;
            gameData.specialStagesPassed = 0;
            gameData.coinsToContinueGame = 10;
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
        gameData.health = gameToSave.health;
        gameData.blueCoins = gameToSave.blueCoins;
        gameData.coinsToContinueGame = gameToSave.coinsToContinueGame;

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
        gameData.health = 0;
        gameData.blueCoins = 0;
        gameData.coinsToContinueGame = 10;

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
        savedGame.health = gameData.health;
        savedGame.blueCoins = gameData.blueCoins;
        savedGame.coinsToContinueGame = gameData.coinsToContinueGame;

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

    public float GetMusicVolume()
    {
        return gameData.musicVolume;
    }

    public float GetSFXVolume()
    {
        return gameData.sfxVolume;
    }

    public void SetMusicVolume(float volume)
    {
        gameData.musicVolume = volume;

        SaveData();
    }

    public void SetSFXVolume(float volume)
    {
        gameData.sfxVolume = volume;

        SaveData();
    }

    public void OnGetPlains(int carId)
    {
        gameData.CarsRegistry[carId].canBuyIt = true;
        gameData.specialStagesPassed++;
        SaveData();
    }

    public int GetSpecialStagesPassed()
    {
        return gameData.specialStagesPassed;
    }

    public void DeleteAllSavedGameData()
    {
        gameData.savedGame = false;
        gameData.SceneIndex = 0;
        gameData.BiomeIndex = 0;
        gameData.powerUpIndex = new int[0];
        gameData.metersRun = 0;
        gameData.carIndex = 0;
        gameData.health = 0;
        gameData.coins = 0;
        gameData.highScore = 0;
        gameData.specialStagesPassed = 0;
        gameData.blueCoins = 0;
        gameData.coinsToContinueGame = 10;


        foreach (PlayableCarRegistry car in gameData.CarsRegistry)
        {

            if(car.CarId <= 4 && car.CarId > 0)
            {
                car.unlocked = false;
                car.canBuyIt = true;
            }
            if (car.CarId > 4)
            {
                car.unlocked = false;
                car.canBuyIt = false;
                
            }
        }

        SaveData();
    }


    public bool IsCarAvailable(int CarId)
    {
        PlayableCarRegistry car = gameData.CarsRegistry.Where(c => c.CarId == CarId).FirstOrDefault();
 
        return car.canBuyIt;
    }
}
