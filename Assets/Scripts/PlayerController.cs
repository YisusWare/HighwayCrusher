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

    [SerializeField] private GameObject[] blueCoinNumbers;
    [SerializeField]
    Joystick joystick;
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

        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody2D>();

        GameManager.instance.OnGetBlueCoin += OnGetBlueCoin;
        
        sprite = GetComponent<SpriteRenderer>();
        Vector3 pointZero = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRigthScreen = new Vector3(Screen.width, Screen.height, 0);
        Vector3 topRigthCorner = Camera.main.ScreenToWorldPoint(topRigthScreen);
        screenLeftEdge = pointZero.x;
        screenBottomEdge = pointZero.y;
        screenTopEdge = topRigthCorner.y;
        screenRigthEdge = topRigthCorner.x;
        SavedGame savedGame = GameManager.instance.dataManager.GetSavedGame();
        
        
        if (savedGame.health != 0)
        {
            health = savedGame.health;
            OnTakeDamage?.Invoke(health);
        }
        else
        {
            health = maxHealth;
        }
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        if(Mathf.Abs(moveDirection.x) > 0.2f || Mathf.Abs(moveDirection.y) > 0.2f)
        {
            rb.velocity = moveDirection * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

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

    public void vanishPlayer()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        animator.SetTrigger("Portal");
        speed = 0;
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

    public void GetCarPlains(PlayableCarModel carModel)
    {
        OnGetPlains?.Invoke(carModel);
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

    private void OnGetBlueCoin(int coinIndex)
    {
        
        GameObject numberImage = Instantiate(blueCoinNumbers[coinIndex - 1], new Vector2( transform.position.x,transform.position.y + 0.7f), Quaternion.identity);
        numberImage.transform.SetParent(transform);
    }

    public int GetHealth()
    {
        return health;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGetBlueCoin -= OnGetBlueCoin;
    }


    public event EventHandler OnDie;

    public event Action<int> OnTakeDamage;
    public event Action<PowerUpContainer> OnGetPowerUp;
    public event Action<PlayableCarModel> OnGetPlains;
}
