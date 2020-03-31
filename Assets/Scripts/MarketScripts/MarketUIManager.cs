using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketUIManager : MonoBehaviour
{
    //multiple objects to set active when pressing buy and set deactivated when selling
    [Header("UI to set active when pressing Buy")]
    public GameObject[] ObjectsForBuying;

    //same for above
    [Header("UI to set active when pressing Sell")]
    public GameObject[] ObjectsForSelling;

    [Header("Sell buttons")]
    public GameObject[] SellButtons;



    //set UI objects true/false for BUY
    public void DisplayBuy()
    {

        SetActiveStateOfArray(ObjectsForBuying, true);
        SetActiveStateOfArray(ObjectsForSelling, false);
    }

    //set UI objects true/false for SELL
    public void DisplaySell()
    {
        SetActiveStateOfArray(ObjectsForSelling, true);
        SetActiveStateOfArray(ObjectsForBuying, false);

        UpdateSellButtonsOwnedNumber();
    }


    //go through array and set state of objects according to passed in boolean
    private void SetActiveStateOfArray(GameObject[] objectArray, bool stateToSet)
    {
        int length = objectArray.Length;
        int i;

        for(i = 0; i < length; ++i)
        {
            objectArray[i].SetActive(stateToSet);
        }
    }

    //update how many is owned by player
    private void UpdateSellButtonsOwnedNumber()
    {
        int length = SellButtons.Length;
        int i;
        MarketItemIndexProcessing buttonScriptReference;

        for (i = 0; i < length; i++)
        {
            SellButtons[i].SetActive(true);

            buttonScriptReference = SellButtons[i].GetComponent<MarketItemIndexProcessing>();

            buttonScriptReference.UpdateOwnedAmountText();
        }
    }
    
}
