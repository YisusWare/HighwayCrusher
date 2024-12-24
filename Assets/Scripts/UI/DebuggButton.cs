using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebuggButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI savedFileText;
    public void PrintSavedDataFile()
    {
        Debug.Log("printed");
        savedFileText.text = GameManager.instance.GetDataFileText();
    }
}
