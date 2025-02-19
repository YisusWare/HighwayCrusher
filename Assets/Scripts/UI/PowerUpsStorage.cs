using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsStorage : MonoBehaviour
{
    GameObject buttonsContainer;
    [SerializeField]
    List<GameObject> buttons;
    [SerializeField]
    List<PowerUpContainer> powerUps;
    [SerializeField]
    Sprite ItemButtonSprite;

    private void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        var savedPowerUps = GameManager.instance.GetSavedPowerUps();

        var boughtItems = GameManager.instance.GetBoughtItems();

        player.OnGetPowerUp += AddPowerUpToList;
        buttons = new List<GameObject>();
        powerUps = new List<PowerUpContainer>();

        if(boughtItems.Count > 0)
        {
            foreach (var powerUp in boughtItems)
            {
                AddPowerUpToList(powerUp);
            }
        }
        else
        {
            if (savedPowerUps.Count > 0)
            {
                foreach (var powerUp in savedPowerUps)
                {
                    AddPowerUpToList(powerUp);
                }
            }
        }
        
        
    }

    public List<PowerUpContainer> GetPowerUps()
    {
        return powerUps;
    }


    public void AddPowerUpToList(PowerUpContainer powerUp)
    {

        if(buttons.Count >= 3)
        {
            DestroyPowerUp(0);
        }

        //the current button
        GameObject newButton = new GameObject("ItemBtn_" + (buttons.Count + 1));
        //create the icon of the item
        GameObject itemIcon = new GameObject("ItemIcon_" + (buttons.Count + 1));

        RectTransform iconTransform = itemIcon.AddComponent<RectTransform>();
        iconTransform.sizeDelta = new Vector2(150, 150);
        Image itemIconSprite = itemIcon.AddComponent<Image>();
        itemIconSprite.sprite = powerUp.sprite;

        RectTransform rectTransform = newButton.AddComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(180, 330);

        Image buttonImage = newButton.AddComponent<Image>();
        buttonImage.sprite = ItemButtonSprite;
        buttonImage.color = Color.white;

        Button buttonComponent = newButton.AddComponent<Button>();

        ColorBlock color = buttonComponent.colors;

        color.selectedColor = new Color(1, 1, 1, 0.5f);
        color.disabledColor = new Color(1, 1, 1, 0.5f);

        newButton.transform.SetParent(this.gameObject.transform, false);
        itemIcon.transform.SetParent(newButton.transform, false);
        buttonComponent.onClick.AddListener(()=> powerUp.Activate(this, newButton));

        buttons.Add(newButton);
        powerUps.Add(powerUp);

    }

    public void DestroyPowerUp(int index)
    {
        Destroy(buttons[index]);
        buttons.RemoveAt(index);
        powerUps.RemoveAt(index);
    }

}
