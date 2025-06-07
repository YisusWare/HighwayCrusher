using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidBody;
    float speed = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Shoot(Vector2 direction)
    {
     
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = direction.normalized * speed;
    }
}
