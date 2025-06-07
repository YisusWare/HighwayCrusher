using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] items;
    void Start()
    {
        int randomIndex = Random.Range(0, items.Length);

        GameObject item =Instantiate(items[randomIndex], transform.position, Quaternion.identity);

        item.transform.SetParent(this.transform);

    }

   
}
