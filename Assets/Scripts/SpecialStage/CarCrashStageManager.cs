using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CarCrashStageManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] cars;
    [SerializeField]
    GameObject[] leftCarSpawnPoints;
    [SerializeField]
    GameObject[] rightCarSpawnPoints;
    [SerializeField]
    GameObject ItemSpawner;
    [SerializeField]
    int CarsGoal;
    [SerializeField]
    float limitTimeInSeconds;
    [SerializeField]
    TMP_Text scoreText;
    [SerializeField]
    TMP_Text remainingTimeText;
    [SerializeField]
    GameObject prize;
    [SerializeField]
    float spawnCadence;
    [SerializeField]
    float carSpeed;

    [SerializeField]
    GameObject powerUpToSpawn;

    [SerializeField]
    float spawnPowerUpCadence;
    [SerializeField]
    GameObject losePanel;

    int totalSpawnPoints;

    private int spawnCarIndex;

    private int collectedPoints;

    private float remainingTime;

    private float spawnPowerUpTimer;
    
    [SerializeField]
    private GameObject CurrentPowerUp;

    private bool passed = false;
    [SerializeField]
    GameObject substitute;

    void Start()
    {
        collectedPoints = 0;
        scoreText.text = "00/" + CarsGoal;
        remainingTimeText.text = limitTimeInSeconds.ToString();
        totalSpawnPoints = leftCarSpawnPoints.Length + rightCarSpawnPoints.Length;
        spawnCarIndex = 0;
        remainingTime = limitTimeInSeconds;
        spawnPowerUpTimer = spawnPowerUpCadence;
        PlayerController player = FindObjectOfType<PlayerController>();
        player.OnGetPowerUp += OnGetPowerUp;
        losePanel.transform.localScale = Vector3.zero;
        StartCoroutine(SpawnCars());
    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.instance.currentState == GameManager.GameState.gameScreen)
        {
            if(remainingTime >= 0)
            {
                remainingTime -= Time.deltaTime;
                remainingTimeText.text = Math.Truncate(remainingTime).ToString();
            }
           

            spawnPowerUpTimer -= Time.deltaTime;

            if(spawnPowerUpTimer <= 0 && CurrentPowerUp == null)
            {
                
                CurrentPowerUp = Instantiate(powerUpToSpawn, ItemSpawner.transform.position, Quaternion.identity);

            }

            if (remainingTime <= 0 && !passed)
            {
                if(collectedPoints >= CarsGoal)
                {

                    int carId = prize.GetComponent<Plains>().carModel.Id;
                    SpecialRoadModule roadModule = FindObjectOfType<SpecialRoadModule>();

                    bool carIsAvailable = GameManager.instance.IsCarAvailable(carId);

                    if (carIsAvailable)
                    {
                        spawnPowerUpTimer = 5;
                        Destroy(CurrentPowerUp);
                        CurrentPowerUp  = Instantiate(substitute, ItemSpawner.transform.position, Quaternion.identity);
                        passed = true;
                        return;
                    }
                    spawnPowerUpTimer = 5;
                   Destroy(CurrentPowerUp);
                   CurrentPowerUp = Instantiate(prize, ItemSpawner.transform.position, Quaternion.identity);
                    passed = true;
                }
                else
                {
                    losePanel.transform.LeanScale(Vector3.one, 0.3f);
                }
               
            }


        }
        
    }

    public void OnGetPowerUp(PowerUpContainer powerUp)
    {
        CurrentPowerUp = null;
        spawnPowerUpTimer = spawnPowerUpCadence;
    }


    private IEnumerator SpawnCars()
    {

        while(spawnCarIndex < cars.Length)
        {
            if(GameManager.instance.currentState == GameManager.GameState.gameScreen && remainingTime > 0)
            {
                int randomSpawnPoint = UnityEngine.Random.Range(0, totalSpawnPoints);

                if (randomSpawnPoint < leftCarSpawnPoints.Length)
                {
                    GameObject currentCar = Instantiate(cars[spawnCarIndex],
                        leftCarSpawnPoints[randomSpawnPoint].transform.position,
                        Quaternion.Euler(new Vector3(0, 0, -90)));

                    Car carScript = currentCar.GetComponent<Car>();
                    carScript.Xspeed = carSpeed;
                    spawnCarIndex++;
                }
                else
                {

                    int fixedSpawnPoint = randomSpawnPoint - leftCarSpawnPoints.Length;
                    GameObject currentCar = Instantiate(cars[spawnCarIndex],
                        rightCarSpawnPoints[fixedSpawnPoint].transform.position,
                        Quaternion.Euler(new Vector3(0, 0, 90)));

                    Car carScript = currentCar.GetComponent<Car>();
                    carScript.Xspeed = -carSpeed;
                    spawnCarIndex++;
                }
                
                spawnCarIndex++;
                yield return new WaitForSeconds(spawnCadence);
            }

            yield return null;
        }
    } 

    public void OnGetPoints(int points)
    {
        collectedPoints += points;
        Debug.Log(collectedPoints);
        Debug.Log("collectedPoints");

        scoreText.text = string.Concat(collectedPoints, "/", CarsGoal);
    }

    public void GoBackToGame()
    {
        losePanel.LeanScale(Vector3.zero, 0.3f);
        GameManager.instance.ChangeScene(1);
    }

}
