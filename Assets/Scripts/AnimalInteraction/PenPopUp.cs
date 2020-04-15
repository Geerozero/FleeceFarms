using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.UI;

public class PenPopUp : MonoBehaviour
{
    public int penChoice = 0;
    public Animal.AnimalType animalToCreate;
    public TextMeshProUGUI text;
    public GameObject player;
    private BasicInteractions basicInteractions;

    public Button pen01Button;
    public Button pen02Button;
    public Button pen03Button;
    public Button pen04Button;

    private void Start()
    {
        basicInteractions = player.GetComponent<BasicInteractions>();
    }

    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            text.text = "Pick animal pen for:\n" + animalToCreate.ToString();
            CheckPurchasedPens();
        }
    }

    //picks correct spawn point(in pen of user's choice) for animals that were bought

    public void PickPen1()
    {
        penChoice = 1;
        LevelManager.instance.SpawnAnimalTypeWithLocation(animalToCreate, basicInteractions.spawnPen01);

        if (animalToCreate == Animal.AnimalType.Sheep)
            LevelManager.instance.sheepToCreate--;
        else if (animalToCreate == Animal.AnimalType.Alpaca)
            LevelManager.instance.alpacaToCreate--;

        if (LevelManager.instance.sheepToCreate > 0 || LevelManager.instance.alpacaToCreate > 0)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    public void PickPen2()
    {
        penChoice = 2;
        LevelManager.instance.SpawnAnimalTypeWithLocation(animalToCreate, basicInteractions.spawnPen02);

        if (animalToCreate == Animal.AnimalType.Sheep)
            LevelManager.instance.sheepToCreate--;
        else if (animalToCreate == Animal.AnimalType.Alpaca)
            LevelManager.instance.alpacaToCreate--;

        if (LevelManager.instance.sheepToCreate > 0 || LevelManager.instance.alpacaToCreate > 0)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    public void PickPen3()
    {
        penChoice = 3;
        LevelManager.instance.SpawnAnimalTypeWithLocation(animalToCreate, basicInteractions.spawnPen03);

        if (animalToCreate == Animal.AnimalType.Sheep)
            LevelManager.instance.sheepToCreate--;
        else if (animalToCreate == Animal.AnimalType.Alpaca)
            LevelManager.instance.alpacaToCreate--;

        if (LevelManager.instance.sheepToCreate > 0 || LevelManager.instance.alpacaToCreate > 0)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    public void PickPen4()
    {
        penChoice = 4;
        LevelManager.instance.SpawnAnimalTypeWithLocation(animalToCreate, basicInteractions.spawnPen04);

        if (animalToCreate == Animal.AnimalType.Sheep)
            LevelManager.instance.sheepToCreate--;
        else if (animalToCreate == Animal.AnimalType.Alpaca)
            LevelManager.instance.alpacaToCreate--;

        if (LevelManager.instance.sheepToCreate > 0 || LevelManager.instance.alpacaToCreate > 0)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    void CheckPurchasedPens()
    {
        if (PlayerManager.instance.playerSave.pen01Purchased)
        {
            pen01Button.interactable = true;
        }
        if (!PlayerManager.instance.playerSave.pen01Purchased)
        {
            pen01Button.interactable = false;
        }
        if (PlayerManager.instance.playerSave.pen02Purchased)
        {
            pen02Button.interactable = true;
        }
        if (!PlayerManager.instance.playerSave.pen02Purchased)
        {
            pen02Button.interactable = false;
        }
        if (PlayerManager.instance.playerSave.pen03Purchased)
        {
            pen03Button.interactable = true;
        }
        if (!PlayerManager.instance.playerSave.pen03Purchased)
        {
            pen03Button.interactable = false;
        }
        if (PlayerManager.instance.playerSave.pen04Purchased)
        {
            pen04Button.interactable = true;
        }
        if (!PlayerManager.instance.playerSave.pen04Purchased)
        {
            pen04Button.interactable = false;
        }
    }
}

