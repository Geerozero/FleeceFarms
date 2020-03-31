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

    //create function to receive reference to particular animal on farm scene that we're acting on
    //like Public Void SetAnimalReference
    private GameObject animalReference;


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

    //back on UI hit
    public void BackCallDeactivateUI()
    {
        SetIsInteracting(false);
        cameraScript.DefaultCameraPositionSet();
        FarmUIContainer.SetActive(false);
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

    public void ShearCall()
    {
        if (!announcingText)
        {
            //see text display below
            StartCoroutine(SetAnnounceText("Jeremy is sheared. Got X amount of fleece"));
        }
    }

    IEnumerator SetAnnounceText(string announceText)
    {
        announcingText = true;
        farmAnnounceText.SetText(announceText);

        yield return new WaitForSeconds(1f);

        farmAnnounceText.SetText("");

        announcingText = false;

    }
}
