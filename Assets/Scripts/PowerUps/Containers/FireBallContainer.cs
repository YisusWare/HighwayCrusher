using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallContainer : PowerUpContainer
{

    public override void Activate(PowerUpsStorage storage,GameObject button)
    {

       
        base.Activate(storage,button);

        Transform player = FindObjectOfType<PlayerController>().gameObject.transform;

        Instantiate(prefab, player.position, Quaternion.identity);
        var index = button.transform.GetSiblingIndex();
        
        storage.DestroyPowerUp(index);
    }
}
