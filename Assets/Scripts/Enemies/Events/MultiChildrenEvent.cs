using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiChildrenEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0)
        {
            RoadManager roadManager = FindObjectOfType<RoadManager>();
            roadManager.specialEventHappening = false;
            Destroy(gameObject);
        }
    }
}
