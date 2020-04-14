using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketFurButton : MonoBehaviour
{
    [Header("Associated Fur Item")]
    public FurItem furItem;

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
        /*---Connecting fur item attributes to this button---*/
        button = GetComponent<Button>();
        itemNameText.text = furItem.name;
        itemCostText.text = "Cost: " + furItem.cost.ToString();
        itemImage.sprite = furItem.buttonImage;

        inventoryManagerScript = inventoryManager.GetComponent<InventoryManager>();

        if(furItem.isPurchased)
        {
            button.interactable = false;
        }
    }

    void Update()
    {
        if (furItem.isPurchased || InventoryManager.money < furItem.cost)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void BuyFur()
    {
        /*---Unlocks fur item in boutique---*/

        furItem.isPurchased = true;
        button.interactable = false;
        inventoryManagerScript.SubtractMoney(furItem.cost);
    }
}
