using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketItemIndexProcessing : MonoBehaviour
{
    //would change eventually to ItemID? For now it's a public variable to edit on the button prefab
    [Header("Manually adjust these per button")]
    public int itemIndex;
    public int itemBUYCost;
    public int itemSELLCost;
    public int itemAmountBuying;
    public int itemAmountSelling;

    //get shopping manager for reference
    [Header("Drag in ShoppingManager here")]
    public ShoppingManager shoppingManagerScript;


    //Both are for buttons to call as buttons only take single parameter functions so need this as a handler
    public void PurchaseSingle()
    {
        Debug.Log("Attempting to purchase item!");
        shoppingManagerScript.ProcessPurchase(itemIndex, itemBUYCost, itemAmountBuying);

    }

    public void SellSingle()
    {
        Debug.Log("Attempt to sell item!");
        shoppingManagerScript.ProcessSell(itemIndex, itemSELLCost, itemAmountSelling);
    }

}
