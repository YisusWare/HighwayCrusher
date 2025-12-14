using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMan : BreakableObject
{

    private PlayerController player;
    [SerializeField] DetectPlayer detectPlayer;
    [SerializeField] float speed;
    [SerializeField] bool lookingRigth;
    [SerializeField] float secondsToExplote;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    int damage;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    protected override void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        healthBarCanvas = healthBar.gameObject.GetComponent<Canvas>();
        healthBarCanvas.enabled = false;
        HealthPoints = MaxHealthPoints;
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer.playerDetected)
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            transform.SetParent(null);
            animator.SetBool("ChasePlayer", true);
            transform.position = Vector2.MoveTowards(transform.position, player.gameObject.transform.position, speed * Time.deltaTime);

            secondsToExplote -= Time.deltaTime;
            if(player.transform.position.x > transform.position.x && !lookingRigth)
            {
                Flip();
            }

            if(player.transform.position.x < transform.position.x && lookingRigth)
            {
                Flip();
            }

            if(secondsToExplote <= 0)
            {
                
                animator.SetTrigger("Explote");
                detectPlayer.playerDetected = false;
            }
        }
    }

    public void MakeDamage()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, 0.7f);

        foreach( var collider in enemyColliders)
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();

            if(player != null)
            {
                player.takeDamage((int)damage - player.Power);

                continue;
            }

            BreakableObject breakableObject = collider.gameObject.GetComponent<BreakableObject>();

            if(breakableObject != null && breakableObject != this)
            {
                breakableObject.TakeDamage((int)damage);
            }


        }
    }

    private void Flip()
    {
       
        transform.localScale = new Vector3(transform.localScale.x * -1, 0.7f);

        lookingRigth = !lookingRigth;
    }

    
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
