using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBullet : EnemyBaseBullet
{
    [SerializeField]
    int damage;
    [SerializeField]
    int power;
    public override void OnHitTarget(GameObject gameObject)
    {
        base.OnHitTarget(gameObject);

        PlayerController player = gameObject.GetComponent<PlayerController>();

        if(player != null)
        {
            player.takeDamage(damage + (power - player.Power));
            Destroy(this.gameObject);
        }

        BreakableObject breakableObject = gameObject.GetComponent<BreakableObject>();

        if(breakableObject != null)
        {
            Debug.Log("hit enemy");
            if (gameObject == parent)
            {
                Debug.Log("Hit Parent");
                return;
            }
            
            breakableObject.TakeDamage(damage);
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject);
    }

    public override void Shoot(Vector2 direction)
    {

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = direction.normalized * speed;
    }

}
