using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool isPlayerDead = false;
    
    public PlayableCarModel selectedCar;
    [SerializeField]
    public GameDataManager dataManager;

    public SavedGame savedGame;


    public enum GameState
    {
        mainMenu,
        gameOverScreen,
        gameScreen,
        gamePaused
    }
    public float metersRun;
    public int collectedCoins;
    public int totalCollectedCoins;
    public GameState currentState;
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            currentState = GameState.mainMenu;
            SceneManager.sceneLoaded += OnSceneLoaded;
            dataManager.InitializeData();
            SetPlayableCarsStatus();
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {
        
        savedGame = new SavedGame();
    }
    // Update is called once per frame
    void Update()
    {
        var carSpeed = selectedCar.prefab.GetComponent<PlayerController>().speed;
        if (currentState == GameState.gameScreen)
        {
            metersRun += Time.deltaTime * carSpeed;
          
        }
    }

    public void PauseGame()
    {
        currentState = GameState.gamePaused;
    }

    public void ResumeGame()
    {
        currentState = GameState.gameScreen;
    }

    public void ExitGame()
    {
        PowerUpsStorage powerUpStorage = FindObjectOfType<PowerUpsStorage>();

        var powerUps = powerUpStorage.GetPowerUps();

        List<int> powerUpIndex = new List<int>();
        foreach(var powerUp in powerUps)
        {
            powerUpIndex.Add(powerUp.index);
        }
        Debug.Log(savedGame);
        savedGame.powerUpIndex = powerUpIndex.ToArray();
        savedGame.metersRun = (int) metersRun;
        savedGame.carIndex = selectedCar.Id;
        savedGame.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalCoins = dataManager.getTotalCoins();
        totalCollectedCoins = totalCoins + collectedCoins;
        dataManager.setTotalCoins(totalCollectedCoins);
        dataManager.SaveCurrentGame(savedGame);
        metersRun = 0;
        collectedCoins = 0;
        SceneManager.LoadScene(0);
    }

    public void SetPlayableCarsStatus()
    {
       
        dataManager.LoadData();

        var carsData = dataManager.GetAllCars();

        for(int i = 0; i < dataManager.playableCars.Length; i++)
        {

            dataManager.playableCars[i].unlocked = carsData[i].unlocked;
            
        }
        selectedCar = dataManager.playableCars[0];
    }

    public void GameOver()
    {
        currentState = GameState.gameOverScreen;
        isPlayerDead = true;
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();
        Destroy(player.gameObject);

        //RoadModule[] roadModules = GameObject.FindObjectsOfType<RoadModule>();

        //foreach(var module in roadModules)
        //{
        //    module.speed = 0f;
        //}

        //Car[] cars = GameObject.FindObjectsOfType<Car>();

        //foreach (var car in cars)
        //{
        //    car.speed = 0f;
        //}

        dataManager.LoadData();
        int highScore = dataManager.getHighScore();

        if (metersRun > highScore)
        {
            dataManager.setHighScore((int)Math.Truncate(metersRun));
            
        }

        int totalCoins = dataManager.getTotalCoins();
        totalCollectedCoins = totalCoins + collectedCoins;
        dataManager.setTotalCoins(totalCollectedCoins);
        dataManager.SaveData();

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        var player = FindObjectOfType<PlayerController>();
        if(scene.buildIndex != 0 && player == null)
        {
            Instantiate(selectedCar.prefab, new Vector2(0, 0), Quaternion.identity);
        }
        
    }

    public void restartGame()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        DeleteSavedGameData();
        SceneManager.LoadScene(activeScene);
        isPlayerDead = false;
        currentState = GameState.gameScreen;
        metersRun = 0;
        collectedCoins = 0;
    }



    public void mainMenu()
    {
        currentState = GameState.mainMenu;
        DeleteSavedGameData();
        //0 is the index of the main menu scene
        SceneManager.LoadScene(0);
        metersRun = 0;
        collectedCoins = 0;
    }

    public void DeleteSavedGameData()
    {
        savedGame.BiomeIndex = 0;
        savedGame.SceneIndex = 1;
        savedGame.powerUpIndex = new int[0];
        savedGame.metersRun = 0;
        dataManager.DeleteSavedGameData();
    }
    public void StartSavedGame()
    {
        savedGame = dataManager.GetSavedGame();
        metersRun = savedGame.metersRun;
        selectedCar = dataManager.playableCars[savedGame.carIndex];

    }

    public List<PowerUpContainer> GetSavedPowerUps()
    {
        return dataManager.GetStoredPowerUps();
    }

    public string GetDataFileText()
    {
        return dataManager.GetDataFileText();
    }

    public event EventHandler OnLoadCarsData;
}
