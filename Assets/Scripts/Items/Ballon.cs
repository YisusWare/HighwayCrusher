using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    int value;
    [SerializeField]
    GameObject number;
    void Start()
    {
        animator = GetComponent<Animator>();
       
    }

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            BallonStageManager stageManager = FindObjectOfType<BallonStageManager>();
            stageManager.OnGetBallon(value);
            animator.SetTrigger("Explote");
            Instantiate(number, transform.position, Quaternion.identity);
        }
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
