 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Animal : MonoBehaviour
{
    /*--------------------Animal Traits-------------------*/

    public enum AnimalType
    {
        Sheep, Alpaca, Rabbit
    }
    
    [Header("Animal Identification")]
    public new string name;
    public int animalID;
    public AnimalType animalType;
    public string pen;

    [Header("Animal Appearance")]
    public FurItem fur;
    public GameObject wool;
    public OutfitItem slot01;
    public OutfitItem slot02;
    public OutfitItem slot03;
    

    /*--------------------Animal Stats-------------------*/

    private int animalFurInventoryIndex = 0; //index within inventory array

    [Header("Watching stats")]
    [SerializeField]
    private bool isHappy;
    [SerializeField]
    private bool isFleeceGrown;
    [SerializeField]
    private int animalClean = 0;
    [SerializeField]
    private int animalFood = 0;
    [SerializeField]
    private int animalBond;
    [SerializeField]
    private int animalFurGrowth = 0;

    [Header("Bond to increase every feed/clean")]
    private int animalBondIncreasePerInteraction;

    //stats tick delay should be shared with ALL objects
    private static float statsTickDelayInput = 2;

    private static float statsTickDelay;
    private float tickPreviousCheckTime;
    private static int furGrowthModifier = 20;


    /*--------------------Animal Movement-------------------*/
    [Header("Time to let animal wait in place")]
    public float timeWaiting;
    private float lastTimeMoved;
    private Scene currentScene;

    private Vector3 nextPositionToMoveTo;
    private GameObject penObject;
    private NavMeshAgent navAgent;
    private bool isMoving;

    /*--------------------Animal Animation-------------------*/
    [Header("Animators")]
    public Animator bodyAnimator;
    public Animator woolAnimator;

    /*---------------------Animal Particle Effects----------------*/
    [Header("Drag in the respective particle effects in prefab")]
    public GameObject eatHayParticleForFEED;
    public GameObject dustParticleForBRUSH;
    private bool isParticlePlaying;
    private Coroutine particleCoroutine;

    /*------------------------------Animal Data Management----------------------------*/

    public LevelManager.AnimalSave GetAnimalSave()
    {
        /* Add additional animal traits here
         * you must also add the new trait to the LevelManager.AnimalSave class as well */

        LevelManager.AnimalSave newSave = new LevelManager.AnimalSave();
        newSave.animalName = name;
        newSave.animalID = animalID;
        newSave.animalType = animalType;
        newSave.assignedPen = pen;
        
        newSave.furID = fur.furID;
        newSave.slot01ClothID = slot01.clothingID;
        newSave.slot02ClothID = slot02.clothingID;
        newSave.slot03ClothID = slot03.clothingID;

        newSave.isFurGrown = isFleeceGrown;
        newSave.animalIsHappy = isHappy;
        newSave.animalClean = animalClean;
        newSave.animalHunger = animalFood;
        newSave.animalFurGrowth = animalFurGrowth;

        newSave.position = transform.position;
        newSave.rotation = transform.eulerAngles;

        return newSave;
    }

    public void LoadAnimalSave(LevelManager.AnimalSave save)
    {
        /* Loads animal with save info
         * Save info contains the last saved fur, clothing items, and animal stats
         * and gives them original position and rotation in Farm Scene */

        name = save.animalName;
        animalID = save.animalID;
        animalType = save.animalType;
        pen = save.assignedPen;
        
        fur = FurManager.instance.furs[save.furID];
        slot01 = ClothingManager.instance.clothes[save.slot01ClothID];
        slot02 = ClothingManager.instance.clothes[save.slot02ClothID];
        slot03 = ClothingManager.instance.clothes[save.slot03ClothID];

        isFleeceGrown = save.isFurGrown;
        isHappy = save.animalIsHappy;
        animalClean = save.animalClean;
        animalFood = save.animalHunger;
        animalFurGrowth = save.animalFurGrowth;

        transform.position = save.position;
        transform.eulerAngles = save.rotation;
    }

    /*------------------------------Animal Stat Management----------------------------*/

    void Start()
    {
      
        currentScene = SceneManager.GetActiveScene();
        statsTickDelay = statsTickDelayInput;


        /*-------Movement*------*/
        //get navmesh agent of this object
        if(currentScene.name == "Farm_design")
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            penObject = GameObject.Find(pen);
            isMoving = false;
            lastTimeMoved = Time.time;
        }

        /* --- Variable Declaration */
        animalBond = 100; //start with 100 bond for 1 Bond Point
        furGrowthModifier = 20; //modifier for how much fur grows per tick if Happy
        animalBondIncreasePerInteraction = 10; //how many bond to increase, 100 bond = 1 Bond Point
        //isFleeceGrown = false; //has no wool at the start! - moved to update bcuz it keeps resetting fleece on scene transitions

        StopMovingAnimations();

        /* --- Particle Effect Declarations/Starts---*/
        isParticlePlaying = false;
        StopParticleEffect(eatHayParticleForFEED);
        StopParticleEffect(dustParticleForBRUSH);

    }

    void Update()
    {
        /*STATS
        * Tick system to constantly update the status of animals
        * Basically as time passes animal becomes hungry, gets dirty, and grows wool
        */
        
        if(currentScene.name == "Farm_design")
        {
            if (Time.time - tickPreviousCheckTime > statsTickDelay)
            {
                tickPreviousCheckTime = Time.time;

                //check happiness always
                CheckAnimalHappy();

                //tick all stats
                TickAnimalStats();
            }

            CheckToDisplayClothing(); //call function to hide fleece[changed to clothing]

            if(isFleeceGrown)
            {
                wool.gameObject.SetActive(true);
            }
            else if(!isFleeceGrown)
            {
                wool.gameObject.SetActive(false);
            }

            /*MOVEMENT
             * Animal should randomly move around to a random point in a circle around the animal
             * 
             */
            AnimalWander();
        } 
    }

    public string GetAnimalName()
    {
        return name;
    }

    public void ChangeAnimalName(string newName)
    {
        name = newName;
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

    /*--------------------Animal Interaction System-------------------*/

    public void CheckAnimalHappy()
    {
        if (animalClean > 0 && animalFood > 0)
        {
            isHappy = true;
        }

        //check clean status
        else if (animalClean <= 0)
        {
            isHappy = false;
        }

        //check hunger status
        else if (animalFood <= 0)
        {
            isHappy = false;
        }
    }

    //tick animal stats at regular intervals
    private void TickAnimalStats()
    {
        //tick fur growth only if animal is happy
        if (isHappy)
        {
            ChangeAnimalFurGrowth(furGrowthModifier * GetAnimalBond());
        }


        //always tick hunger and cleanliness
        ChangeAnimalClean(-1);
        ChangeAnimalFood(-1);

        //tick check to display fleece
        CheckToDisplayClothing();
    }

    //adjusting animal statistics
    //only want to ADJUST statistics of animal, not set fully
    //can pass in negative or positive integer
    public void ChangeAnimalClean(int cleanValueChange)
    {
        animalClean += cleanValueChange;

        bool cleanPositiveCheck;
        cleanPositiveCheck = CheckToIncreaseBond(cleanValueChange);

        if(cleanPositiveCheck)
        {
            PlayParticleEffect(dustParticleForBRUSH, 2.0f);
            //add partical effect/audio to play HERE
        }

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
        animalFood += hungerValueChange;

        bool foodPositiveCheck;
        foodPositiveCheck = CheckToIncreaseBond(hungerValueChange);


        if(foodPositiveCheck)
        {
            PlayParticleEffect(eatHayParticleForFEED, 2.0f);
            //add particle effect/audio to play HERE
            //should be as simple as adding and subtracting it
        }

        //set lower and upper bounds
        if (animalFood > 100)
        {
            animalFood = 100;
        }

        if (animalFood < 0)
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
        if (animalFurGrowth >= 100)
        {
            //anytime fur growth is at >100, set fleece grown bool true
            isFleeceGrown = true;
            animalFurGrowth = 100;
        }
        else
        {
            isFleeceGrown = false;
        }

        if (animalFurGrowth <= 0)
        {
            animalFurGrowth = 0;
        }
    }

    //get fur growth
    public int GetAnimalFurGrowth()
    {
        return animalFurGrowth;
    }

    //function to change visbility of fleece
    private void CheckToDisplayClothing() //- changed function name to dealing with activating and deactiviating only the animals clothing
    {
        //if fleece is not grown, set mesh renderer of fleece/wool OFF
        if(!isFleeceGrown)
        {
            /* was having problems with enabling and diabling the mesh renderer
             * when customizing in the boutique
             * so changed moved this to Update and setting the wool gameobject to active and inactive */

            //wool.GetComponentInChildren<MeshRenderer>().enabled = false;
            
            for(int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).tag == "Accessory")
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        //else, turn it on
        else
        {
           // wool.GetComponentInChildren<MeshRenderer>().enabled = true;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Accessory")
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    public bool GetIsFleeceGrown()
    {
        return isFleeceGrown;
    }

    //checks if FurGrowht is at 100, returns true if it is, false if it is NOT
    public bool CheckIfAnimalSheerable()
    {
        if (animalFurGrowth >= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*--------------------Animal Bonding System-------------------*/
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
    private bool CheckToIncreaseBond(int valueChangeToCheck)
    {
        //if positive value, return true
        if (valueChangeToCheck > 0)
        {
            ChangeAnimalBond(animalBondIncreasePerInteraction);
            return true;
        }

        return false;
    }

    //return Animal Bond divided by 100
    public int GetAnimalBond()
    {
        return (animalBond / 100);
    }


    /*--------------------Animal Movement System-------------------*/
    private void AnimalWander()
    {
        if (isMoving)           //if currently moving - check remaining distance
        {
            //checks if within navAgent stopping distance
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                //stop moving and stop animation
                isMoving = false;
                StopMovingAnimations();

                //sets time that animal stopped moving
                lastTimeMoved = Time.time;

                
            }
        }
        else    //if done moving, check if enough time passed to move to next random position
        {
            //let animal wait before moving it
            if (Time.time - lastTimeMoved >= timeWaiting)
            {
                //set moving boolean true
                isMoving = true;

                //get random position inside a sphere in assigned pen
                nextPositionToMoveTo = penObject.transform.position + (Random.insideUnitSphere * 30);
                //move to location
                navAgent.SetDestination(nextPositionToMoveTo);

                //begin moving animation
                PlayMovingAnimations();
            }
        }
    }
    /*--------------------Animal Animation-------------------*/
    //animators have boolean for animation -> change them here
    private void PlayMovingAnimations()
    {
        bodyAnimator.SetBool("isWalking", true);
        woolAnimator.SetBool("isWalking", true);
    }


    private void StopMovingAnimations()
    {
        bodyAnimator.SetBool("isWalking", false);
        woolAnimator.SetBool("isWalking", false);
    }


    /*--------------------Animal Particle Effect System-------------------*/
    private void PlayParticleEffect(GameObject particleEffectGameobject, float timeBeforeStoppingParticle)
    {
        //if particle effect was alreayd playing, stop coroutine, stop particle
        if(isParticlePlaying)
        {
            StopParticleEffect(particleEffectGameobject);
            StopCoroutine(particleCoroutine);
        }


        particleCoroutine = StartCoroutine(TimedParticleEffect(particleEffectGameobject, timeBeforeStoppingParticle));
    }

    //stop particle effect!
    private void StopParticleEffect(GameObject particleEffectToStop)
    {
        particleEffectToStop.GetComponent<ParticleSystem>().Stop();
    }

    //put a timer on the particle effect to stop it so that it plays properly next time it's called
    IEnumerator TimedParticleEffect(GameObject particleEffectGameobject,float timeBeforeStoppingParticle)
    {
        //reset particle if already playing
        if(isParticlePlaying)
        {
            StopParticleEffect(particleEffectGameobject);
        }

        particleEffectGameobject.GetComponent<ParticleSystem>().Play();
        isParticlePlaying = true;

        yield return new WaitForSeconds(timeBeforeStoppingParticle);

        particleEffectGameobject.GetComponent<ParticleSystem>().Stop();
        isParticlePlaying = false;
    }

}
