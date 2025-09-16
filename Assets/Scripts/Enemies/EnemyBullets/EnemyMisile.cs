using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMisile : EnemyBaseBullet
{

    [SerializeField]
    int damage;
    [SerializeField]
    int power;
    PlayerController player;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    float timer;
    [SerializeField]
    float linearSpeed;

    [SerializeField]
    bool followPlayer;

    [SerializeField]
    bool HitObstacles;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        timer -= Time.deltaTime;
        if (followPlayer)
        {
            Vector3 Direction = player.gameObject.transform.position - transform.position;
            float angulo = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg; // Calcula el ángulo en grados
            Quaternion targetRotation = Quaternion.Euler(0, 0, angulo);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
        
        rigidBody.velocity = transform.right * linearSpeed;

        if(timer <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


    public override void OnHitTarget(GameObject gameObject)
    {
        base.OnHitTarget(gameObject);

        PlayerController player = gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        BreakableObject breakableObject = gameObject.GetComponent<BreakableObject>();

        if (breakableObject != null && HitObstacles)
        {
           
            if (gameObject == parent)
            {
                Debug.Log("Hit Parent");
                return;
            }

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        
    }

    public override void Shoot(Vector2 direction)
    {
        float angulo = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }
}
