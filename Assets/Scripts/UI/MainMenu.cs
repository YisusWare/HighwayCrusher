using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


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

    //Item Shop Section

    [SerializeField] GameObject ShopContainer;
    [SerializeField] int ItemsAmmount;
    [SerializeField] Sprite ItemButtonSprite;
    [SerializeField] Button buyButton;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] TextMeshProUGUI currentCoinsText;
    [SerializeField] GameObject bougthItemsContainer;


    private List<GameObject> itemButtons = new List<GameObject>();

    private int selectedItemIndex;
    
    private int selectedItemButtonSibilingIndex;

    private List<int> bougthItems = new List<int>();

    private int currentCoins;

   

    private List<PowerUpContainer> availableItems = new List<PowerUpContainer>();
    private void Start()
    {
        PreGamePanel.transform.localScale = Vector2.zero;
        ResumeGamePanel.transform.localScale = Vector2.zero;
        SelectCar(0);

        buyButton.interactable = false;
        LoadItemButtons();
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
        GameManager.instance.SetBoughtItems(bougthItems);
    }

    public void ResumeGame()
    {
        GameManager.instance.StartSavedGame();

        int nextScene = GameManager.instance.savedGame.SceneIndex;
        SceneManager.LoadScene(nextScene);
    }

    public void LoadItemButtons()
    {

        currentCoins = GameManager.instance.dataManager.getTotalCoins();

        currentCoinsText.text = "$ " + currentCoins;


        PowerUpContainer[] powerUps = GameManager.instance.dataManager.powerUpsGlobalList;

        for (int i = 0; i < ItemsAmmount; i++)
        {

            int positionIndex = i;
            int randomIndex = Random.Range(0, powerUps.Length);

            PowerUpContainer powerUpAux = powerUps[randomIndex];
            availableItems.Add(powerUpAux);

            GameObject newButton = new GameObject("ItemBtn_" + (i));
            //create the icon of the item
            GameObject itemIcon = new GameObject("ItemIcon_" + (i));
            RectTransform iconTransform = itemIcon.AddComponent<RectTransform>();

            iconTransform.sizeDelta = new Vector2(270, 270);

            Image itemIconSprite = itemIcon.AddComponent<Image>();
            itemIconSprite.sprite = powerUps[randomIndex].sprite;

            RectTransform rectTransform = newButton.AddComponent<RectTransform>();

            rectTransform.sizeDelta = new Vector2(300, 300);



            Image buttonImage = newButton.AddComponent<Image>();
            buttonImage.sprite = ItemButtonSprite;
            buttonImage.color = Color.white;

            Button buttonComponent = newButton.AddComponent<Button>();

            ColorBlock color = buttonComponent.colors;

            color.selectedColor = new Color(1, 1, 1, 0.5f);
            color.disabledColor = new Color(1, 1, 1, 0.5f);
            

            buttonComponent.onClick.AddListener(() => SelectItem(positionIndex, newButton));

            newButton.transform.SetParent(ShopContainer.transform, false);
            itemIcon.transform.SetParent(newButton.transform, false);

            itemButtons.Add(newButton);


        }
    }

    public void SelectItem(int itemIndex,GameObject button)
    {

        buyButton.interactable = true;

        PowerUpContainer selectedItem = availableItems[itemIndex];

        itemPrice.text = "$" + selectedItem.price;

        selectedItemIndex = itemIndex;
        selectedItemButtonSibilingIndex = button.transform.GetSiblingIndex();
    }

    public void BuyItem()
    {
        PowerUpContainer selectedItem = availableItems[selectedItemIndex];

        int coinsAux = GameManager.instance.dataManager.getTotalCoins() - selectedItem.price;


        GameManager.instance.dataManager.setTotalCoins(coinsAux);

        currentCoinsText.text = "$" + coinsAux;

        GameManager.instance.dataManager.SaveData();

        //actually save item
        bougthItems.Add(selectedItem.index);

        //destroy the item button
        Destroy(itemButtons[selectedItemButtonSibilingIndex]);
        itemButtons.RemoveAt(selectedItemButtonSibilingIndex);

        //bougthItemsContainer
        GameObject bougthItemIconContainer = new GameObject("boughtItem");

        GameObject bougthItemIcon = new GameObject("boughtItem");

        RectTransform iconTransform = bougthItemIcon.AddComponent<RectTransform>();

        Image bougthItemIconSprite = bougthItemIcon.AddComponent<Image>();
        bougthItemIconSprite.sprite = selectedItem.sprite;

        iconTransform.sizeDelta = new Vector2(150, 150);

        Image buttonImage = bougthItemIconContainer.AddComponent<Image>();
        buttonImage.sprite = ItemButtonSprite;
        buttonImage.color = Color.white;

        RectTransform iconContainerTransform = bougthItemIconContainer.GetComponent<RectTransform>();
        iconContainerTransform.sizeDelta = new Vector2(250, 250);

        bougthItemIconContainer.transform.SetParent(bougthItemsContainer.transform, false);
        bougthItemIcon.transform.SetParent(bougthItemIconContainer.transform, false);

        buyButton.interactable = false;

        if(bougthItems.Count >= 3)
        {
            foreach(var button in itemButtons)
            {
                button.GetComponent<Button>().interactable = false;

                GameObject icon = button.transform.GetChild(0).gameObject;
                icon.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            }
        }
    }

    public void clearItemButtons()
    {
        foreach (RectTransform child in ShopContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
