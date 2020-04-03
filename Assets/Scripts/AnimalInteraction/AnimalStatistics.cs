using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStatistics : MonoBehaviour
{
    //holds the stats of the animal including:
    /*
    Name - added
    AnimalID - added
    FurID - added
    Bond - added
    Hunger - added
    Fur Growth - added
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
    private int animalBond = 0; //changing to public to be saved as well

    [Header("Bond to increase every feed/clean")]
    public int animalBondIncreasePerInteraction;

    private bool animalHappy;

    //stats tick delay should be shared with ALL objects
    [Header("Delay between ticks for stats")]
    public float statsTickDelayInput;

    private static float statsTickDelay;
    private float tickPreviousCheckTime;
    public static int furGrowthModifier = 20;

    void Start()
    {
        statsTickDelay = statsTickDelayInput;
        animalHappy = true; //changing this to a public variable to be saved from scene to scene
                            //otherwise every animal will be happy when scenes change

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

    public string GetAnimalName() // - added
    {
        return animalName;
    }

    public void ChangeAnimalName(string newName) // - added
    {
        animalName = newName;
    }

    //change Animal Fur Inventory Index
    public void SetAnimalFurInventoryIndex(int newFurInventoryIndex) // - added
    {
        animalFurInventoryIndex = newFurInventoryIndex;
    }

    public int GetAnimalFurInventoryIndex() // - added
    {
        return animalFurInventoryIndex;
    }


    // ///////////////////////////////INTERACTION STATS SYSTEM

    private void CheckAnimalHappy() // - added
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

    private void TickAnimalStats() // - added not implemented
    {
        //tick fur growth only if animal is happy
        if(animalHappy)
        {
            Debug.Log("Fur growing");
            ChangeAnimalFurGrowth(furGrowthModifier * GetAnimalBond());
        }


        //always tick hunger and cleanliness
        ChangeAnimalClean(-1);
        ChangeAnimalFood(-1);

    }
    //adjusting animal statistics
    //only want to ADJUST statistics of animal, not set fully
    //can pass in negative or positive integer

    public void ChangeAnimalClean(int cleanValueChange) // - added
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

    public int GetAnimalClean() // - added
    {
        return animalClean;
    }

    //animal hunger should always just be added
    public void ChangeAnimalFood(int hungerValueChange) // - added
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

    public int GetAnimalHunger() // - added
    {
        return animalFood;
    }


    //fur growth
    public void ChangeAnimalFurGrowth(int furGrowthValueChange) // - added
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
    public int GetAnimalFurGrowth() // - added
    {
        return animalFurGrowth;
    }
   

    //checks if FurGrowht is at 100, returns true if it is, false if it is NOT
    public bool CheckIfAnimalSheerable() // - added
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
    public void ChangeAnimalBond(int bondValueChange) // - added
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
    private void CheckToIncreaseBond(int valueChangeToCheck) //- added
    {
        if(valueChangeToCheck > 0)
        {
            ChangeAnimalBond(animalBondIncreasePerInteraction);
        }
    }

    //return Animal Bond divided by 100
    public int GetAnimalBond() // - added
    {
        return (animalBond/100);
    }
}
