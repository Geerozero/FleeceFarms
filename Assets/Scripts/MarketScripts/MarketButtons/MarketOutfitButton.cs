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
    public GameObject inventoryManager;

    /*---Private Variables---*/
    private Button button;
    private InventoryManager inventoryManagerScript;

    void Start()
    {
        /*---Connecting outfit item attributes to this button---*/
        button = GetComponent<Button>();
        itemNameText.text = outfitItem.name;
        itemCostText.text = "Cost: " + outfitItem.cost.ToString();
        itemImage.sprite = outfitItem.buttonImage;

        inventoryManagerScript = inventoryManager.GetComponent<InventoryManager>();
    }
    void Update()
    {
        if (outfitItem.isPurchased || InventoryManager.money < outfitItem.cost)
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

        outfitItem.isPurchased = true;
        button.interactable = false;
        inventoryManagerScript.SubtractMoney(outfitItem.cost);
    }
}
