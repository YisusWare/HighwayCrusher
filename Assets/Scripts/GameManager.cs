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

    private List<int> boughtItems = new List<int>();

    PlayerController player;

    [HideInInspector]
    public bool IsAdReward;

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
        
        player.gameObject.SetActive(false);

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

        if(metersRun > 1000)
        {
            AdsManager.instance.interstitialAds.ShowIntertitalAd();
        }
        
    }

    public void ContinueGameWithCoins()
    {
        collectedCoins = 0;

        if (!IsAdReward)
        {
            totalCollectedCoins -= 30;
            dataManager.setTotalCoins(totalCollectedCoins);
            dataManager.SaveData();
        }
        

        player.gameObject.SetActive(true);
        player.SetInvincibleTimer(3f);
        player.RestartHealth();

        

        isPlayerDead = false;

        currentState = GameState.gameScreen;
        Color spriteColor = new Color(255, 255, 255, 255);
        player.gameObject.GetComponent<SpriteRenderer>().color = spriteColor;

        IsAdReward = false;
        
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        player = FindObjectOfType<PlayerController>();
        if(scene.buildIndex != 0 && player == null)
        {
            Instantiate(selectedCar.prefab, new Vector2(0, 0), Quaternion.identity);
            player = FindObjectOfType<PlayerController>();
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
        boughtItems = new List<int>();
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
        boughtItems = new List<int>();
        dataManager.DeleteSavedGameData();
    }
    public void StartSavedGame()
    {
        savedGame = dataManager.GetSavedGame();
        metersRun = savedGame.metersRun;
        selectedCar = dataManager.playableCars[savedGame.carIndex];
        boughtItems = new List<int>();

    }

    public List<PowerUpContainer> GetSavedPowerUps()
    {
        return dataManager.GetStoredPowerUps();
    }

    public string GetDataFileText()
    {
        return dataManager.GetDataFileText();
    }

    public void SetBoughtItems(List<int> _boughtItems)
    {
        boughtItems = _boughtItems;
    }

    public List<PowerUpContainer> GetBoughtItems()
    {
        List<PowerUpContainer> powerups = new List<PowerUpContainer>();

        var powerUpsGlobalList = dataManager.powerUpsGlobalList;
        foreach (var powerUpIndex in boughtItems)
        {
            powerups.Add(powerUpsGlobalList[powerUpIndex]);
        }

        return powerups;
    }

    public event EventHandler OnLoadCarsData;
}
