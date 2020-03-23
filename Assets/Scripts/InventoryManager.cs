using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    static public int money = 0;
    public TextMeshProUGUI moneyUIText;

    private void Start()
    {
        UpdateText();
    }
    public void AddMoney(int increaseAmount)
    {
        money += increaseAmount;
        UpdateText();
    }

    public void SubtractMoney(int decreaseAmount)
    {
        money -= decreaseAmount;
        UpdateText();
    }

    private void UpdateText()
    {
        moneyUIText.SetText("Money: " + money);
    }
}
