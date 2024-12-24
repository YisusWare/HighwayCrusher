using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject PausePanel;

    private void Start()
    {
        PausePanel.transform.localScale = Vector2.zero;
    }
    public void PauseGame()
    {
        GameManager.instance.PauseGame();
        PausePanel.transform.LeanScale(Vector2.one, 0.3f);
    }

    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();
        PausePanel.transform.LeanScale(Vector2.zero, 0.3f);
    }

    public void ExitGame()
    {
        GameManager.instance.ExitGame();
    }
}
