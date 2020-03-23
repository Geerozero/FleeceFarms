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
    public TextMeshProUGUI buySellAnnounceText;
    private bool announcingText = false;

    private void Start()
    {
        inventoryManagerScript = InventoryManager.GetComponent<InventoryManager>();
    }

    public void CheckIfEnoughMoney(int moneyCostToCheck)
    {
        //expand this to add from inventory
        if(inventoryManagerScript.GetMoney() < moneyCostToCheck)
        {
            StartCoroutine(SetAnnounceText("Not enough money"));

        }
        else
        {
            if(!announcingText)
            {
                StartCoroutine(SetAnnounceText("Got Item"));
            }
            inventoryManagerScript.SubtractMoney(moneyCostToCheck);
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
