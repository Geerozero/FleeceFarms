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
    public Text text;
    public GameObject player;
    private BasicInteractions basicInteractions;

    public Button pen01Button;
    public Button pen02Button;
    public Button pen03Button;
    public Button pen04Button;

    public GameObject pen01;
    public GameObject pen02;
    public GameObject pen03;
    public GameObject pen04;

    private Pen pen01Info;
    private Pen pen02Info;
    private Pen pen03Info;
    private Pen pen04Info;

    void Start()
    {
        basicInteractions = player.GetComponent<BasicInteractions>();
        pen01Info = pen01.GetComponent<Pen>();
        pen02Info = pen02.GetComponent<Pen>();
        pen03Info = pen03.GetComponent<Pen>();
        pen04Info = pen04.GetComponent<Pen>();
    }

    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            text.text = "Pick animal pen for:\n" + animalToCreate.ToString();
            CheckPens();
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

        pen01Info.AddAnimalToPen();
        PlayerManager.instance.playerSave.pen01Animals = pen01Info.GetCurrentAnimals();

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

        pen02Info.AddAnimalToPen();
        PlayerManager.instance.playerSave.pen02Animals = pen02Info.GetCurrentAnimals();

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

        pen03Info.AddAnimalToPen();
        PlayerManager.instance.playerSave.pen03Animals = pen03Info.GetCurrentAnimals();

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

        pen04Info.AddAnimalToPen();
        PlayerManager.instance.playerSave.pen04Animals = pen04Info.GetCurrentAnimals();

        if (LevelManager.instance.sheepToCreate > 0 || LevelManager.instance.alpacaToCreate > 0)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    void CheckPens()
    {
        if (PlayerManager.instance.playerSave.pen01Purchased && PlayerManager.instance.playerSave.pen01IsFull == false)
        {
            pen01Button.interactable = true;
        }
        if (!PlayerManager.instance.playerSave.pen01Purchased || PlayerManager.instance.playerSave.pen01IsFull == true)
        {
            if(PlayerManager.instance.playerSave.pen01IsFull == true)
            {
                pen01Button.GetComponentInChildren<Text>().text = "Pen Full";
            }
            pen01Button.interactable = false;
        }
        
        if (PlayerManager.instance.playerSave.pen02Purchased && PlayerManager.instance.playerSave.pen02IsFull == false)
        {
            pen02Button.interactable = true;
        }
        if (!PlayerManager.instance.playerSave.pen02Purchased || PlayerManager.instance.playerSave.pen02IsFull == true)
        {
            if (PlayerManager.instance.playerSave.pen02IsFull == true)
            {
                pen02Button.GetComponentInChildren<Text>().text = "Pen Full";
            }
            pen02Button.interactable = false;
        }
        
        if (PlayerManager.instance.playerSave.pen03Purchased && PlayerManager.instance.playerSave.pen03IsFull == false)
        {
            pen03Button.interactable = true;
        }
        if (!PlayerManager.instance.playerSave.pen03Purchased || PlayerManager.instance.playerSave.pen03IsFull == true)
        {
            if (PlayerManager.instance.playerSave.pen03IsFull == true)
            {
                pen03Button.GetComponentInChildren<Text>().text = "Pen Full";
            }
            pen03Button.interactable = false;
        }
        if (PlayerManager.instance.playerSave.pen04Purchased && PlayerManager.instance.playerSave.pen04IsFull == false)
        {
            pen04Button.interactable = true;
        }
        if (!PlayerManager.instance.playerSave.pen04Purchased || PlayerManager.instance.playerSave.pen04IsFull == true)
        {
            if (PlayerManager.instance.playerSave.pen04IsFull == true)
            {
                pen04Button.GetComponentInChildren<Text>().text = "Pen Full";
            }
            pen04Button.interactable = false;
        }
    }
}

