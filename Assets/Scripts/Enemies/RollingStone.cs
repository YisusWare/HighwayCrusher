using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : BreakableObject
{
    [SerializeField]
    Vector2 speed;
    Rigidbody2D rb;
    
    float screenTopEdge;

    bool launched = false;

    [SerializeField]
    int damage;
    [SerializeField]
    int power;
    
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Vector3 topRigthCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        screenTopEdge = topRigthCorner.y;

        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.takeDamage(damage);
            
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            BreakableObject breakableObject = collision.gameObject.GetComponent<BreakableObject>();
            breakableObject.TakeDamage(damage);
        }
    }


}
