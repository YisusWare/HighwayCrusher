using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField]
    Button continueButton;
    [SerializeField]
    TextMeshProUGUI coinsToContinueText;

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
        gameOverPanel.transform.LeanScale(Vector2.zero, 0.3f).setIgnoreTimeScale(true); ;
        GameManager.instance.ContinueGameWithCoins();
    }

    public void ContinueWithAd()
    {
        
        AdsManager.instance.rewardedAds.ShowRewardedAd();
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.transform.LeanScale(Vector2.zero, 0.3f)
            .setIgnoreTimeScale(true);
    }

    private void SetActive(object sender, EventArgs e)
    {
        int totalCoins = GameManager.instance.totalCollectedCoins;
        int coinsToContinue = GameManager.instance.coinsToContinueGame;

        coinsToContinueText.text = string.Concat(coinsToContinue, " x");
        gameOverPanel.transform.LeanScale(Vector2.one, 0.3f).setIgnoreTimeScale(true); ;
        highScoreText.text = GameManager.instance.dataManager.getHighScore() + "m";
        collectedCoinsText.text = string.Concat("Collected Coins: ",GameManager.instance.collectedCoins);
        totalCoinsText.text = string.Concat("Total Coins: ", totalCoins);


        if (totalCoins < coinsToContinue)
        {
            continueButton.interactable = false;
        }
    }
}
