using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] LoadingScreens loadingScreenManager;
    public bool isPlayerDead = false;

    public bool specialStage = false;
    
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
    public int blueCoins;
    public int redCoins;
    public int totalCollectedCoins;
    public GameState currentState;

    public int coinsToContinueGame = 10;
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
            totalCollectedCoins = dataManager.getTotalCoins();
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
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        currentState = GameState.gameScreen;
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        Debug.Log("Special stage: " + specialStage);
        if (!specialStage)
        {
            SaveCurrentGame();
        }

        blueCoins = 0;
        metersRun = 0;
        collectedCoins = 0;
        ChangeScene(0);
    }

    public void SaveCurrentGame()
    {

        PowerUpsStorage powerUpStorage = FindObjectOfType<PowerUpsStorage>();

        var powerUps = powerUpStorage.GetPowerUps();
        List<int> powerUpIndex = new List<int>();
        foreach (var powerUp in powerUps)
        {
            powerUpIndex.Add(powerUp.index);
        }

        Debug.Log("Biome: " + savedGame.BiomeIndex);
        savedGame.powerUpIndex = powerUpIndex.ToArray();
        savedGame.metersRun = (int)metersRun;
        savedGame.carIndex = selectedCar.Id;
        savedGame.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        savedGame.health = player.GetHealth();
        savedGame.blueCoins = blueCoins;
        savedGame.coinsToContinueGame = coinsToContinueGame;

        dataManager.setTotalCoins(totalCollectedCoins);
        dataManager.SaveCurrentGame(savedGame);
    }

    public void SetPlayableCarsStatus()
    {
       
        dataManager.LoadData();

        var carsData = dataManager.GetAllCars();

        for(int i = 0; i < dataManager.playableCars.Length; i++)
        {

            dataManager.playableCars[i].unlocked = carsData[i].unlocked;
            dataManager.playableCars[i].canBuyIt = carsData[i].canBuyIt;
        }
        selectedCar = dataManager.playableCars[0];
    }

    public void GameOver()
    {
        currentState = GameState.gameOverScreen;
        isPlayerDead = true;

        player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        

        dataManager.LoadData();
        int highScore = dataManager.getHighScore();

        if (metersRun > highScore)
        {
            dataManager.setHighScore((int)Math.Truncate(metersRun));
            
        }

        dataManager.setTotalCoins(totalCollectedCoins);
        dataManager.SaveData();

        if(metersRun > 1500 && selectedCar.Id != 0)
        {
            AdsManager.instance.interstitialAds.ShowIntertitalAd();
        }

        Time.timeScale = 0;

    }

    public void ContinueGameWithCoins()
    {
        Time.timeScale = 1;
        collectedCoins = 0;

        if (!IsAdReward)
        {
            totalCollectedCoins -= coinsToContinueGame;
            dataManager.setTotalCoins(totalCollectedCoins);
            dataManager.SaveData();
            coinsToContinueGame *= 2;
        }
        
        if(currentState == GameState.gameOverScreen)
        {
            player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            player.SetInvincibleTimer(3f);
            player.RestartHealth();
            isPlayerDead = false;
            currentState = GameState.gameScreen;
        }

        IsAdReward = false;
        
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        Debug.Log("Scene" + scene.buildIndex);

        if (scene.buildIndex == 1)
        {
            AudioManager.instance.PlayRadio();
            currentState = GameManager.GameState.gameScreen;
            
        }

        if(scene.buildIndex == 0)
        {
            AudioManager.instance.PlayMainMenu();
            AdsManager.instance.bannerAds.ShowBannerAd();
        }

        if(scene.buildIndex > 1)
        {
            AudioManager.instance.PauseMusic();
            GameManager.instance.specialStage = true;
            GameManager.instance.currentState = GameManager.GameState.gamePaused;
        }

        player = FindObjectOfType<PlayerController>();
        if (scene.buildIndex != 0 && player == null)
        {
            Instantiate(selectedCar.prefab, new Vector2(0, -2f), Quaternion.identity);
            player = FindObjectOfType<PlayerController>();
        }

        loadingScreenManager.HideLoadingScreen();
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        DeleteSavedGameData();
        SceneManager.LoadScene(activeScene);
        isPlayerDead = false;
        currentState = GameState.gameScreen;
        metersRun = 0;
        boughtItems = new List<int>();
        coinsToContinueGame = 10;
    }



    public void mainMenu()
    {
        Time.timeScale = 1;
        currentState = GameState.mainMenu;
        DeleteSavedGameData();
        //0 is the index of the main menu scene
       
        ChangeScene(0);
       
    }

    public void DeleteSavedGameData()
    {
        savedGame.BiomeIndex = 0;
        savedGame.SceneIndex = 1;
        savedGame.powerUpIndex = new int[0];
        savedGame.metersRun = 0;
        savedGame.blueCoins = 0;
        metersRun = 0;
        coinsToContinueGame = 10;

        boughtItems = new List<int>();
        savedGame.health = 0;
        dataManager.DeleteSavedGameData();
        coinsToContinueGame = 10;
        totalCollectedCoins = dataManager.getTotalCoins();
    }
    public void StartSavedGame()
    {
        savedGame = dataManager.GetSavedGame();
        metersRun = savedGame.metersRun;
        selectedCar = dataManager.playableCars[savedGame.carIndex];
        boughtItems = new List<int>();
        blueCoins = savedGame.blueCoins;
        coinsToContinueGame = savedGame.coinsToContinueGame;
        totalCollectedCoins = dataManager.getTotalCoins();

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

    public float GetMusicVolume()
    {
        return dataManager.GetMusicVolume();
    }

    public float GetSFXVolume()
    {
        return dataManager.GetSFXVolume();
    }

    public void SetMusicVolume(float volume)
    {
        dataManager.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        dataManager.SetSFXVolume(volume);
    }

    public void OnGetCarPlains(int carId)
    {
        dataManager.OnGetPlains(carId);
    }

    public void GetBlueCoin()
    {
        blueCoins++;
        OnGetBlueCoin?.Invoke(blueCoins);

    }

    public int GetSpecialStagesPassed()
    {
        return dataManager.GetSpecialStagesPassed();
    }

    public void ChangeScene(int sceneIndex)
    {
        StopAllCoroutines();
        loadingScreenManager.ChangeScene(sceneIndex);
    }

    public void DeleteAllSavedGameData()
    {
        DeleteSavedGameData();
        dataManager.DeleteAllSavedGameData();
    }

    public void PlaySpecialStageMusic()
    {
        AudioManager.instance.PlaySpecialStage();
    }

    public bool IsCarAvailable(int carIndex)
    {
        return dataManager.IsCarAvailable(carIndex);
    }

    public event EventHandler OnLoadCarsData;
    public event Action<int> OnGetBlueCoin;
    public event EventHandler OnGetAllBlueCoins;
}
