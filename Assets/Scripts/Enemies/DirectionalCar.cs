using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalCar : MonoBehaviour
{
    bool goLeft;
    bool ligthOff;
    float targetX;
    Animator animator;
    Rigidbody2D rb;
    float positionX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        positionX = transform.position.x;

        goLeft = positionX >= 0;

        animator.SetBool("TurnLeft", goLeft);

        targetX = goLeft ? positionX - 0.8f : positionX + 0.8f;
        
    }

    private void FixedUpdate()
    {
        if(transform.position.y <= 1)
        {
            animator.SetBool("LigthOff", true);

            if(Vector2.Distance(transform.position, new Vector2(targetX,transform.position.y)) > 0.2)
            {
                rb.AddForce(new Vector2(goLeft ? -70 : 70, 0));
            }

        }
    }
}
