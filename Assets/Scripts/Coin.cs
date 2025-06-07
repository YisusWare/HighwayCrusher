using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool redCoin;
    public bool blueCoin;
    [SerializeField] int value = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            GameManager.instance.totalCollectedCoins += value;

            Debug.Log(GameManager.instance.totalCollectedCoins);
            if (redCoin)
            {

            }

            if (blueCoin)
            {
                GameManager.instance.GetBlueCoin();
            }

            Destroy(this.gameObject);
        }
    }

}
