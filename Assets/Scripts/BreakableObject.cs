using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakableObject : MonoBehaviour
{

    [SerializeField]
    protected int HealthPoints;
    [SerializeField]
    protected int MaxHealthPoints;
    protected bool canMakeDamage;
    protected Animator animator;
    [SerializeField]
    protected EnemyHealthBar healthBar;
    protected Canvas healthBarCanvas;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        HealthPoints = MaxHealthPoints;
        healthBarCanvas = healthBar.gameObject.GetComponent<Canvas>();
        healthBarCanvas.enabled = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        healthBarCanvas.enabled = true;
        HealthPoints = Mathf.Clamp(HealthPoints - damage, 0, MaxHealthPoints);
        healthBar.UpdateHealthBar(MaxHealthPoints, HealthPoints);
        
        if (HealthPoints <= 0)
        {
            healthBarCanvas.enabled = false;
            StartDestroyAnimation();
        }
        else
        {
            OnTakeDamage();
        }

    }

    public virtual void StartDestroyAnimation()
    {
       
        canMakeDamage = false;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        this.animator.SetTrigger("Destroyed");

    }

    public void OnTakeDamage()
    {

        animator.SetTrigger("Damage");
    }

    public virtual void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
