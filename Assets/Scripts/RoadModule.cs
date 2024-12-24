using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadModule : MonoBehaviour
{
    [SerializeField]
    public int nextBiomeId;
    [SerializeField]
    public int[] adyacentModules;
    [SerializeField]
    public bool transitionModule;
    public float speed = -3f;
    
    private void Start()
    {
        
    }
    private void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameState.gameScreen)
        {
            transform.Translate(new Vector2(0, speed * Time.deltaTime));
        }

        if(transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
    }
}
