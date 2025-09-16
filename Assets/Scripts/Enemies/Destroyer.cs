using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BreakableObject bo = collision.gameObject.GetComponent<BreakableObject>();
            bo.TakeDamage(bo.MaxHealthPoints);
        }
    }
}
