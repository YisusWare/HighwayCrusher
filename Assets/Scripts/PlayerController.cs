using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;

    int health = 100;
    [SerializeField] private InputActionReference moveActionToUse;
    [SerializeField] public float speed;
    [SerializeField] public float Acceleration;
    [SerializeField] public int Power;

    private float invincibleTimer = 0;

    SpriteRenderer sprite;
    float screenLeftEdge;
    float screenRigthEdge;
    float screenTopEdge;
    float screenBottomEdge;
    Rigidbody2D rb;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.instance.currentState = GameManager.GameState.gameScreen;
        sprite = GetComponent<SpriteRenderer>();
        Vector3 pointZero = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRigthScreen = new Vector3(Screen.width, Screen.height, 0);
        Vector3 topRigthCorner = Camera.main.ScreenToWorldPoint(topRigthScreen);
        screenLeftEdge = pointZero.x;
        screenBottomEdge = pointZero.y;
        screenTopEdge = topRigthCorner.y;
        screenRigthEdge = topRigthCorner.x;
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = moveActionToUse.action.ReadValue<Vector2>();
        transform.Translate(moveDirection * Acceleration * Time.deltaTime);
      

        float playerHeigth = sprite.bounds.size.y;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, screenLeftEdge + (playerHeigth / 4), screenRigthEdge - (playerHeigth / 4)),
                                        Mathf.Clamp(transform.position.y, screenBottomEdge + (playerHeigth / 2), screenTopEdge - (playerHeigth / 2)),
                                        transform.position.z);

        if(invincibleTimer < 0)
        {
            animator.SetBool("Invincible", false);
        }
       
    }

    private void FixedUpdate()
    {
        invincibleTimer -= Time.deltaTime;
    }

    public void SetInvincibleTimer(float timer)
    {
        invincibleTimer = timer;
        animator.SetBool("Invincible", true);
    }

    

    public void takeDamage(int damage)
    {
        if(invincibleTimer < 0)
        {
            health = Mathf.Clamp(health - damage, 0, maxHealth);

            OnTakeDamage?.Invoke(health);
            if (health <= 0)
            {
                GameManager.instance.GameOver();
                OnDie?.Invoke(this, EventArgs.Empty);

            }
            else
            {
                animator.SetTrigger("Damage");
            }
        }
        
        
    }



    public void GetPowerUp(PowerUpContainer powerUp) 
    {
        OnGetPowerUp?.Invoke(powerUp);
    }

    public void RestartHealth()
    {
        health = maxHealth;
        OnTakeDamage?.Invoke(health);
    }

    public event EventHandler OnDie;

    public event Action<int> OnTakeDamage;
    public event Action<PowerUpContainer> OnGetPowerUp;
}
