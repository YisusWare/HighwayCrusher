using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDeamon : BreakableObject
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    
    Vector3 targetPoint;
    float shootingTime;
    [SerializeField]
    float shootingCadence;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform shootingPoint;
    [SerializeField]
    float delay;
    GameObject parent;
    Transform playerTransform;
    bool shootingFlag = false;
    [SerializeField]
    Transform targetPointTransform;

    float screenLeftEdge;
    float screenRigthEdge;
    float screenTopEdge;
    float screenBottomEdge;

    protected override void Start()
    {
        base.Start();
        Debug.Log("Running");
        rb = GetComponent<Rigidbody2D>();

        Vector3 pointZero = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRigthScreen = new Vector3(Screen.width, Screen.height, 0);
        Vector3 topRigthCorner = Camera.main.ScreenToWorldPoint(topRigthScreen);
        screenLeftEdge = pointZero.x;
        screenBottomEdge = pointZero.y;
        screenTopEdge = topRigthCorner.y;
        screenRigthEdge = topRigthCorner.x;

        targetPoint = new Vector3(Random.Range(screenLeftEdge + 1f, screenRigthEdge - 1f), transform.position.y + Random.Range(-1,1), transform.position.z);
        parent = gameObject;
        playerTransform = FindObjectOfType<PlayerController>().gameObject.transform;
        
        shootingTime = shootingCadence + delay;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameState.gameScreen)
        {
            shootingTime -= Time.deltaTime;
            
            Vector3 Direction = playerTransform.position - transform.position;

            if (shootingTime <= 0 && shootingFlag)
            {
                animator.SetTrigger("Attack");
                Shoot(Direction);
            }
        }
    }

    private void FixedUpdate()
    {
        if(Vector2.Distance(targetPoint,transform.position) >= 0.5f)
        {
            rb.AddForce((targetPoint - transform.position).normalized * 2);
            
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("Stay", true);
            shootingFlag = true;
        }
    }

    public virtual void Shoot(Vector3 direction)
    {
        shootingTime = shootingCadence;
        GameObject bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        EnemyBaseBullet bulletComponent = bulletInstance.GetComponent<EnemyBaseBullet>();
        EnemyBaseBullet baseBullet = bulletInstance.GetComponent<EnemyBaseBullet>();
        baseBullet.parent = parent;
        if (bulletComponent != null)
        {
            bulletComponent.Shoot(direction);
        }
    }

    public override void DestroyEnemy()
    {
        base.DestroyEnemy();

        if(transform.parent.childCount <= 1)
        {
            Destroy(transform.parent);
        }
    }


}
