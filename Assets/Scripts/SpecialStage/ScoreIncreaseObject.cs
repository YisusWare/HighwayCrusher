using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreIncreaseObject : MonoBehaviour
{
    [SerializeField]
    int value;
    [SerializeField]
    GameObject pointsSpan;
    void Start()
    {
        
    }

    public void OnGetPoints()
    {
        CarCrashStageManager stageManager = FindObjectOfType<CarCrashStageManager>();

        if(stageManager != null)
        {
            stageManager.OnGetPoints(value);
            Instantiate(pointsSpan, transform.position, Quaternion.identity);
        }
    }
}
