// MoveTo.cs

using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MoveTo : MonoBehaviour {

    public Transform mGoal;
    public bool mUseRandomValues = false;
    public Vector2 mRandomSpeed = new Vector2(5, 10);
    public Vector2 mRandomAcceleration = new Vector2(30, 120);
    public Vector2 mRandomStoppingDistance = new Vector2(0, 1);
    public float mDelayedStartNavigation = 0;
    private NavMeshAgent _agent;

 
    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (mDelayedStartNavigation > 0)
            _agent.enabled = false;
        if(mUseRandomValues)
            SetRandomValues();
        InvokeRepeating("UpdateGoal", mDelayedStartNavigation, Random.Range(.7f,1.2f));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void UpdateGoal()
    {
        _agent.enabled = true;
        if (!mGoal)
        {
            GameObject[] goals = GameObject.FindGameObjectsWithTag("Player");
            int rand = Random.Range(0, goals.Length);
            
            mGoal = goals[rand].transform;
        }
        else
        {
            _agent.destination = mGoal.position;
        }
    }

    void SetRandomValues()
    {
        _agent.speed = Random.Range(mRandomSpeed.x, mRandomSpeed.y);
        _agent.acceleration = Random.Range(mRandomAcceleration.x, mRandomAcceleration.y);
        _agent.stoppingDistance = Random.Range(mRandomStoppingDistance.x, mRandomStoppingDistance.y);
    }
}