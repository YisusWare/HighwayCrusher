using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : AllyProjectile
{
    Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        transform.SetParent(null);
    }
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyCollider = collision;

            HitEnemy();
        }
    }

    public override void HitEnemy()
    {
        base.HitEnemy();
        Debug.Log("Damage");
        BreakableObject enemy = enemyCollider.gameObject.GetComponent<BreakableObject>();

        enemy.TakeDamage(power);

        animator.SetTrigger("Destroy");
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
