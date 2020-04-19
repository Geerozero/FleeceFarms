using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketUpgradeButton : MonoBehaviour
{
    public Button button;
    public Image greyedImage;
    public GameObject titleContainer;
    public Text titleText;
    public Text costText;
    public Text levelText;
    
    [Header("Shear Upgrade Cost Increases")]
    public int cost;
    public int costIncreaseOne;
    public int costIncreaseTwo;
    public int costIncreaseThree;

    [Header("Fur Sale Price Increases")]
    public int levelTwo;
    public int levelThree;
    public int levelFour;
    public int levelFive;

    void Start()
    {
        titleText.text = "Upgrade Shears";
        CheckCost();
        costText.text = "Upgrade Cost: " + cost.ToString();
        levelText.text = "Shear Level: " + (PlayerManager.instance.playerSave.shearLevel + 1).ToString();
    }

    void Update()
    {
        if (InventoryManager.instance.money < cost || PlayerManager.instance.playerSave.shearLevel >= 4)
        {
            if(PlayerManager.instance.playerSave.shearLevel >= 4)
            {
                FinalUpgrade();
            }
            
            greyedImage.gameObject.SetActive(true);
            titleContainer.SetActive(false);
            button.interactable = false;
        }
        else
        {
            titleContainer.SetActive(true);
            greyedImage.gameObject.SetActive(false);
            button.interactable = true;
        }
    }


    public void UpgradeShears()
    {
        if(PlayerManager.instance.playerSave.shearLevel == 0)
        {
            for(int i = 0; i < FurManager.instance.furs.Count; i++)
            {
                FurManager.instance.furs.ElementAt(i).Value.sellPrice = levelTwo;
            }

            PlayerManager.instance.playerSave.shearLevel++;
            InventoryManager.instance.money -= cost;

            cost += costIncreaseOne;
            UpdateCostText(cost, PlayerManager.instance.playerSave.shearLevel + 1);
        }
        else if (PlayerManager.instance.playerSave.shearLevel == 1)
        {
            for (int i = 0; i < FurManager.instance.furs.Count; i++)
            {
                FurManager.instance.furs.ElementAt(i).Value.sellPrice = levelThree;
            }

            PlayerManager.instance.playerSave.shearLevel++;
            InventoryManager.instance.money -= cost;

            cost += costIncreaseTwo;
            UpdateCostText(cost, PlayerManager.instance.playerSave.shearLevel + 1);
        }
        else if (PlayerManager.instance.playerSave.shearLevel == 2)
        {
            for (int i = 0; i < FurManager.instance.furs.Count; i++)
            {
                FurManager.instance.furs.ElementAt(i).Value.sellPrice = levelFour;
            }

            PlayerManager.instance.playerSave.shearLevel++;
            InventoryManager.instance.money -= cost;

            cost += costIncreaseThree;
            UpdateCostText(cost, PlayerManager.instance.playerSave.shearLevel + 1);
        }
        else if (PlayerManager.instance.playerSave.shearLevel == 3)
        {
            for (int i = 0; i < FurManager.instance.furs.Count; i++)
            {
                FurManager.instance.furs.ElementAt(i).Value.sellPrice = levelFive;
                button.interactable = false;
            }

            PlayerManager.instance.playerSave.shearLevel++;
            InventoryManager.instance.money -= cost;

            FinalUpgrade();
        }
    }

    void UpdateCostText(int newCost, int newLevel)
    {
        costText.text = "Upgrade Cost: " + newCost.ToString();
        levelText.text = "Shear Level: " + newLevel.ToString();
    }

    void FinalUpgrade()
    {
        titleText.text = "Fully Upgraded";
        costText.gameObject.SetActive(false);
        levelText.text = "Shear Level: 5";
    }

    void CheckCost()
    {
        if (PlayerManager.instance.playerSave.shearLevel == 0)
        {
            cost = 1000;
        }
        else if (PlayerManager.instance.playerSave.shearLevel == 1)
        {
            cost = 1500;
        }
        else if (PlayerManager.instance.playerSave.shearLevel == 2)
        {
            cost = 3000;
        }
        else if (PlayerManager.instance.playerSave.shearLevel == 3)
        {
            cost = 5000;
        }
    }
}
