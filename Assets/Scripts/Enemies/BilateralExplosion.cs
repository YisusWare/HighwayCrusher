using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilateralExplosion : MonoBehaviour
{
    public float explosionRadius;
    public float damage;
    void Start()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach( var collider in enemyColliders)
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();

            if(player != null)
            {
                player.takeDamage((int)damage - player.Power);

                continue;
            }

            BreakableObject breakableObject = collider.gameObject.GetComponent<BreakableObject>();

            if(breakableObject != null)
            {
                breakableObject.TakeDamage((int)damage);
            }


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }




}
