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

    void Update()
    {
        text.text = "Pick animal pen for: " + animalToCreate.ToString();
    }

    //picks correct spawn point(in pen of user's choice) for animals that were bought

    public void PickPen1()
    {
        penChoice = 1;
        LevelManager.instance.SpawnNewAnimal(animalToCreate, LevelManager.instance.spawnPen01);

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
        LevelManager.instance.SpawnNewAnimal(animalToCreate, LevelManager.instance.spawnPen02);
        
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
