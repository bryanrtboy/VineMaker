using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DisableNearestObstacle : MonoBehaviour
{
    public float mDistance = 2;
    private NavMeshObstacle[] _obstacles;

    void Start()
    {
        _obstacles =  FindObjectsByType<NavMeshObstacle>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        foreach (var other in _obstacles)
        {
            Vector3 offset = other.transform.position - transform.position;
            float sqrLen = offset.sqrMagnitude;
    
            // square the distance we compare with
            if (sqrLen < mDistance * mDistance)
            {
                other.gameObject.SetActive(false);
            }
            else
            {
                other.gameObject.SetActive(true);
            }
        }
        
    }
}
