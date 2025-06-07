using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorEnemy : MonoBehaviour
{
    [SerializeField]
    WheelJoint2D wheelJoint;
    [SerializeField]
    float speed;
    void Start()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.currentState != GameManager.GameState.gameScreen)
        {
            JointMotor2D motor = wheelJoint.motor;
            motor.motorSpeed = 0;
            wheelJoint.motor = motor;
        }
        else
        {
            JointMotor2D motor = wheelJoint.motor;
            motor.motorSpeed = speed;
            wheelJoint.motor = motor;
        }
    }
}
