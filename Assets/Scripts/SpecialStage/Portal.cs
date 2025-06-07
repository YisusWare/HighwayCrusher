using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    int nextSceneIndex;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if(player != null)
            {
                player.vanishPlayer();
            }
            GameManager.instance.specialStage = true;
            GameManager.instance.currentState = GameManager.GameState.gamePaused;
            animator.SetTrigger("Activated");
            GameManager.instance.ChangeScene(nextSceneIndex);
            GameManager.instance.SaveCurrentGame();
        }
    }

    public void OnChangeScene()
    {
       
    }
}
