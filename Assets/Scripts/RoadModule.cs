using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadModule : MonoBehaviour
{
    [SerializeField]
    public int nextBiomeId;
    [SerializeField]
    public int[] adyacentModules;
    [SerializeField]
    public bool transitionModule;
    public float speed = -3f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if(transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
    }
}
