using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpContainer: MonoBehaviour
{
    [SerializeField]
    public Sprite sprite;
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public int price;
    public int index;
 
    public virtual void Activate(PowerUpsStorage storage, GameObject button)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            player.GetPowerUp(this);
            Destroy(this.gameObject);
        }
    }
}
