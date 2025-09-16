using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutter : Aim360
{
    Animator animator;
    bool shootLeft = false;
    GameObject parent;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        parent = transform.parent.gameObject;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Shoot(Vector3 direction)
    {
        Debug.Log("Disparando");
        animator.SetTrigger("Shoot");
        shootingTime = shootingCadence;

        if(Random.Range(0, 2)  == 0)
        {
            animator.SetTrigger("leftAtack");
            shootLeft = false;
        }
        else
        {
            animator.SetTrigger("rigthAtack");
            shootLeft = true;
        }

    }

    public void InstanceAxe()
    {
        GameObject axe = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Axe axeScript = axe.GetComponent<Axe>();
        axeScript.parent = parent;
        axeScript.goLeft = shootLeft;
       
    }


}
