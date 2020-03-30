using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStatistics : MonoBehaviour
{
    //holds the stats of the animal including:
    /*
    Name
    AnimalID
    FurID
    Bond
    Hunger
    Fur Growth
    */

    //this should implement Kaitlin's Animal ID aspects later (this week probably)

    private string animalName;
    //AniamlID here
    //FurID here
    private int animalBond = 0;
    private int animalHunger = 0;
    private int animalFurGrowth = 0;
 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //adjusting animal statistics
    //only want to ADJUST statistics of animal, not set fully
    //can pass in negative or positive integer
    public void changeAnimalBond(int bondValueChange)
    {
        animalBond += bondValueChange;
    }

    public void changeAnimalHunger(int hungerValueChange)
    {
        animalHunger += hungerValueChange;
    }

    public void changeAnimalFurGrowth(int furGrowthValueChange)
    {
        animalFurGrowth += furGrowthValueChange;

        if(animalFurGrowth > 100)
        {
            animalFurGrowth = 100;
        }
    }

    //get variable functions
    public int GetAnimalBond()
    {
        return animalBond;
    }

    public int GetAnimalHunger()
    {
        return animalHunger;
    }

    public int GetAnimalFurGrowth()
    {
        return animalFurGrowth;
    }
}
