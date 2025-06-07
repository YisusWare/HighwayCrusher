using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    int damage;
    [SerializeField]
    int power;
    Transform playerTransform;
    [SerializeField]
    float distanceThereshold;
    Animator animator;
    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = FindObjectOfType<PlayerController>().gameObject.transform;

        if(Vector2.Distance(transform.position,playerTransform.position) <= distanceThereshold)
        {
            animator.SetBool("Scare",true);
        }
        else
        {
            animator.SetBool("Scare", false);
        }
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            player.takeDamage(damage + (power - player.Power));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distanceThereshold);
    }
}
