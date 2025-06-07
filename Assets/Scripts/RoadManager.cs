using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
  
    [SerializeField]
    GameObject currentModule;
    GameObject nextModule;
    [SerializeField]
    Biome[] biomes;
    Biome currentBiome;
    private float screenTop;
    private float screenBottom;
    private float halfSpriteHeigth;
    private bool isTransition = false;
    private PlayerController player;
    private float enemyInstanceCadence;
    int modulesByBiome = 0;
    int totalModulesSpawned = 0;
    public GameObject[] portals;
    public Queue<int> enemyBuffer;
    public int bufferMaxSize;

    public bool specialEventHappening;
    void Start()
    {
        enemyBuffer = new Queue<int>();
        halfSpriteHeigth = currentModule.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        halfSpriteHeigth /= 2;
        screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        player = GameObject.FindObjectOfType<PlayerController>();
        int biomeIndex = GameManager.instance.savedGame.BiomeIndex;
        currentBiome = biomes[biomeIndex];
        GameObject startingModule = currentBiome.roadModules[0];
        currentModule = Instantiate(startingModule, new Vector2(0, screenBottom + halfSpriteHeigth), Quaternion.identity);
        var moduleInfo = currentModule.GetComponent<RoadModule>();
        moduleInfo.speed = -(player.speed);
        enemyInstanceCadence = 1.4f +(((4 - player.speed) / 10) * 3);
        StartCoroutine(InstanceEnemy());
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.blueCoins >= 8)
        {
            CreateBluePortal();
        }
        if (currentModule.transform.position.y <= screenTop && !GameManager.instance.isPlayerDead)
        {

            if (isTransition)
            {
                Transform intacePosition = currentModule.transform.Find("roadEnd");
                currentModule = Instantiate(nextModule, new Vector2(0, intacePosition.position.y + halfSpriteHeigth), Quaternion.identity);
                var moduleInfo = currentModule.GetComponent<RoadModule>();
                moduleInfo.speed = -(player.speed);
                isTransition = false;

            }
            else
            {
                int nextModuleIndex = UnityEngine.Random.Range(0, currentBiome.roadModules.Length);
                Transform intacePosition = currentModule.transform.Find("roadEnd");
                currentModule = Instantiate(currentBiome.roadModules[nextModuleIndex], new Vector2(0, intacePosition.position.y + halfSpriteHeigth), Quaternion.identity);
                var moduleInfo = currentModule.GetComponent<RoadModule>();
                moduleInfo.speed = -(player.speed);
                modulesByBiome++;
                totalModulesSpawned++;
                int randomNum = UnityEngine.Random.Range(0, 60);

                if (randomNum == 0 && modulesByBiome >= 40)
                {

                    modulesByBiome = 0;
                    int nextBiomeIndex = UnityEngine.Random.Range(0, currentBiome.adyacentBiomes.Length);
                    nextModule = currentBiome.adyacentBiomes[nextBiomeIndex];
                    currentBiome = biomes[nextModule.GetComponent<RoadModule>().nextBiomeId];
                    GameManager.instance.savedGame.BiomeIndex = nextModule.GetComponent<RoadModule>().nextBiomeId;
                    isTransition = true;
                }

                
            }

          
        }
    }

    private void CreateBluePortal()
    {
        int portalIndex = GameManager.instance.GetSpecialStagesPassed();
        int portalToGet = (portalIndex % portals.Length);
        GameObject portal = portals[portalToGet];
        Vector3 topRigthScreen = new Vector3(Screen.width, Screen.height, 0);
        Vector3 topRigthCorner = Camera.main.ScreenToWorldPoint(topRigthScreen);
        Instantiate(portal, new Vector3(0, topRigthCorner.y - 0.6f , 0), Quaternion.identity);
        GameManager.instance.blueCoins = 0;
    }

    IEnumerator InstanceEnemy()
    {
        while (true)
        {
            int createEvent = UnityEngine.Random.Range(0, 1000);

            if (createEvent >= 0 && createEvent < 49 && specialEventHappening)
            {
                //To do: create a normal event

            }

            if (createEvent >= 50 && createEvent < 59 && specialEventHappening)
            {
                //To do: create a rare event
            }

            if (createEvent == 60 && specialEventHappening)
            {
                //To do: create extremely rare event
            }


            yield return new WaitForSeconds(enemyInstanceCadence);
            if(currentBiome.spawnPoints.Length > 0)
            {
                int spawnPointIndex = UnityEngine.Random.Range(0, currentBiome.spawnPoints.Length);
                
                
                int nextEnemie = UnityEngine.Random.Range(0, currentBiome.enemies.Length);
                while (enemyBuffer.Contains(nextEnemie))
                {
                    nextEnemie = UnityEngine.Random.Range(0, currentBiome.enemies.Length);
                }

                enemyBuffer.Enqueue(nextEnemie);
                if(enemyBuffer.Count > bufferMaxSize)
                {
                    enemyBuffer.Dequeue();
                }
                if (GameManager.instance.currentState == GameManager.GameState.gameScreen)
                {
                 
                    var enemy = Instantiate(currentBiome.enemies[nextEnemie], currentBiome.spawnPoints[spawnPointIndex].position, Quaternion.identity);
                    var enemyInfo = enemy.GetComponent<Car>();

                    enemyInfo.speed = -(player.speed - 1);
                }
            }

        }

    }
}
