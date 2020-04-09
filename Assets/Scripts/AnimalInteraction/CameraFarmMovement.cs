using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFarmMovement : MonoBehaviour
{

    [Header("Default camera snapping")]
    public Transform DefaultCameraSnap;
    public GameObject FarmUI;
    private FarmUIManager farmUIManager;

    // Start is called before the first frame update
    void Start()
    {
        DefaultCameraPositionSet();
        farmUIManager = FarmUI.GetComponent<FarmUIManager>();
    }

    public void DefaultCameraPositionSet()
    {
        //set camera to default spawn position
        transform.position = DefaultCameraSnap.position;
        transform.rotation = DefaultCameraSnap.rotation;
    }

    public void NewCameraPositionSet(Transform inputTransform)
    {
        //change only position of camera, not rotation
    }
}
