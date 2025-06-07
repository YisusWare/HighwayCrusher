using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPosition : MonoBehaviour
{
    [SerializeField]GameObject Joystick;
    [SerializeField] Vector2 joystickVector;
    private Vector2 joystickTouchPosition;
    private Vector2 JoystickOriginalPosition;
    private float joystickRadius;
    void Start()
    {
        
    }
}
