using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaElastica : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("bounce");
    }
}
