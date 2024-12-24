using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallConcrete : AllyProjectile
{
    
    private Animator animator;
    [SerializeField]
    Transform explosionCenter;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered");
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
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(explosionCenter.position, 0.4f,enemyLayer);
        
        foreach (Collider2D collider in enemyColliders)
        {
           
            
            collider.gameObject.GetComponent<Obstacle>().TakeDamage(power);
        }
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {

        
        // Dibujar el círculo en 2D
        Gizmos.DrawWireSphere(explosionCenter.position, 0.4f); 
    }
}
