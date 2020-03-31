using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MarketItemIndexProcessing : MonoBehaviour
{
    //would change eventually to ItemID? For now it's a public variable to edit on the button prefab
    [Header("Manually adjust these per button")]
    public bool isSellButton;
    public int itemIndex;
    public int itemBUYCost;
    public int itemSELLCost;
    public int itemAmountBuying;
    public int itemAmountSelling;

    [Header("Drag in child OwnedAmountText here")]
    public TextMeshProUGUI ownedUIText;

    //get shopping manager for reference
    [Header("Drag in Manager here")]
    public GameObject gameManager;

    private ShoppingManager shoppingManagerScript;


    [Header("Inventory manager heere")]
    public InventoryManager inventoryScript;


    private void Start()
    {
        shoppingManagerScript = gameManager.GetComponent<ShoppingManager>();        
    }


    //Both are for buttons to call as buttons only take single parameter functions so need this as a handler
    public void PurchaseSingle()
    {
        Debug.Log("Attempting to purchase item!");
        shoppingManagerScript.ProcessPurchase(itemIndex, itemBUYCost, itemAmountBuying);
        UpdateOwnedAmountText();
    }

    public void SellSingle()
    {
        Debug.Log("Attempt to sell item!");
        shoppingManagerScript.ProcessSell(itemIndex, itemSELLCost, itemAmountSelling);
        UpdateOwnedAmountText();
    }

    //set display text for UI display
    public void UpdateOwnedAmountText()
    {
        if (isSellButton)
        {
            ownedUIText.SetText(inventoryScript.GetFurInventoryIndex(itemIndex).ToString());
        }
    }
}
