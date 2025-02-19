using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarShop : MonoBehaviour
{
    [SerializeField]
    private GameObject carsContainer;
    private PlayableCarModel selectedCar;
    [SerializeField]
    TextMeshProUGUI selectedCarName;
    [SerializeField]
    TextMeshProUGUI selectedCarPrice;
    [SerializeField]
    Button buyButton;
    private int currentCoins;
    [SerializeField]
    TextMeshProUGUI currentCoinsText;
    List<Button> AvailableCarsButtons;
    public GameObject ShopPanel;
    [SerializeField]
    Slider speedSlider;
    [SerializeField]
    Slider accelerationSlider;
    [SerializeField]
    Slider maxHealthSlider;
    [SerializeField]
    Slider powerSlider;
    [SerializeField]
    Image selectedCarImage;
    [SerializeField]
    Sprite CarCursorImage;

    GameObject itemCursor;
    int selectedCarButtonIndex = 0;
    
    private void Start()
    {
        ShopPanel.transform.localScale = Vector2.zero;
        AvailableCarsButtons = new List<Button>();
        
    }

    public void InitializeAvailableCars()
    {
        GameManager.instance.dataManager.LoadData();
        var carSprites = GameManager.instance.dataManager.playableCars;
        currentCoins = GameManager.instance.dataManager.getTotalCoins();
        currentCoinsText.text = "Your cash: $" + currentCoins;

        itemCursor = new GameObject("ItemCursor");
        RectTransform cursorTransform = itemCursor.AddComponent<RectTransform>();
        cursorTransform.sizeDelta = new Vector2(350, 630);
        Image CursorImage = itemCursor.AddComponent<Image>();

        CursorImage.sprite = CarCursorImage;

        foreach (var car in carSprites)
        {

            if (car.canBuyIt) { 
                GameObject newButton = new GameObject("" + car.Id);

                RectTransform rectTransform = newButton.AddComponent<RectTransform>();

                rectTransform.sizeDelta = new Vector2(160, 300);

                Image buttonImage = newButton.AddComponent<Image>();
                buttonImage.sprite = car.sprite;
                buttonImage.color = Color.white;

                Button buttonComponent = newButton.AddComponent<Button>();
                ColorBlock color = buttonComponent.colors;

                color.selectedColor = new Color(1, 1, 1, 0.5f);
                color.disabledColor = new Color(1, 1, 1, 0.5f);

                buttonComponent.interactable = !car.unlocked;

                newButton.transform.SetParent(carsContainer.transform, false);
                buttonComponent.onClick.AddListener(() => SetSelectedCar(car,newButton));
                AvailableCarsButtons.Add(buttonComponent);
            }
        }

        
    }

    public void OpenShopPanel()
    {
        foreach (RectTransform child in carsContainer.transform)
        {
            Destroy(child.gameObject);
        }
        InitializeAvailableCars();
        ShopPanel.transform.LeanScale(Vector2.one, 0.3f);

    }

    public void CloseShopPanel()
    {
        ShopPanel.transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
    }

    private void SetSelectedCar(PlayableCarModel car,GameObject button)
    {

        
        itemCursor.SetActive(true);
        itemCursor.transform.SetParent(button.transform,false);
        itemCursor.transform.position = button.transform.position;
        selectedCar = car;
        
        selectedCarName.text = selectedCar.carName;
        selectedCarPrice.text = "$" + selectedCar.price;
        
        if (car.price <= currentCoins)
        {
            buyButton.interactable = true;
        }

        PlayerController selectedCarPlayer = selectedCar.prefab.GetComponent<PlayerController>();
        speedSlider.value = selectedCarPlayer.speed;
        accelerationSlider.value = selectedCarPlayer.Acceleration;
        maxHealthSlider.value = selectedCarPlayer.maxHealth;
        powerSlider.value = selectedCarPlayer.Power;
        selectedCarImage.sprite = selectedCar.sprite;
        selectedCarImage.gameObject.SetActive(true);
        selectedCarButtonIndex = button.transform.GetSiblingIndex();
        Debug.Log(selectedCarButtonIndex);
    }

    public void BuyCar()
    {

        itemCursor.SetActive(false);
        GameManager.instance.dataManager.setTotalCoins(currentCoins - selectedCar.price);

        GameManager.instance.dataManager.LoadData();

        GameManager.instance.dataManager.BuyCar(selectedCar);
        currentCoins = GameManager.instance.dataManager.getTotalCoins();
        currentCoinsText.text = "Your cash: $" + currentCoins;
        AvailableCarsButtons[selectedCarButtonIndex].interactable = false;
        buyButton.interactable = false;
        selectedCarImage.gameObject.SetActive(false);

        selectedCarName.text = "";
        selectedCarPrice.text = "";
        speedSlider.value = 0;
        accelerationSlider.value = 0;
        maxHealthSlider.value = 0;
        powerSlider.value = 0;

        GameManager.instance.SetPlayableCarsStatus();

    }
}
