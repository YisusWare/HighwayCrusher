using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPositionSigns : MonoBehaviour
{
    [SerializeField]
    bool top;
    [SerializeField]
    bool bottom;
    [SerializeField]
    bool left;
    [SerializeField]
    bool rigth;
    [SerializeField]
    float duration;
    float timer;

    float screenLeftEdge;
    float screenRigthEdge;
    float screenTopEdge;
    float screenBottomEdge;

    void Start()
    {
        timer = duration;
        Vector3 pointZero = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRigthScreen = new Vector3(Screen.width, Screen.height, 0);
        Vector3 topRigthCorner = Camera.main.ScreenToWorldPoint(topRigthScreen);
        screenLeftEdge = pointZero.x;
        screenBottomEdge = pointZero.y;
        screenTopEdge = topRigthCorner.y;
        screenRigthEdge = topRigthCorner.x;
        if (left)
        {
            transform.position = new Vector2(screenLeftEdge + 0.4f, transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(this.gameObject);


        }
    }
}
