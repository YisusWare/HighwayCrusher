using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockBall : AllyProjectile
{
    private Animator animator;
    [SerializeField]
    Transform explosionCenter;
    [SerializeField]
    GameObject residualShockBalls;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
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
        
        
        GameObject ball1 = Instantiate(residualShockBalls, transform.position, Quaternion.identity);

        ball1.GetComponent<ShockBallResidual>().direction = new Vector2(1, 0);

        GameObject ball2 = Instantiate(residualShockBalls, transform.position, Quaternion.identity);

        ball2.GetComponent<ShockBallResidual>().direction = new Vector2(-1, 0);

        SelfDestroy();
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
