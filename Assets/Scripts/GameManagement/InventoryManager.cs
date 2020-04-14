using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    //Inventory manager focuses on what money the player has, what kinds of Fur owned by player and what amounts, and what clothes they have purchased

    public static InventoryManager instance;

    //money should be a static variable
    //[Header("Money!")]
    public int money;
    //public TextMeshProUGUI moneyUIText;

    //the array of these should match the array of shop values
    [Header("Inventory array")]
    public static int[] ownedFurInventory = new int[] { 0, 0, 0 };

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateText();
    }

    //updates money text within UI
    private void UpdateText()
    {
        //moneyUIText.SetText("Money: " + money);
    }

    //adds money to inventory from passed in amount
    public void AddMoney(int increaseAmount)
    {
        money += increaseAmount;
        UpdateText();
    }

    //subtracts money from passed in amount
    public void SubtractMoney(int decreaseAmount)
    {
        money -= decreaseAmount;
        UpdateText();
    }

    //variable Getter
    public int GetMoney()
    {
        return money;
    }

    /////////INVENTORY
    ///

    public void AddToFurInventory(int furArray, int amountToAdd)
    {
        ownedFurInventory[furArray] += amountToAdd;

        //print amount in log
        Debug.Log("Have " + ownedFurInventory[furArray] + " furs in Index:" + furArray);
    }

    public void SubtractFromFurInventory(int furArray, int amountToSubtract)
    {
        ownedFurInventory[furArray] -= amountToSubtract;

        //print amount debug
        Debug.Log("Have " + ownedFurInventory[furArray] + " furs in Index:" + furArray);
    }

    public int GetFurInventoryIndex(int furArray)
    {
        int returnNum = ownedFurInventory[furArray];
        return returnNum;
    }
}
