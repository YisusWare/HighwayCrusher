using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : EnemyBaseBullet
{
    Rigidbody2D rb;
    
    public float speedLinear;
    
    public float speedParabolic;
    public float alturaDeLaOnda;
    [SerializeField]
    float maxSpeedX;
    [SerializeField]
    float maxSpeedY;
    public bool goLeft;
    float t = 0;
    float x;
    Vector2 Direction;
    Vector2 Perpendicular;

    [SerializeField]
    int damage;
    [SerializeField]
    int power;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerController playerTransform = FindObjectOfType<PlayerController>();
        Direction = (playerTransform.transform.position - transform.position).normalized;
        if (goLeft)
            Perpendicular = new Vector2(-Direction.y, Direction.x);

        if (!goLeft)
            Perpendicular = new Vector2(Direction.y, -Direction.x);
        rb.AddForce(Perpendicular * -2, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * speedParabolic;

        x = Mathf.Pow(t, 2) - (t * alturaDeLaOnda);
       
        if(t > 20)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        
        rb.AddForce(Direction * speedLinear);

        rb.AddForce(x * Perpendicular);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeedX, maxSpeedX), Mathf.Clamp(rb.velocity.y, -maxSpeedY, maxSpeedY));
    }

    public override void OnHitTarget(GameObject gameObject)
    {
        base.OnHitTarget(gameObject);

        PlayerController player = gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.takeDamage(damage + (power - player.Power));
           
        }

        BreakableObject breakableObject = gameObject.GetComponent<BreakableObject>();

        if (breakableObject != null)
        {

            if (gameObject == parent)
            {

                return;
            }

            breakableObject.TakeDamage(damage);
            
        }

        
    }
}
