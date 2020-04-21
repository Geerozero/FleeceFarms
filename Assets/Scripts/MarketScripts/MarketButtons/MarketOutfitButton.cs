using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketOutfitButton : MonoBehaviour
{
    [Header("Associated Outfit Item")]
    public OutfitItem outfitItem;

    [Header("Button Referenecs")]
    public Text itemCostText;
    public Text itemNameText;
    public Image itemImage;

    /*---Private Variables---*/
    private Button button;

    void Start()
    {
        /*---Connecting outfit item attributes to this button---*/
        button = GetComponent<Button>();
        itemNameText.text = outfitItem.name;
        itemCostText.text = "Cost: " + outfitItem.cost.ToString();
        itemImage.sprite = outfitItem.buttonImage;
    }
    void Update()
    {
        if (outfitItem.isPurchased || InventoryManager.instance.money < outfitItem.cost)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void BuyClothing()
    {
        /*---Unlocks clothing item in boutique---*/
        Debug.Log("Buying: " + outfitItem.name);
        outfitItem.isPurchased = true;
        button.interactable = false;
        InventoryManager.instance.SubtractMoney(outfitItem.cost);
    }
}
