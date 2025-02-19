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

    void Start()
    {

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
        
        
    }

    private void FixedUpdate()
    {
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
                int nextModuleIndex = Random.Range(0, currentBiome.roadModules.Length);
                Transform intacePosition = currentModule.transform.Find("roadEnd");
                currentModule = Instantiate(currentBiome.roadModules[nextModuleIndex], new Vector2(0, intacePosition.position.y + halfSpriteHeigth), Quaternion.identity);
                var moduleInfo = currentModule.GetComponent<RoadModule>();
                moduleInfo.speed = -(player.speed);
                modulesByBiome++;
                int randomNum = Random.Range(0, 60);

                if (randomNum == 0 && modulesByBiome >= 35)
                {

                    
                    modulesByBiome = 0;
                    int nextBiomeIndex = Random.Range(0, currentBiome.adyacentBiomes.Length);
                    nextModule = currentBiome.adyacentBiomes[nextBiomeIndex];
                    currentBiome = biomes[nextModule.GetComponent<RoadModule>().nextBiomeId];
                    GameManager.instance.savedGame.BiomeIndex = nextModule.GetComponent<RoadModule>().nextBiomeId;
                    isTransition = true;
                }
            }
        }

        
    }

    IEnumerator InstanceEnemy()
    {
        while (true)
        {
            int createEvent = Random.Range(0, 1000);

            if (createEvent >= 0 && createEvent < 49)
            {
                //To do: create a normal event

            }

            if (createEvent >= 50 && createEvent < 59)
            {
                //To do: create a rare event
            }

            if (createEvent == 60)
            {
                //To do: create extremely rare event
            }


            yield return new WaitForSeconds(enemyInstanceCadence);
            if(currentBiome.spawnPoints.Length > 0)
            {
                int spawnPointIndex = Random.Range(0, currentBiome.spawnPoints.Length);
                int nextEnemie = Random.Range(0, currentBiome.enemies.Length);
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
