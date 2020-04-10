using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicInteractions : MonoBehaviour
{
    //This sets up basic interaction on the farm with the animal

    [Header("Snap point inside animal prefab")]
    //public Transform InteractionCameraSnap;

    //camera in game
    public GameObject mainCamera;
    public CameraFarmMovement cameraSnapScript;     //keep this public for now

    //managers of scene that are local to this scene
    private GameObject managersLocalToScene;
    public FarmUIManager FarmUI;     //keep this public for now
    
    private Scene currentScene;
    private GameObject selectedAnimal; //gameobject reference of selected animal

    //camera positioning
    private float xdist;
    private float zdist;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Farm_design")
        {
            //gets managers local to scene to work with
            managersLocalToScene = GameObject.Find("ManagersLocalToScene");
            FarmUI = managersLocalToScene.GetComponent<FarmUIManager>();

            //gets main camera in scene when instancing this object - gets script for camera snapping
            //mainCamera = GameObject.Find("MainCamera");
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
                MoveCamera();
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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //print what was hit by raycast
                Debug.Log("Click hit:" + hit.transform.name);

                //clicked on an animal
                if (hit.transform.tag == "Animal")
                {
                    Debug.Log("Clicked Animal");
                    selectedAnimal = hit.transform.gameObject;
                    xdist = selectedAnimal.transform.position.x - selectedAnimal.transform.GetChild(0).transform.position.x;
                    zdist = selectedAnimal.transform.position.z - selectedAnimal.transform.GetChild(0).transform.position.z;
                    
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
    private void MoveCamera()
    {
        //moves camera to appropriate animal
        //cameras positioning needs to be constantly updated so it can follow the animal and not rotate with the animal
        //keeping camera movement here on player
        mainCamera.transform.position = new Vector3(selectedAnimal.transform.position.x - xdist, selectedAnimal.transform.GetChild(0).transform.position.y, selectedAnimal.transform.position.z - zdist);
        mainCamera.transform.LookAt(selectedAnimal.transform);
    }

    //pass reference to this gameObject to UI
    private void PassReferenceToUIManager()
    {
        //gets parent of this object to pass up
        FarmUI.GetReferenceOfAnimalBeingInteractedWith(selectedAnimal);
    }
}
