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
        gameOverPanel.transform.localScale = Vector2.zero;
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

    public void ContinueWithCoins()
    {
        gameOverPanel.transform.LeanScale(Vector2.zero, 0.3f);
        GameManager.instance.ContinueGameWithCoins();
    }

    public void ContinueWithAd()
    {
        gameOverPanel.transform.LeanScale(Vector2.zero, 0.3f);
        AdsManager.instance.rewardedAds.ShowRewardedAd();
    }

    private void SetActive(object sender, EventArgs e)
    {
        gameOverPanel.transform.LeanScale(Vector2.one, 0.3f);
        highScoreText.text = GameManager.instance.dataManager.getHighScore() + "m";
        collectedCoinsText.text = string.Concat("Collected Coins: ",GameManager.instance.collectedCoins);
        totalCoinsText.text = string.Concat("Collected Coins: ",GameManager.instance.totalCollectedCoins);
    }
}
