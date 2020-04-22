using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShoppingManager : MonoBehaviour
{
    //Shopping Manager processes transactions within the Market, facilitates cost checks and transfer of items into player inventory

    [Header("Feedback text for shop")]
    public TextMeshProUGUI buySellAnnounceText;
    public Text moneyText;

    private bool announcingText = false;

    //variable for messing with coroutine logic
    private Coroutine lastCoroutine;

    private void Start()
    {

    }

    private void Update()
    {
        UpdateMoneyText();
    }

    //for referencing by scripts
    public int GetInventoryOwnedFurAtIndex(int itemIndex)
    {
        Debug.Log(itemIndex);

        return InventoryManager.instance.GetFurInventoryIndex(itemIndex);
    }

    //for testing, 100 is index 0, 500 is index 1, 1000 is index 2
    public void ProcessPurchase(int itemIndex, int itemBuyCost, int itemAmountBuying)
    {
        //checks if Player has enough money - stops if they don't
        if (InventoryManager.instance.GetMoney() < itemBuyCost)
        {
            
                //see text display below
                SetAnnounceText("Not enough money");

        }
        //Player has enough money
        else
        {
            //FindObjectOfType<AudioManager>().Play("Money");
            //see text display below
            SetAnnounceText("Got index in Index: " + itemIndex);

            //subtract money, add ONE to inventory in given index
            InventoryManager.instance.SubtractMoney(itemBuyCost);
            InventoryManager.instance.AddToFurInventory(itemIndex, itemAmountBuying);
        }
    }

    public void ProcessSell(int itemIndex, int itemSellCost, int itemAmountSelling)
    {
        //checks if Player has enough of item to sell in inventory to attempt sale - stop if they don't
        if(InventoryManager.instance.GetFurInventoryIndex(itemIndex) < itemAmountSelling)
        {
            SetAnnounceText("Not enough items in inventory");
        }

        //player has enough in inventory to sell!
        else
        {
            SetAnnounceText("Selling item in inventory index: " + itemIndex);


            //remove from inventory, add money
            InventoryManager.instance.SubtractFromFurInventory(itemIndex, itemAmountSelling);
            InventoryManager.instance.AddMoney(itemSellCost);
        }
    }


    //function to call to set announcement text that appears on screen for two seconds, should reset timer if clicked while running
    private void SetAnnounceText(string textToDisplay)
    {
        if (announcingText)
        {
            Debug.Log("Already displaying");
            StopCoroutine(lastCoroutine);
            announcingText = false;
        }

        //see text display below
        lastCoroutine = StartCoroutine(AnnounceTextTimer(textToDisplay));
    }

    //timer yield return enumerator
    IEnumerator AnnounceTextTimer(string announceText)
    {
        announcingText = true;
        buySellAnnounceText.SetText(announceText);

        yield return new WaitForSeconds(1f);

        buySellAnnounceText.SetText("");

        announcingText = false;


    }

    public void UpdateMoneyText()
    {
        moneyText.text = InventoryManager.instance.money.ToString();
    }

    //----------------------------Testing
    public void AddMoney()
    {
        InventoryManager.instance.money += 1000;
        //UpdateText();
    }

    //subtracts money from passed in amount
    public void SubtractMoney()
    {
        
        InventoryManager.instance.money -= 1000;
        //UpdateText();
    }

}
