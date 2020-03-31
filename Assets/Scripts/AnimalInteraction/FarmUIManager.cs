using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class FarmUIManager : MonoBehaviour
{
    [Header("Feedback text")]
    public TextMeshProUGUI farmAnnounceText;
    bool announcingText;

    [Header("UI References")]
    public GameObject FarmUIContainer;

    //camera script for resetting camera to view
    [Header("Camera reference")]
    public CameraFarmMovement cameraScript;

    [Header("Inventory Manager")]
    public InventoryManager inventory;

    //boolean for other functions to check
    private bool isInteracting;

    //reference for Animal being interacted with for working with and calling functions
    private GameObject animalReference;
    private AnimalStatistics animalStats;

    //variable for messing with coroutine logic
    private Coroutine lastCoroutine;

    private void Start()
    {
        isInteracting = false;
    }

    public void SetIsInteracting(bool newInteractingStatus)
    {
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
        //
    }

    //events to be called through UI Buttons
    public void FeedCall()
    {
        //0 is hungry, 100 is full
        animalStats.ChangeAnimalFood(20);

        SetAnnounceText("Jeremy is fed, hunger is at: " + animalStats.GetAnimalHunger());
 
    }

    public void BrushCall()
    {
        animalStats.ChangeAnimalClean(20);

        SetAnnounceText("Jeremy is brushed, clean is at: " + animalStats.GetAnimalClean());
    }

    public void ShearCall()
    {
        //check if animal is sheerable
        if(animalStats.CheckIfAnimalSheerable())
        {
            SetAnnounceText("Jeremy is sheared. Got 1 fur");
            animalStats.ChangeAnimalFurGrowth(-100);

            inventory.AddToFurInventory(animalStats.GetAnimalFurInventoryIndex(), 1);
        }

        else
        {
            SetAnnounceText("Jeremy is not ready to be sheared, currently at " + animalStats.GetAnimalFurGrowth() + "% fur growth");
        }
    }

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
        animalStats = animalReference.GetComponent<AnimalStatistics>();

    }

    public void ResetReferenceOfAnimalBeingInteractedWith()
    {
        Debug.Log("Resetting animal reference");
        animalReference = null;
    }
}
