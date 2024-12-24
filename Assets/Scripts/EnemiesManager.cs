using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] enemies;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject[] normalEvents;
    [SerializeField]
    GameObject[] rareEvents;
    [SerializeField]
    GameObject[] extremmelyRareEvents;
    private bool eventRunning = false;
    void Start()
    {
        StartCoroutine(InstanceEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RunNormalEvent()
    {
        StopAllCoroutines();


    }

    IEnumerator InstanceEnemy()
    {
        while (true)
        {
            int createEvent = Random.Range(0, 1000);

            if(createEvent >= 0 && createEvent < 49)
            {
                //To do: create a normal event

            }

            if (createEvent >= 50 && createEvent < 59)
            {
                //To do: create a rare event
            }

            if(createEvent == 60)
            {
                //To do: create extremely rare event
            }


            yield return new WaitForSeconds(1.4f);

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            int nextEnemie = Random.Range(0, enemies.Length);
            Instantiate(enemies[nextEnemie], spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }
        
    }
}
