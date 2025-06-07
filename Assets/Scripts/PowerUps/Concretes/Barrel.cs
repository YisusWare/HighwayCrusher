using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    int damage;

    Rigidbody2D rigidbody;
    Animator animator;

    private void Start()
    {
        speed = FindObjectOfType<PlayerController>().speed * -1;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidbody.AddForce(new Vector2(0, speed),ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BreakableObject breakableObject = collision.gameObject.GetComponent<BreakableObject>();
            breakableObject.TakeDamage(damage);
            animator.SetTrigger("Destroy");
        }
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }

}
