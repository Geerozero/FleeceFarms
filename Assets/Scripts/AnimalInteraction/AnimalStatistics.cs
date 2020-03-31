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

    public string animalName;
    //AniamlID here
    //FurID here
    private int animalFurInventoryIndex = 0; //index within inventory array

    [Header("Watching these stats")]
    [SerializeField]
    private int animalClean = 0;
    [SerializeField]
    private int animalFood = 0;


    [SerializeField]
    private int animalFurGrowth = 0;
    [SerializeField]
    private int animalBond = 0;

    [Header("Bond to increase every feed/clean")]
    public int animalBondIncreasePerInteraction;

    private bool animalHappy;

    //stats tick delay should be shared with ALL objects
    [Header("Delay between ticks for stats")]
    public float statsTickDelayInput;

    private static float statsTickDelay;
    private float tickPreviousCheckTime;

    void Start()
    {
        statsTickDelay = statsTickDelayInput;
        animalHappy = true;

        animalBond = 100;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        * Tick system to constantly update the status of animals
        * Basically as time passes animal becomes hungry, gets dirty, and grows wool
        */

        if (Time.time - tickPreviousCheckTime > statsTickDelay)
        {
            Debug.Log("Stats tick");

            tickPreviousCheckTime = Time.time;

            //tick all stats
            TickAnimalStats();

            //check happiness always
            CheckAnimalHappy();
        }
    }


    //change Animal Fur Inventory Index
    public void SetAnimalFurInventoryIndex(int newFurInventoryIndex)
    {
        animalFurInventoryIndex = newFurInventoryIndex;
    }

    public int GetAnimalFurInventoryIndex()
    {
        return animalFurInventoryIndex;
    }


    // ///////////////////////////////INTERACTION STATS SYSTEM

    private void CheckAnimalHappy()
    {
        if(animalClean > 0 && animalFood > 0)
        {
            animalHappy = true;
            Debug.Log("Happy animal!");
        }

        //check clean status
        else if(animalClean <= 0)
        {
            animalHappy = false;
            Debug.Log("Unhappy: dirty");
        }

        //check hunger status
        else if(animalFood <= 0)
        {
            animalHappy = false;
            Debug.Log("Unhappy: hungry");
        }

        
        
    }

    private void TickAnimalStats()
    {
        //tick fur growth only if animal is happy
        if(animalHappy)
        {
            Debug.Log("Fur growing");
            ChangeAnimalFurGrowth(5 * GetAnimalBond());
        }


        //always tick hunger and cleanliness
        ChangeAnimalClean(-1);
        ChangeAnimalFood(-1);

    }
    //adjusting animal statistics
    //only want to ADJUST statistics of animal, not set fully
    //can pass in negative or positive integer

    public void ChangeAnimalClean(int cleanValueChange)
    {
        CheckToIncreaseBond(cleanValueChange);
        animalClean += cleanValueChange;

        //set lower and upper bounds
        if (animalClean > 100)
        {
            animalClean = 100;
        }

        if (animalClean < 0)
        {
            animalClean = 0;
        }
    }

    public int GetAnimalClean()
    {
        return animalClean;
    }

    //animal hunger should always just be added
    public void ChangeAnimalFood(int hungerValueChange)
    {
        CheckToIncreaseBond(hungerValueChange);
        animalFood += hungerValueChange;

        //set lower and upper bounds
        if (animalFood > 100)
        {
            animalFood = 100;
        }

        if(animalFood < 0)
        {
            animalFood = 0;
        }
    }

    public int GetAnimalHunger()
    {
        return animalFood;
    }


    //fur growth
    public void ChangeAnimalFurGrowth(int furGrowthValueChange)
    {
        animalFurGrowth += furGrowthValueChange;

        //set lower and upper bounds
        if (animalFurGrowth > 100)
        {
            animalFurGrowth = 100;
        }

        if(animalFurGrowth < 0)
        {
            animalFurGrowth = 0;
        }
    }

    //get fur growth
    public int GetAnimalFurGrowth()
    {
        return animalFurGrowth;
    }
   

    //checks if FurGrowht is at 100, returns true if it is, false if it is NOT
    public bool CheckIfAnimalSheerable()
    {
        if(animalFurGrowth >= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // //////////////////////////////////////BOND SYSTEM////////////

    //animal Bond is general affection for player that increases as player interacts with animal
    //bond can constantly increase up to a max of 1000
    //bond make Fur grow faster
    public void ChangeAnimalBond(int bondValueChange)
    {
        animalBond += bondValueChange;

        //set lower and upper bounds
        if (animalBond > 1000)
        {
            animalBond = 1000;
        }

        if (animalBond < 0)
        {
            animalBond = 0;
        }
    }

    //call whenever feeding or cleaning to check if a positive value is being added, if so add bond points
    private void CheckToIncreaseBond(int valueChangeToCheck)
    {
        if(valueChangeToCheck > 0)
        {
            ChangeAnimalBond(animalBondIncreasePerInteraction);
        }
    }

    //return Animal Bond divided by 100
    public int GetAnimalBond()
    {
        return (animalBond/100);
    }
}
