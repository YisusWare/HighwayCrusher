using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    [SerializeField]
    protected int HealthPoints;
    [SerializeField]
    protected int MaxHealthPoints;
    protected bool canMakeDamage;
    protected Animator animator;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
       
        HealthPoints = Mathf.Clamp(HealthPoints - damage, 0, MaxHealthPoints);

        if (HealthPoints <= 0)
        {

            StartDestroyAnimation();
        }
        else
        {
            OnTakeDamage();
        }

    }

    public virtual void StartDestroyAnimation()
    {
       
        canMakeDamage = false;
        animator.SetTrigger("Destroyed");

    }

    public void OnTakeDamage()
    {

        animator.SetTrigger("Damage");
    }
}
