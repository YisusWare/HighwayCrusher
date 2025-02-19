using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerDetected;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
