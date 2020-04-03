﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicInteractions : MonoBehaviour
{
    //This sets up basic interaction on the farm with the animal

    [Header("Snap point inside animal prefab")]
    public Transform InteractionCameraSnap;

    //camera in game
    private GameObject mainCamera;
    public CameraFarmMovement cameraSnapScript;     //keep this public for now

    //managers of scene that are local to this scene
    private GameObject managersLocalToScene;
    public FarmUIManager FarmUI;     //keep this public for now
    
    private Scene currentScene;
    private GameObject selectedAnimal; //gameobject reference of selected animal


    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        Debug.Log("Scene: " + currentScene.name);

        if (currentScene.name == "Farm_design")
        {
            //gets managers local to scene to work with
            managersLocalToScene = GameObject.Find("ManagersLocalToScene");
            FarmUI = managersLocalToScene.GetComponent<FarmUIManager>();

            //gets main camera in scene when instancing this object - gets script for camera snapping
            mainCamera = GameObject.Find("MainCamera");
            cameraSnapScript = mainCamera.GetComponent<CameraFarmMovement>();
        }
    }
    private void Update()
    {
        //calls UI Manager to see if player is interacting
        if(currentScene.name == "Farm_design")
        {
            CheckClickRaycast();

            if (FarmUI.GetIsInteracting())
            {
                MoveCamera(InteractionCameraSnap);
            }
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
                    selectedAnimal = hit.transform.gameObject;
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
        FarmUI.GetReferenceOfAnimalBeingInteractedWith(selectedAnimal);
    }

}
