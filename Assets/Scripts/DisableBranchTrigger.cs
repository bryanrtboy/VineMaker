using System;
using UnityEngine;
using UnityEngine.AI;

public class DisableBranchTrigger : MonoBehaviour
{
    
    private NavMeshObstacle _navMeshObstacle;
    private void Start()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    void OnTriggerEnter (Collider other)
    {
        _navMeshObstacle.carving = false;
    }

    // void OnTriggerStay (Collider other)
    // {
    //     Debug.Log ("A collider is inside the DoorObject trigger");
    // }
    //
    void OnTriggerExit (Collider other)
    {
        _navMeshObstacle.carving = true;
    }
}
