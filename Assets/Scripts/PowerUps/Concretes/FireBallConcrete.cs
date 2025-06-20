using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallConcrete : AllyProjectile
{
    
    private Animator animator;
    [SerializeField]
    Transform explosionCenter;
    [SerializeField]
    bool environmentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (environmentSpeed)
        {
            speed = FindObjectOfType<PlayerController>().speed * -1;
        }
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            

            HitEnemy();
            //TO DO: create circle and apply damage
        }
    }

    public override void HitEnemy()
    {
        base.HitEnemy();
        MakeDamage();
        animator.SetTrigger("Explote");
    }

    private void MakeDamage()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(explosionCenter.position, 0.5f,enemyLayer);
        
        foreach (Collider2D collider in enemyColliders)
        {
            ScoreIncreaseObject scoreObject = collider.gameObject.GetComponent<ScoreIncreaseObject>();

            if(scoreObject != null)
            {
                scoreObject.OnGetPoints();
            }

            BreakableObject breakableObject = collider.gameObject.GetComponent<BreakableObject>();
            
            if(breakableObject != null)
            {
                breakableObject.TakeDamage(power);
            }
            
        }
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {

        
        // Dibujar el c�rculo en 2D
        Gizmos.DrawWireSphere(explosionCenter.position, 0.5f); 
    }
}
