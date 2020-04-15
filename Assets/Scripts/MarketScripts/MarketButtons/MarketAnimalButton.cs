using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    /*---Private Variables---*/
    private Button button;

    void Start()
    {
        /*---Connecting fur item attributes to this button---*/
        button = GetComponent<Button>();
        itemNameText.text = animalType.ToString();
        itemCostText.text = "Cost: " + cost.ToString();
        itemImage.sprite = image;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Market")
        {
            if (InventoryManager.instance.money < cost || CheckTotalAnimalsAllowedWithCurrentPens() == false)
            {
                button.interactable = false;
            }
            else if (InventoryManager.instance.money >= cost && CheckTotalAnimalsAllowedWithCurrentPens() == true)
            {
                button.interactable = true;
            }
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

        PlayerManager.instance.playerSave.animalsBought++;
        InventoryManager.instance.SubtractMoney(cost);
    }

    private bool CheckTotalAnimalsAllowedWithCurrentPens()
    {
        if(PlayerManager.instance.playerSave.pensBought == 0)
        {
            return false;
        }
        if (PlayerManager.instance.playerSave.pensBought == 1 && PlayerManager.instance.playerSave.animalsBought + 1 > 20)
        {
            return false;
        }
        if(PlayerManager.instance.playerSave.pensBought == 2 && PlayerManager.instance.playerSave.animalsBought + 1 > 40)
        {
            return false;
        }
        if(PlayerManager.instance.playerSave.pensBought == 3 && PlayerManager.instance.playerSave.animalsBought + 1 > 60)
        {
            return false;
        }
        if(PlayerManager.instance.playerSave.pensBought == 4 && PlayerManager.instance.playerSave.animalsBought + 1 > 80)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
