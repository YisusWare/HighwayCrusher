using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : BreakableObject
{
    [SerializeField]
    int damage;
   
    [SerializeField]
    public int power;
    
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        canMakeDamage = true;
        HealthPoints = MaxHealthPoints;

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

    
    

    
    public void DestroyEnemy() 
    {
        Destroy(this.gameObject);
    }
}
