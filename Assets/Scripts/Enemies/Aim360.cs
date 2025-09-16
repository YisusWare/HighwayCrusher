using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim360 : MonoBehaviour
{
    [SerializeField]
    float MinAngle;
    [SerializeField]
    float MaxAngle;
    [SerializeField]
    protected float shootingCadence;
    [SerializeField]
    protected Transform shootingPoint;

    [SerializeField]
    protected GameObject bulletPrefab;

    Transform playerTransform;

    GameObject parent;

    bool shootFlag;

    [SerializeField]
    float playerDistanceThereshold;
    [SerializeField]
    float delay;
    Vector3 playerDirection;
    protected float shootingTime;

    float screenLeftEdge;
    float screenRigthEdge;
    float screenTopEdge;
    float screenBottomEdge;
    
    protected virtual void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().gameObject.transform;
        shootingTime = shootingCadence + delay;
        parent = transform.parent.gameObject;
        Vector3 pointZero = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRigthScreen = new Vector3(Screen.width, Screen.height, 0);
        Vector3 topRigthCorner = Camera.main.ScreenToWorldPoint(topRigthScreen);
        screenLeftEdge = pointZero.x;
        screenBottomEdge = pointZero.y;
        screenTopEdge = topRigthCorner.y;
        screenRigthEdge = topRigthCorner.x;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        float playerDistance = Vector2.Distance(transform.position, playerTransform.position);
        if(playerDistance <= playerDistanceThereshold)
        {
            shootFlag = true;
        }

        if(transform.position.x > screenRigthEdge || 
            transform.position.x < screenLeftEdge ||
            transform.position.y > screenTopEdge ||
            transform.position.y < screenBottomEdge)
        {
            shootFlag = false;
        }
       

        if (GameManager.instance.currentState == GameManager.GameState.gameScreen )
        {
            shootingTime -= Time.deltaTime;
            Vector3 Direction = playerTransform.position - transform.position;
            float angulo = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg; // Calcula el ángulo en grados

          
            float anguloLimitado = Mathf.Clamp(angulo, MinAngle, MaxAngle);

            // Aplica la rotación
            transform.rotation = Quaternion.Euler(0, 0, anguloLimitado);

            if (shootingTime <= 0 &&
                    shootFlag )
            {
                Shoot(Direction);
            }
        }
           
    }

    public virtual void Shoot(Vector3 direction)
    {
        shootingTime = shootingCadence;
        GameObject bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        EnemyBaseBullet bulletComponent = bulletInstance.GetComponent<EnemyBaseBullet>();
        EnemyBaseBullet baseBullet = bulletInstance.GetComponent<EnemyBaseBullet>();
        baseBullet.parent = parent;
        if(bulletComponent != null)
        {
            bulletComponent.Shoot(direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,playerDistanceThereshold);
    }

}
