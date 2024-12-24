using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.OnTakeDamage += SetCurrentHealth;
        slider = GetComponent<Slider>();
        SetMaxHealth(player.maxHealth);
        SetCurrentHealth(player.maxHealth);
    }

    public void SetMaxHealth(float maxLife)
    {
        slider.maxValue = maxLife;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }
}
