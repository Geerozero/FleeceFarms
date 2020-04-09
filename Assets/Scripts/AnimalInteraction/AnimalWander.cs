using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalWander : MonoBehaviour
{
    private Vector3 nextPositionToMoveTo;

    private NavMeshAgent navAgent;

    private bool isMoving;

    [Header("Time to let animal wait in place")]
    public float timeWaiting;
    private float lastTimeMoved;

    // Start is called before the first frame update
    void Start() //-added
    {
        //get navmesh agent of this object
        navAgent = this.GetComponent<NavMeshAgent>();

        //initialize some variables
        isMoving = false;
        lastTimeMoved = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // /////////MOVEMENT
        //if currently moving, just check remaining distance
        if (isMoving)
        {
            //change bool for isMoving just to check loop
            if(navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                lastTimeMoved = Time.time;
                isMoving = false;
            }
        }
        else    //if done moving, check if enough time passed to move to next random position
        {
            //let animal wait before moving it
            if (Time.time - lastTimeMoved >= timeWaiting)
            {
                //set moving boolean true
                isMoving = true;

                //get random position inside a sphere
                nextPositionToMoveTo = transform.position + (Random.insideUnitSphere * 5);

                //move to location
                navAgent.SetDestination(nextPositionToMoveTo);
                
            }
        }

    }
}
