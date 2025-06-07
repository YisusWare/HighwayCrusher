using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plains : MonoBehaviour
{
    [SerializeField]
    public PlayableCarModel carModel;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            player.GetCarPlains(carModel);
            GameManager.instance.OnGetCarPlains(carModel.Id);

            Destroy(this.gameObject);
        }
    }

   
}
