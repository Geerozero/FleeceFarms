using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteractions : MonoBehaviour
{
    //This sets up basic interaction on the farm with the animal

    [Header("Camera snapping for interaction references")]
    public Transform InteractionCameraSnap;
    public CameraFarmMovement cameraSnapScript;

    [Header("Farm UI handler")]
    public FarmUIButtonHandler FarmUI;


    private void Start()
    {

    }
    private void Update()
    {

        CheckClickRaycast();

        //calls UI Manager to see if player is interacting
        if(FarmUI.GetIsInteracting())
        {
            MoveCamera(InteractionCameraSnap);
        }

    }

    //checks for raycast of player click if it hit this animal
    private void CheckClickRaycast()
    {
        //looks for left click from mouse -- change for tap for mobile for extension
        if (Input.GetMouseButtonDown(0))
        {
            //raycast to where mouse was clicked on screen
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //general case of something being clicked
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //print what was hit by raycast
                Debug.Log("Click hit:" + hit.transform.name);

                //clicked on an animal
                if (hit.transform.tag == "Animal")
                {
                    Debug.Log("Clicked Animal");
                    //set isInteracting true
                    SetIsInteractingInUIHandler(true);
                    //pass reference to UI
                    PassReferenceToUIManager();
                    //show UI
                    FarmUI.ActivateUI();
                }
            }
        }
    }

    //set status of IsInteracting in UI Handler
    private void SetIsInteractingInUIHandler(bool newInteractingStatus)
    {
        FarmUI.SetIsInteracting(newInteractingStatus);
    }

    //move camera to given position
    private void MoveCamera(Transform newCameraPosition)
    {
        //ping camera script
        cameraSnapScript.NewCameraPositionSet(newCameraPosition);
        
    }

    //pass reference to this gameObject to UI
    private void PassReferenceToUIManager()
    {
        //gets parent of this object to pass up
        FarmUI.GetReferenceOfAnimalBeingInteractedWith(transform.parent.gameObject);
    }

}
