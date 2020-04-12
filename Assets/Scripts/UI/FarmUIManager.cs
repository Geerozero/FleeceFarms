using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FarmUIManager : MonoBehaviour
{
    [Header("Feedback text")]
    public TextMeshProUGUI farmAnnounceText;
    bool announcingText;

    [Header("UI References")]
    public GameObject FarmUIContainer;
    public GameObject marketButton;
    public GameObject pauseMenu;
    public TextMeshProUGUI editNameButtonText;

    [Header("Animal Name text")]
    public TextMeshProUGUI animalNameText;

    //camera script for resetting camera to view
    [Header("Camera reference")]
    public CameraFarmMovement cameraScript;

    [Header("Inventory Manager")]
    public InventoryManager inventory;

    //boolean for other functions to check
    [SerializeField]
    private bool isInteracting;

    //reference for Animal being interacted with for working with and calling functions
    private GameObject animalReference;
    private Animal animalStats;

    private string animalName;
    [Header("Name field")]
    public GameObject nameInputTextField;

    //variable for messing with coroutine logic
    private Coroutine lastCoroutine;

    private LevelManager.AnimalSave animalSave;

    private void Start()
    {
        isInteracting = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
    }

    //SCENE TRANSITIONS

    public void LoadMarket()
    {
        LevelManager.instance.SaveAnimalsToFile();
        SceneManager.LoadScene("Market");
    }

    public void LoadBoutique()
    {
        BoutiqueManager.instance.selectedAnimal = animalSave;
        LevelManager.instance.SaveAnimalsToFile();
        SceneManager.LoadScene("Boutique");
    }

    public void SetIsInteracting(bool newInteractingStatus)
    {
        Debug.Log("Set interacting!");
        isInteracting = newInteractingStatus;
    }

    public bool GetIsInteracting()
    {
        return isInteracting;
    }

    //animal has been clicked on, show UI
    public void ActivateUI()
    {
        FarmUIContainer.SetActive(true);
        marketButton.SetActive(false);
        //set name of animal in UI
        animalNameText.SetText(animalStats.name);
    }

    //  ////////////////////
    //back on UI hit
    public void BackCallDeactivateUI()
    {
        //set interacting to false
        SetIsInteracting(false);
        //reset camera position
        cameraScript.DefaultCameraPositionSet();
        //deactivate UI
        FarmUIContainer.SetActive(false);
        marketButton.SetActive(true);
        //
    }

    //events to be called through UI Buttons
    public void FeedCall()
    {
        //0 is hungry, 100 is full
        animalStats.ChangeAnimalFood(20);

        SetAnnounceText(animalName + " is fed, hunger is at: " + animalStats.GetAnimalHunger());
    }

    public void BrushCall()
    {
        animalStats.ChangeAnimalClean(20);

        SetAnnounceText(animalName + " is brushed, clean is at: " + animalStats.GetAnimalClean());
    }

    public void ShearCall()
    {
        //check if animal is sheerable
        if(animalStats.CheckIfAnimalSheerable())
        {
            SetAnnounceText(animalName + " is sheared. Got 1 fur");
            animalStats.ChangeAnimalFurGrowth(-100);

            inventory.AddToFurInventory(animalStats.GetAnimalFurInventoryIndex(), 1);
        }

        else
        {
            SetAnnounceText(animalName + " is not ready to be sheared, currently at " + animalStats.GetAnimalFurGrowth() + "% fur growth");
        }
    }

    // ////////////INPUT TEXT FIELD START
    //change animal name
    public void ChangeAnimalName(string newName)
    {
        //change in animal stats
        animalStats.ChangeAnimalName(newName);

        //change in UI reference
        animalName = newName;
    }

    public void ShowOrHideNameFieldInput()
    {
        if(!nameInputTextField.activeSelf)
        {
            animalNameText.SetText("");
            nameInputTextField.SetActive(true);
            editNameButtonText.text = "Save Name";
        }
        else
        {
            nameInputTextField.SetActive(false);
            animalNameText.SetText(animalName);
            editNameButtonText.text = "Edit Name";
        }
    }

    public void HideNameFieldInput()
    {
        nameInputTextField.SetActive(false);
        animalNameText.SetText(animalName);
    }

    // //////////////// INPUT TEXT FIELD END

    //function to call to set announcement text that appears on screen for two seconds, should reset timer if clicked while running
    private void SetAnnounceText(string textToDisplay)
    {
        if(announcingText)
        {
            Debug.Log("Already displaying");
            StopCoroutine(lastCoroutine);
            announcingText = false;
        }

        //see text display below
        lastCoroutine = StartCoroutine(AnnounceTextTimer(textToDisplay));
    }

    //timer yield return enumerator
    IEnumerator AnnounceTextTimer(string announceText)
    {
        announcingText = true;
        farmAnnounceText.SetText(announceText);

        yield return new WaitForSeconds(1f);

        farmAnnounceText.SetText("");

        announcingText = false;
    }

    //set reference to Animal to apply UI actions to
    public void GetReferenceOfAnimalBeingInteractedWith(GameObject animalGameObject)
    {
        Debug.Log("Setting to reference: " + animalGameObject);
        animalReference = animalGameObject;
        animalStats = animalReference.GetComponent<Animal>();
        animalSave = animalStats.GetAnimalSave();
        animalName = animalStats.GetAnimalName();
    }

    public void ResetReferenceOfAnimalBeingInteractedWith()
    {
        Debug.Log("Resetting animal reference");
        animalReference = null;
    }
}
