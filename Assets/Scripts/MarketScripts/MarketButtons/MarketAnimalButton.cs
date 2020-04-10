using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketAnimalButton : MonoBehaviour
{
    [Header("Animal Info")]
    public Animal.AnimalType animalType;
    public int cost;
    public Sprite image;

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
        inventoryManagerScript = inventoryManager.GetComponent<InventoryManager>();
        itemNameText.text = animalType.ToString();
        itemCostText.text = "Cost: " + cost.ToString();
        itemImage.sprite = image;
    }

    void Update()
    {
        if (InventoryManager.money < cost)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void BuyAnimal()
    {
        if(animalType == Animal.AnimalType.Sheep)
        {
            LevelManager.instance.sheepToCreate += 1;
        }
        if(animalType == Animal.AnimalType.Alpaca)
        {
            LevelManager.instance.alpacaToCreate += 1;
        }

        inventoryManagerScript.SubtractMoney(cost);
    }
}
