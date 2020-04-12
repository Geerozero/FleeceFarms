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

    private void Start()
    {
        basicInteractions = player.GetComponent<BasicInteractions>();
    }

    void Update()
    {
        text.text = "Pick animal pen for:\n" + animalToCreate.ToString();
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
}

