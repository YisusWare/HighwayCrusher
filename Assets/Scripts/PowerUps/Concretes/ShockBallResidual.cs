using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockBallResidual : AllyProjectile
{
    private Animator animator;
    [SerializeField]
    Transform explosionCenter;

    public Vector2 direction;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
 
        timer += Time.deltaTime;

        if(timer >= 5)
        {
            SelfDestroy();
        }
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
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(explosionCenter.position, 0.3f, enemyLayer);

        foreach (Collider2D collider in enemyColliders)
        {

            BreakableObject breakableObject = collider.gameObject.GetComponent<BreakableObject>();

            if (breakableObject != null)
            {
                breakableObject.TakeDamage(power);
            }

        }
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
