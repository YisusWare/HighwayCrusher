using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaxonConcrete : MonoBehaviour
{
    [SerializeField]
    int power;
    [SerializeField]
    LayerMask enemyLayer;
    private CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        StartCoroutine(cameraShake.Shake(0.3f, 0.2f));
        MakeDamage();
        
    }

    private void MakeDamage()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, 2.3f, enemyLayer);
        
        foreach (Collider2D collider in enemyColliders)
        {
            BreakableObject breakableObject = collider.gameObject.GetComponent<BreakableObject>();
            if (breakableObject != null)
            {
                breakableObject.TakeDamage(power);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelfDestroy()
    {
        cameraShake.transform.localPosition = new Vector3(0, 0, cameraShake.transform.localPosition.z);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {


        // Dibujar el círculo en 2D
        Gizmos.DrawWireSphere(transform.position, 2.3f);
    }
}
