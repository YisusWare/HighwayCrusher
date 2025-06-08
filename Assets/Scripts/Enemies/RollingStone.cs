using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : MonoBehaviour
{
    [SerializeField]
    Vector2 speed;
    Rigidbody2D rb;
    Animator animator;
    
    float screenTopEdge;

    bool launched = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Vector3 topRigthCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        screenTopEdge = topRigthCorner.y;

        rb.AddForce(speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

          
        }
    }


}
