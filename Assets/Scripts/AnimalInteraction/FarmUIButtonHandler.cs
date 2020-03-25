using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class FarmUIButtonHandler : MonoBehaviour
{
    [Header("Feedback text")]
    public TextMeshProUGUI farmAnnounceText;
    bool announcingText;

    [Header("UI References")]
    public GameObject FarmUIContainer;

    //camera script for resetting camera to view
    [Header("Camera reference")]
    public CameraFarmMovement cameraScript;

    //boolean for other functions to check
    private bool isInteracting;

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
        if (!announcingText)
        {
            //see text display below
            StartCoroutine(SetAnnounceText("Jeremy is fed"));
        }
    }

    public void BrushCall()
    {
        if (!announcingText)
        {
            //see text display below
            StartCoroutine(SetAnnounceText("Jeremy is brushed"));
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
