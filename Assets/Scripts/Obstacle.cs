using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : BreakableObject
{
    [SerializeField]
    int damage;
   
    [SerializeField]
    public int power;
    
    

    protected override void Start()
    {
        base.Start();
        
        canMakeDamage = true;
        

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && canMakeDamage)
    //    {
            
    //        var player = collision.gameObject.GetComponent<PlayerController>();
            
    //        player.takeDamage(damage + (power - player.Power));

    //        TakeDamage(player.Power);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canMakeDamage)
        {

            var player = collision.gameObject.GetComponent<PlayerController>();

            player.takeDamage(damage + (power - player.Power));

            TakeDamage(player.Power);
        }
    }

    public override void DestroyEnemy() 
    {
        Destroy(this.gameObject);
    }
}
