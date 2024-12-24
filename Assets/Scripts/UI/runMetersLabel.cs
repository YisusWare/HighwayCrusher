using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class runMetersLabel : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI metersText;
    void Start()
    {
        metersText = GetComponent<TextMeshProUGUI>();
       
    }

    private void Update()
    {
        metersText.text = Math.Truncate(GameManager.instance.metersRun) + " m";
    }


}
