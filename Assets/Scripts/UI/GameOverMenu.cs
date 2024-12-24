using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    TextMeshProUGUI highScoreText;
    [SerializeField]
    TextMeshProUGUI collectedCoinsText;
    [SerializeField]
    TextMeshProUGUI totalCoinsText;

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.OnDie += SetActive;
    }

    public void Restart()
    {
        GameManager.instance.restartGame();

    }

    public void GoToMainMenu()
    {
        GameManager.instance.mainMenu();
    }

    private void SetActive(object sender, EventArgs e)
    {
        gameOverPanel.SetActive(true);
        highScoreText.text = GameManager.instance.dataManager.getHighScore() + "m";
        collectedCoinsText.text = string.Concat("Collected Coins: ",GameManager.instance.collectedCoins);
        totalCoinsText.text = string.Concat("Collected Coins: ",GameManager.instance.totalCollectedCoins);
    }
}
