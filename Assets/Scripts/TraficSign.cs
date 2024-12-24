using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficSign : Obstacle
{
    private bool destroyAnimation = false;

    private void Update()
    {
        if (destroyAnimation)
        {
            transform.Translate(new Vector3(0, -6 * Time.deltaTime, 0));
        }    
    }

    public override void StartDestroyAnimation()
    {
        base.StartDestroyAnimation();
        destroyAnimation = true;
    }
}
