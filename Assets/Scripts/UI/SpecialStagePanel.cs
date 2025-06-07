using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialStagePanel : MonoBehaviour
{
    [SerializeField]
    GameObject specialStagePanel;
    void Start()
    {
        specialStagePanel.transform.localScale = Vector2.one;
    }

    public void StartSpecialStage()
    {
        GameManager.instance.PlaySpecialStageMusic();
        specialStagePanel.transform.LeanScale(Vector2.zero, 0.3f);
        GameManager.instance.currentState = GameManager.GameState.gameScreen;
    }
}
