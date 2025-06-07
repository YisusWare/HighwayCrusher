using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackConcrete : MonoBehaviour
{
    [SerializeField]
    int health;
    void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        player.takeDamage(health);
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
