using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseBullet : MonoBehaviour
{
    public GameObject parent;
    protected Rigidbody2D rigidBody;

    protected float speed = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHitTarget(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnHitTarget(collision.gameObject);
        }
    }

    public virtual void OnHitTarget(GameObject gameObject)
    {

    }
    
    public virtual void Shoot(Vector2 direction)
    {

    }
}
