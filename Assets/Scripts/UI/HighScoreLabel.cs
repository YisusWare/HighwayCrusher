using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreLabel : MonoBehaviour
{
    TextMeshProUGUI highScoreText;
    void Start()
    {
        highScoreText = GetComponent<TextMeshProUGUI>();
        GameManager.instance.dataManager.LoadData();
        highScoreText.text = GameManager.instance.dataManager.getHighScore().ToString() + "m";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
