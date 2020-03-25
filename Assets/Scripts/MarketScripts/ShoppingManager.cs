using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShoppingManager : MonoBehaviour
{
    //Shopping Manager processes transactions within the Market, facilitates cost checks and transfer of items into player inventory
    public GameObject InventoryManager;
    private InventoryManager inventoryManagerScript;

    [Header("Feedback text for shop")]
    public TextMeshProUGUI buySellAnnounceText;

    private bool announcingText = false;

    private void Start()
    {
        inventoryManagerScript = InventoryManager.GetComponent<InventoryManager>();
    }
    
    //for testing, 100 is index 0, 500 is index 1, 1000 is index 2
    public void ProcessPurchase(int itemIndex, int itemBuyCost, int itemAmountBuying)
    {
        //checks if Player has enough money - stops if they don't
        if (inventoryManagerScript.GetMoney() < itemBuyCost)
        {
            if (!announcingText)
            {
                //see text display below
                StartCoroutine(SetAnnounceText("Not enough money"));
            }
        }
        //Player has enough money
        else
        {
            //Boolean for making text appear smoothly
            if(!announcingText)
            {
                //see text display below
                StartCoroutine(SetAnnounceText("Got index in Index: " + itemIndex));
            }

            //subtract money, add ONE to inventory in given index
            inventoryManagerScript.SubtractMoney(itemBuyCost);
            inventoryManagerScript.AddToFurInventory(itemIndex, itemAmountBuying);
        }
    }

    public void ProcessSell(int itemIndex, int itemSellCost, int itemAmountSelling)
    {
        //checks if Player has enough of item to sell in inventory to attempt sale - stop if they don't
        if(inventoryManagerScript.GetFurInventoryIndex(itemIndex) < itemAmountSelling)
        {
            if(!announcingText)
            {
                //see text display below
                StartCoroutine(SetAnnounceText("Not enough items in inventory"));
            }
        }

        //player has enough in inventory to sell!
        else
        {
            if (!announcingText)
            {
                //see text display below
                StartCoroutine(SetAnnounceText("Selling item in inventory index: " + itemIndex));
            }

            //remove from inventory, add money
            inventoryManagerScript.SubtractFromFurInventory(itemIndex, itemAmountSelling);
            inventoryManagerScript.AddMoney(itemSellCost);
        }
    }


    //need to add functionality for Selling
    
    IEnumerator SetAnnounceText(string announceText)
    {
        announcingText = true;
        buySellAnnounceText.SetText(announceText);

        yield return new WaitForSeconds(1f);

        buySellAnnounceText.SetText("");

        announcingText = false;

    }
}
