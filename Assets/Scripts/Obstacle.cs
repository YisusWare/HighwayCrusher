using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    int damage;
    [SerializeField]
    int HealthPoints;
    [SerializeField]
    int MaxHealthPoints;
    [SerializeField]
    public int power;
    Animator animator;
    bool canMakeDamage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        HealthPoints = MaxHealthPoints;
        canMakeDamage = true;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canMakeDamage)
        {
            
            var player = collision.gameObject.GetComponent<PlayerController>();
            
            player.takeDamage(damage + (power - player.Power));

            TakeDamage(player.Power);
        }
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

    public void OnTakeDamage() 
    {

        animator.SetTrigger("Damage");
    }
    public virtual void StartDestroyAnimation()
    {
        canMakeDamage = false;
        animator.SetTrigger("Destroyed");
    }

    
    public void DestroyEnemy() 
    {
        Destroy(this.gameObject);
    }
}
