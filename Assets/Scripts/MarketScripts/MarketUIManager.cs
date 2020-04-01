using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketUIManager : MonoBehaviour
{
    //multiple objects to set active when pressing buy and set deactivated when selling
    //[Header("UI to set active when pressing Buy")]
    //public GameObject[] ObjectsForBuying;

    //same for above
    [Header("UI to set active when pressing Sell")]
    public GameObject[] ObjectsForSelling;

    [Header("Sell buttons")]
    public GameObject[] SellButtons;

    [Header("UI Elements")]
    public GameObject buyContainer;
    public GameObject animalsContainer;
    public GameObject furscontainer;
    public GameObject clothesContainer;

    public GameObject sellContainer;
    public GameObject fleeceContainer;

    /* The buttons need to be added manually bcuz each one is associated with a specifc item
     * so there's no need to go through an array of of them to display them
     * I'm not sure about the sell buttons, you may be able to add them in this way
    
        //set UI objects true/false for BUY
    public void DisplayBuy()
    {

        SetActiveStateOfArray(ObjectsForBuying, true);
        SetActiveStateOfArray(ObjectsForSelling, false);
    } */

    void Start()
    {
        DisplayBuyOptions();
    }

    //set UI objects true/false for SELL
    public void DisplaySell()
    {
        SetActiveStateOfArray(ObjectsForSelling, true);
        //SetActiveStateOfArray(ObjectsForBuying, false);

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

    /*---Display UI elements---*/

    public void DisplayBuyOptions()
    {
        buyContainer.SetActive(true);
        animalsContainer.SetActive(true);

        furscontainer.SetActive(false);
        clothesContainer.SetActive(false);
        sellContainer.SetActive(false);
    }

    public void DisplayAnimalsToBuy()
    {
        animalsContainer.SetActive(true);
        furscontainer.SetActive(false);
        clothesContainer.SetActive(false);
    }

    public void DisplayFursToBuy()
    {
        animalsContainer.SetActive(false);
        furscontainer.SetActive(true);
        clothesContainer.SetActive(false);
    }

    public void DisplayClothesToBuy()
    {
        animalsContainer.SetActive(false);
        furscontainer.SetActive(false);
        clothesContainer.SetActive(true);
    }

    public void DisplaySellOptions()
    {
        sellContainer.SetActive(true);
        fleeceContainer.SetActive(true);

        buyContainer.SetActive(false);
    }

    public void GoToFarm()
    {
        SceneManager.LoadScene("Farm_test");
    }
    
}
