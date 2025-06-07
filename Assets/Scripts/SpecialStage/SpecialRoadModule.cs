using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoadModule : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] Transform anchor;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.currentState == GameManager.GameState.gameScreen)
        {
            rb.velocity = new Vector2(0, speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if(anchor.position.y < 0)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
