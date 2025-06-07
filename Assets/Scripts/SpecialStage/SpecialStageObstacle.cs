using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialStageObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if(player != null)
            {
                player.vanishPlayer();
            }
            GameManager.instance.specialStage = false;
            Debug.Log("Touch enemie");
            GameManager.instance.ChangeScene(1);
        }
    }
}
