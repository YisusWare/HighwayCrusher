using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollectedCoinsLabel : MonoBehaviour
{
    TextMeshProUGUI coinsLabel;

    private void Start()
    {
        coinsLabel = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        coinsLabel.text = string.Concat("X ", GameManager.instance.totalCollectedCoins);
    }
}
