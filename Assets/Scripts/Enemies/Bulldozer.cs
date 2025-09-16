using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulldozer : BreakableObject
{

    PlayerController player;
    Rigidbody2D rigidBody;
    [SerializeField]
    int damage;
    [SerializeField]
    int power;
    [SerializeField]
    float baseRotationSpeed;

    float rotationSpeed;

    float Timer = 0;

    bool isEnemyDestroyed = false;
    [SerializeField]
    GameObject litleExplosions;

    [SerializeField]
    float linearSpeed;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
        rotationSpeed = baseRotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyDestroyed)
            Timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Vector3 Direction = player.gameObject.transform.position - transform.position;
        if (!isEnemyDestroyed)
        {
            float angulo = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg; // Calcula el ángulo en grados

            Quaternion targetRotation = Quaternion.Euler(0, 0, angulo);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
        
        
        rigidBody.velocity = transform.right * linearSpeed;

        if(Vector2.Distance(transform.position, player.gameObject.transform.position) > 5)
        {
            rotationSpeed = baseRotationSpeed + 2;
        }
        else
        {
            rotationSpeed = baseRotationSpeed;
        }

        if(Timer >= 7 && isEnemyDestroyed)
        {
            DestroyEnemy();
        }

        
    }

    public override void DestroyEnemy()
    {
        RoadManager roadManager = FindObjectOfType<RoadManager>();
        roadManager.specialEventHappening = false;
        Destroy(transform.parent.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BreakableObject bo = collision.gameObject.GetComponent<BreakableObject>();
            bo.TakeDamage(bo.MaxHealthPoints);
        }

        if (collision.gameObject.CompareTag("Player"))
        {

            var player = collision.gameObject.GetComponent<PlayerController>();

            player.takeDamage(damage + (power - player.Power));

            TakeDamage(player.Power);
        }
    }

    public override void StartDestroyAnimation()
    {
        this.animator.SetTrigger("Destroyed");
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        Timer = 0;
        litleExplosions.SetActive(true);
        
        isEnemyDestroyed = true;
    }
}
