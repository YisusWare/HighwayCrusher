using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UnlockedCarPanel : MonoBehaviour
{

    [SerializeField]
    GameObject panel;
    [SerializeField]
    TextMeshProUGUI messageText;
    [SerializeField]
    Image carImage;
    // Start is called before the first frame update
    void Start()
    {

        panel.transform.localScale = Vector3.zero;
        PlayerController player = FindObjectOfType<PlayerController>();

        player.OnGetPlains += OnGetPlain;
    }

    public void OnGetPlain(PlayableCarModel carModel)
    {
        GameManager.instance.PauseGame();
        messageText.text = "El auto " + carModel.carName + " ya esta disponible en la tienda.";

        carImage.sprite = carModel.sprite;
        panel.transform.LeanScale(Vector3.one, 0.3f)
            .setIgnoreTimeScale(true);


    }

    public void ClosePanel()
    {
        panel.transform.LeanScale(Vector3.zero, 0.3f)
            .setIgnoreTimeScale(true);
        GameManager.instance.specialStage = false;
        GameManager.instance.ResumeGame();
        GameManager.instance.ChangeScene(1);
    }
}
