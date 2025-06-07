using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDriveCar : MonoBehaviour
{

    public static SelfDriveCar instance;
    [SerializeField]
    Transform frontSensorPoint;
    [SerializeField]
    Transform leftSensorPoint;
    [SerializeField]
    Transform leftSensorPointB;
    [SerializeField]
    Transform leftSensorPointC;
    [SerializeField]
    Transform rightSensorPoint;
 
    [SerializeField]
    Transform rightSensorPointB;
    [SerializeField]
    Transform rightSensorPointC;


    [SerializeField]
    Transform AtackModeSensorA;


    [SerializeField]
    Transform AtackModeSensorB;
    [SerializeField]
    float frontSensorDistance;


    [SerializeField]
    float sideSensorDistance;
    [SerializeField]
    LayerMask ObstaclesMask;

    [SerializeField]
    LayerMask PlayerMask;
    bool frontSensor;
    bool leftSensor;
    bool leftSensorB;
    bool leftSensorC;
    bool rightSensor;
    bool rightSensorB;
    bool rightSensorC;
   

    bool chaseMode;

    [SerializeField]
    float speed;

    [SerializeField]
    GameObject substitute;
    SpriteRenderer sprite;
    float spriteHeigth;
    float spriteWidth;
    float screenLeftEdge;
    float screenRigthEdge;
    float screenTopEdge;
    float screenBottomEdge;

    Rigidbody2D rigidBody;
    void Start()
    {

        if(instance == null)
        {
            instance = this;
        }
        RoadManager roadManager = FindObjectOfType<RoadManager>();

        if (roadManager.specialEventHappening)
        {
            GameObject substituteInstance = Instantiate(substitute, transform.position, Quaternion.identity);

            Car substituteCar = substituteInstance.GetComponent<Car>();
            if(substituteCar != null)
            {
                float carSpeed = GetComponent<Car>().speed;
                substituteCar.speed = carSpeed;
            }

            Destroy(this.gameObject);
        }

        roadManager.specialEventHappening = true;

        sprite = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteHeigth = sprite.bounds.size.y;
        spriteWidth = sprite.bounds.size.x;
        Vector3 pointZero = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRigthScreen = new Vector3(Screen.width, Screen.height, 0);
        Vector3 topRigthCorner = Camera.main.ScreenToWorldPoint(topRigthScreen);
        screenLeftEdge = pointZero.x;
        screenBottomEdge = pointZero.y;
        screenTopEdge = topRigthCorner.y;
        screenRigthEdge = topRigthCorner.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameState.gameScreen)
        {
            frontSensor = Physics2D.OverlapCircle(frontSensorPoint.position, frontSensorDistance, ObstaclesMask);

            leftSensor = Physics2D.Raycast(leftSensorPoint.position, Vector2.up, sideSensorDistance, ObstaclesMask);
            leftSensorB = Physics2D.Raycast(leftSensorPointB.position, Vector2.up, sideSensorDistance, ObstaclesMask);
            leftSensorC = Physics2D.Raycast(leftSensorPointC.position, Vector2.up, sideSensorDistance, ObstaclesMask);
            rightSensor = Physics2D.Raycast(rightSensorPoint.position, Vector2.up, sideSensorDistance, ObstaclesMask);
            rightSensorB = Physics2D.Raycast(rightSensorPointB.position, Vector2.up, sideSensorDistance, ObstaclesMask);
            rightSensorC = Physics2D.Raycast(rightSensorPointC.position, Vector2.up, sideSensorDistance, ObstaclesMask);

            if (Physics2D.Raycast(AtackModeSensorA.position, Vector2.left, 8f, PlayerMask) ||
                Physics2D.Raycast(AtackModeSensorB.position, Vector2.right, 8f, PlayerMask))
            {
                chaseMode = true;
                transform.SetParent(null);

                Car enemyCar = GetComponent<Car>();

                Destroy(enemyCar);
            }

            if (chaseMode)
            {
                if (transform.position.x > screenRigthEdge + (spriteWidth / 2))
                {
                    Destroy(this.gameObject);
                }

                if (transform.position.x < screenLeftEdge - (spriteWidth / 2))
                {
                    Destroy(this.gameObject);
                }

                if (transform.position.y > screenTopEdge + (spriteHeigth / 2))
                {
                    Destroy(this.gameObject);
                }

                if (transform.position.y < screenBottomEdge - (spriteHeigth / 2))
                {
                    Destroy(this.gameObject);
                }
            }
            
        }
           



        
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.currentState == GameManager.GameState.gameScreen)
        {
            if (transform.position.y <= -8)
            {
                Destroy(this.gameObject);
            }
            if (chaseMode)
            {
                if (frontSensor)
                {
                    if (transform.position.x > 0)
                    {
                        if (!leftSensor && !leftSensorB && !leftSensorC)
                        {

                            rigidBody.velocity = Vector2.left * speed;
                            return;
                        }

                        if (!rightSensor && !rightSensorB && !rightSensorC)
                        {

                            rigidBody.velocity = Vector2.right * speed;
                            return;
                        }

                        rigidBody.velocity = Vector2.left * speed;
                        return;
                    }
                    else
                    {
                        if (!rightSensor && !rightSensorB && !rightSensorC)
                        {

                            rigidBody.velocity = Vector2.right * speed;
                            return;
                        }

                        if (!leftSensor && !leftSensorB && !leftSensorC)
                        {

                            rigidBody.velocity = Vector2.left * speed;
                            return;
                        }

                        rigidBody.velocity = Vector2.right * speed;
                        return;
                    }

                }
                else
                {
                    rigidBody.velocity = Vector2.zero;
                }
            }
        }
        
       
    }

    private void OnDestroy()
    {
        RoadManager roadManager = FindObjectOfType<RoadManager>();
        if(roadManager != null)
        {
            if(instance == this)
            {
                roadManager.specialEventHappening = false;
                Debug.Log("destroying camioneta");
            }
            
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(frontSensorPoint.position, frontSensorDistance);
        Debug.DrawLine(leftSensorPoint.position, new Vector3(leftSensorPoint.position.x, leftSensorPoint.position.y + sideSensorDistance), Color.green);
        Debug.DrawLine(leftSensorPointB.position, new Vector3(leftSensorPointB.position.x, leftSensorPointB.position.y + sideSensorDistance), Color.green);
        Debug.DrawLine(leftSensorPointC.position, new Vector3(leftSensorPointC.position.x, leftSensorPointC.position.y + sideSensorDistance), Color.green);
        Debug.DrawLine(rightSensorPoint.position, new Vector3(rightSensorPoint.position.x, rightSensorPoint.position.y + sideSensorDistance), Color.green);
        Debug.DrawLine(rightSensorPointB.position, new Vector3(rightSensorPointB.position.x, rightSensorPointB.position.y + sideSensorDistance), Color.green);
        Debug.DrawLine(rightSensorPointC.position, new Vector3(rightSensorPointC.position.x, rightSensorPointC.position.y + sideSensorDistance), Color.green);

        Debug.DrawLine(rightSensorPoint.position, new Vector3(rightSensorPointC.position.x + 8f, rightSensorPointC.position.y ), Color.blue);
        Debug.DrawLine(leftSensorPoint.position, new Vector3(leftSensorPoint.position.x - 8f, leftSensorPoint.position.y), Color.blue);
    }
}
