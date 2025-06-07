using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMovement : MonoBehaviour
{
    [SerializeField]
    protected Transform[] movePoints;
    [SerializeField]
    float speed;
    [SerializeField]
    float smooth = 0.5f;

    private Vector3 currentSpeed = Vector3.zero;
    Rigidbody2D rb;
    protected int currentIndex = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        if(Vector2.Distance(transform.position,movePoints[currentIndex].position) < 0.3f)
        {
            if(currentIndex < movePoints.Length - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
        }

        if(GameManager.instance.currentState == GameManager.GameState.gameScreen)
        {
            transform.position = Vector3.SmoothDamp(transform.position, 
                                                    movePoints[currentIndex].position,
                                                    ref currentSpeed,
                                                    smooth,
                                                    speed);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }
}
