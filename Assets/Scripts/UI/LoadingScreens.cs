using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreens : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void ChangeScene(int sceneIndex)
    {
        animator.SetBool("ShowScreen", true);
        StartCoroutine(ChangeSceneCoroutine(sceneIndex));
    }

    public void HideLoadingScreen() 
    {
        animator.SetBool("ShowScreen", false);
    }

    private IEnumerator ChangeSceneCoroutine(int nextScene)
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(nextScene);
    }
}
