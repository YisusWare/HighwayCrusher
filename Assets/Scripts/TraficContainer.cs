using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficContainer : MonoBehaviour
{
    [SerializeField]
    GameObject lastCar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, -5 * Time.deltaTime));

        if (lastCar.transform.position.y <= -10)
        {
            Destroy(this.gameObject);
        }
    }
}
