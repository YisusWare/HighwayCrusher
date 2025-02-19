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

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        HealthPoints = MaxHealthPoints;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer.playerDetected)
        {
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
                MakeDamage();
                animator.SetTrigger("Explote");
                detectPlayer.playerDetected = false;
            }
        }
    }

    private void MakeDamage()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 0.4f, playerLayer);

        if(playerCollider != null)
        {
            PlayerController player = playerCollider.gameObject.GetComponent<PlayerController>();
           
            player.takeDamage(damage - player.Power);
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, 1);

        lookingRigth = !lookingRigth;
    }

    
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
