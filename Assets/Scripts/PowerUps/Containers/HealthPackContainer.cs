using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackContainer : PowerUpContainer
{
    // Start is called before the first frame update
    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.LeanScale(Vector3.one * 0.5f, 0.3f);
    }

    public override void Activate(PowerUpsStorage storage, GameObject button)
    {

        base.Activate(storage, button);


        Transform player = FindObjectOfType<PlayerController>().gameObject.transform;

        Instantiate(prefab, player.position, Quaternion.identity);
        var index = button.transform.GetSiblingIndex();
        storage.DestroyPowerUp(index);
    }
}
