using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteractions : MonoBehaviour
{
    //This sets up basic interaction on the farm with the animal

    /*
     * Click on Animal - > Dynamic view near them - > Click Button to Feed/Brush/Sheer
     */

    [Header("Camera snapping for interaction references")]
    public Transform InteractionCameraSnap;
    public CameraFarmMovement cameraSnapScript;

    [Header("Farm UI handler")]
    public FarmUIButtonHandler FarmUI;

    //TO IMPLEMENT
    //telling UI which animal on the scene to apply Feed/Brush/Sheer logic
    //AKA pass reference of this object to FarmUI when GetMouseButtonDown is active

    //


    private void Start()
    {

    }
    private void Update()
    {
        //looks for left click from mouse -- change for tap for mobile for extension
        if(Input.GetMouseButtonDown(0))
        {
            //raycast to where mouse was clicked on screen
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //general case of something being clicked
            if(Physics.Raycast(ray, out hit, 100.0f))
            {
                //print what was hit by raycast
                Debug.Log("Click hit:" + hit.transform.name);

                //clicked on an animal
                if(hit.transform.tag == "Animal")
                {
                    Debug.Log("Clicked Animal");
                    //set isInteracting true
                    SetIsInteractingInUIHandler(true);
                    FarmUI.ActivateUI();
                }
            }
        }

        //calls UI Manager to see if player is interacting
        if(FarmUI.GetIsInteracting())
        {
            MoveCamera(InteractionCameraSnap);
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

    


}
