using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float speed = -3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.currentState == GameManager.GameState.gameScreen)
        {
            transform.Translate(new Vector2(0, speed * Time.deltaTime));
        }
        


        if (transform.position.y <= -10)
        {
            Destroy(this.gameObject);
        }
    }

}
