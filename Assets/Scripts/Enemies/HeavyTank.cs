using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTank : BreakableObject
{

    [SerializeField]
    int damage;
    [SerializeField]
    int power;
    Rigidbody2D rb;
    Vector3 targetPoint;
    
    protected override void Start()
    {
        base.Start();
        targetPoint = new Vector3(0, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(Vector2.Distance(targetPoint, transform.position) >= 0.6f)
        {
            rb.AddForce((targetPoint - transform.position).normalized * 3);
        }
        else
        {
            rb.mass = 1f;
            rb.velocity = Vector2.zero;
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BreakableObject bo = collision.gameObject.GetComponent<BreakableObject>();
            bo.TakeDamage(bo.MaxHealthPoints);
        }

        if (collision.gameObject.CompareTag("Player"))
        {

            var player = collision.gameObject.GetComponent<PlayerController>();

            player.takeDamage(damage + (power - player.Power));

            TakeDamage(player.Power);
        }
    }

    public override void DestroyEnemy()
    {
        RoadManager roadManager = FindObjectOfType<RoadManager>();
        roadManager.specialEventHappening = false;
        Destroy(transform.parent.gameObject);
    }
}
