using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;


    public void UpdateHealthBar(int maxHealth, int currentHealth)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}
