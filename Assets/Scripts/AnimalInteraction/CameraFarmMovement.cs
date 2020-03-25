using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFarmMovement : MonoBehaviour
{

    [Header("Default camera snapping")]
    public Transform DefaultCameraSnap;

    // Start is called before the first frame update
    void Start()
    {
        DefaultCameraPositionSet();
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

        transform.position = inputTransform.position;
    }
}
