using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UnlockedCarsSelector;
    public GameObject PreGamePanel;
    public GameObject ResumeGamePanel;
   
    public Image selectedCarImage;
    public List<Button> availableCarsButtons;
    [SerializeField]
    Slider speedSlider;
    [SerializeField]
    Slider accelerationSlider;
    [SerializeField]
    Slider maxHealthSlider;
    [SerializeField]
    Slider powerSlider;
    [SerializeField]
    TextMeshProUGUI selectedCarNameText;
    private void Start()
    {
        PreGamePanel.transform.localScale = Vector2.zero;
        ResumeGamePanel.transform.localScale = Vector2.zero;
        SelectCar(0);
    }

    private void InitializeUnlockedCarsSelection()
    {

        
        GameManager.instance.dataManager.LoadData();
        var carSprites = GameManager.instance.dataManager.playableCars;
        

        PlayableCarRegistry[] UnlockedCars = GameManager.instance.dataManager.GetUnlockedCars();

        foreach (var car in UnlockedCars)
        {
            GameObject newButton = new GameObject("" + car.CarId);

            RectTransform rectTransform = newButton.AddComponent<RectTransform>();

            rectTransform.sizeDelta = new Vector2(270, 550);

            Image buttonImage = newButton.AddComponent<Image>();
            buttonImage.sprite = carSprites[car.CarId].sprite;
            buttonImage.color = Color.white;  // Color de fondo del botón (puedes usar sprites aquí)

            // Añadir el componente Button
            Button buttonComponent = newButton.AddComponent<Button>();
            ColorBlock color = buttonComponent.colors;

            color.selectedColor = new Color(1, 1, 1, 0.5f);
            color.disabledColor = new Color(1, 1, 1, 0.5f);
            availableCarsButtons.Add(buttonComponent);
            newButton.transform.SetParent(UnlockedCarsSelector.transform, false);
            int capturedIndex = car.CarId;
            buttonComponent.onClick.AddListener(() => SelectCar(capturedIndex));
        }
            
        
    }

    public void SelectCar(int id)
    {
        
        GameManager.instance.selectedCar = GameManager.instance.dataManager.playableCars[id];
        PlayerController carController = GameManager.instance.selectedCar.prefab.GetComponent<PlayerController>();
        selectedCarImage.sprite = GameManager.instance.selectedCar.sprite;
        selectedCarImage.gameObject.SetActive(true);
        speedSlider.value = carController.speed;
        accelerationSlider.value = carController.Acceleration;
        maxHealthSlider.value = carController.maxHealth;
        powerSlider.value = carController.Power;
        selectedCarNameText.text = GameManager.instance.selectedCar.carName;


    }

    public void OpenSelectionPanel()
    {
        if (GameManager.instance.dataManager.IsSavedData())
        {
            //open continue game panel
            ResumeGamePanel.transform.LeanScale(Vector2.one, 0.3f);

            return;
        }

        //open selection car panel
        OpenNewGamePanel();

    }

    public void OpenNewGamePanel()
    {
        ResumeGamePanel.transform.LeanScale(Vector2.zero, 0.3f);
        foreach (RectTransform child in UnlockedCarsSelector.transform)
        {
            Destroy(child.gameObject);
        }


        InitializeUnlockedCarsSelection();
        PreGamePanel.transform.LeanScale(Vector2.one, 0.3f);
    }

    public void CloseSelectionPanel()
    {
        PreGamePanel.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
        
    }


    public void Play()
    {

        GameManager.instance.DeleteSavedGameData();
        SceneManager.LoadScene(1);
        GameManager.instance.isPlayerDead = false;
    }

    public void ResumeGame()
    {
        GameManager.instance.StartSavedGame();

        int nextScene = GameManager.instance.savedGame.SceneIndex;
        SceneManager.LoadScene(nextScene);
    }
}
