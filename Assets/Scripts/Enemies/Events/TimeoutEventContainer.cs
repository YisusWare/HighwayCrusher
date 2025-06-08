using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeoutEventContainer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float timeoutLimit;
    float timeout;
    void Start()
    {
        timeout = timeoutLimit;
    }

    // Update is called once per frame
    void Update()
    {
        timeout -= Time.deltaTime;
        
        if(timeout <= 0)
        {

            RoadManager roadManager = FindObjectOfType<RoadManager>();
            roadManager.specialEventHappening = false;
            Destroy(this.gameObject);

        }
    }
}
