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
        text.text = "Pick animal pen for: " + animalToCreate.ToString();
    }

    //picks correct spawn point(in pen of user's choice) for animals that were bought

    public void PickPen1()
    {
        penChoice = 1;
        GameObject newAnimal = LevelManager.instance.SpawnAnimalType(animalToCreate);
        Animal animalInfo = newAnimal.GetComponent<Animal>();
        
        LevelManager.instance.InitializeNewAnimal(animalInfo, animalToCreate);

        animalInfo.wool.GetComponent<MeshRenderer>().material = FurManager.instance.furs[animalInfo.fur.furID].furMaterial;
        LevelManager.instance.SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot01.clothingID);
        LevelManager.instance.SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot02.clothingID);
        LevelManager.instance.SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot03.clothingID);

        newAnimal.transform.position = basicInteractions.spawnPen01.transform.position;

        LevelManager.instance.animals.Add(animalInfo);

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
        GameObject newAnimal = LevelManager.instance.SpawnAnimalType(animalToCreate);
        Animal animalInfo = newAnimal.GetComponent<Animal>();
        
        LevelManager.instance.InitializeNewAnimal(animalInfo, animalToCreate);

        animalInfo.wool.GetComponent<MeshRenderer>().material = FurManager.instance.furs[animalInfo.fur.furID].furMaterial;
        LevelManager.instance.SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot01.clothingID);
        LevelManager.instance.SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot02.clothingID);
        LevelManager.instance.SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot03.clothingID);
        
        newAnimal.transform.position = basicInteractions.spawnPen02.transform.position;

        LevelManager.instance.animals.Add(animalInfo);

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
