using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellicopter : BreakableObject
{
    [SerializeField]
    int damage;
    [SerializeField]
    int power;
    Rigidbody2D rb;
    Vector3 targetPoint;
    float Timer = 0;
    bool isEnemyDestroyed = false;
    [SerializeField]
    GameObject litleExplosions;

    [SerializeField]
    Transform targetPointTransform;

    protected override void Start()
    {
        base.Start();
        targetPoint = targetPointTransform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isEnemyDestroyed)
        {
            if (Vector2.Distance(targetPoint, transform.position) >= 0.6f)
            {
                rb.AddForce((targetPoint - transform.position).normalized );
            }
            else
            {
                rb.mass = 1f;
                rb.velocity = Vector2.zero;

            }
        }
        else
        {
            rb.AddForce(Vector2.down * 5);
        }

        if (Timer >= 7 && isEnemyDestroyed)
        {
            DestroyEnemy();
        }
    }

    void Update()
    {
        if (isEnemyDestroyed)
            Timer += Time.deltaTime;
    }

    public override void DestroyEnemy()
    {
        RoadManager roadManager = FindObjectOfType<RoadManager>();
        roadManager.specialEventHappening = false;
        Destroy(transform.parent.gameObject);
    }

    public override void StartDestroyAnimation()
    {
        this.animator.SetTrigger("Destroyed");
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Timer = 0;
        litleExplosions.SetActive(true);

        isEnemyDestroyed = true;
    }
}
