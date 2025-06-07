using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float speed = -3f;
    public float Xspeed;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.instance.currentState == GameManager.GameState.gameScreen)
        {

            rb.velocity = new Vector2(Xspeed, speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }


        if (transform.position.y <= -10)
        {
            Destroy(this.gameObject);
        }
    }

}
