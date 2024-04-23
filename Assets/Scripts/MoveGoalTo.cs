using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveGoalTo : MonoBehaviour
{
    public Transform mActiveWindow;
    public List<Transform> mWindowList;
    public Material mGlowMaterial;
    public Material mDarkMaterial;
    public float mStartDelay = 0;
    public float mTimeToPickInterval = 5;
    public bool mUseRandomDelay;
    public Vector2 mDelayRange;

    private bool _isDelayed = false;
    private Transform _lastWindow;
    private void Start()
    {
        foreach (var t in mWindowList)
        {
            if(mDarkMaterial)
                t.GetComponent<Renderer>().material = mDarkMaterial;
        }
        InvokeRepeating(nameof(PickARandomWindow),mStartDelay,mTimeToPickInterval);
    }

    void PickARandomWindow()
    {
        if (_isDelayed)
            return;
        
        if (mUseRandomDelay)
        {
            _isDelayed = true;
            float rand = Random.Range(mDelayRange.x, mDelayRange.y);
            Invoke(nameof(ResetDelay), rand);
        }
        //Get a random window and turn it on
        int randomIndex = Random.Range(0, mWindowList.Count);
        mActiveWindow = mWindowList[randomIndex];
        if(mGlowMaterial)
            mActiveWindow.GetComponent<Renderer>().material = mGlowMaterial;
        
        //Add the last window selected (if any) back into the list
        if (_lastWindow is not null)
        {
            mWindowList.Add(_lastWindow);
            if(mDarkMaterial)
                _lastWindow.GetComponent<Renderer>().material = mDarkMaterial;
        }

        //Now set the selected random window as the last selected and remove it from the list
        _lastWindow = mActiveWindow;
        mWindowList.RemoveAt(randomIndex);
        this.gameObject.transform.position = mActiveWindow.position;

    }

    void ResetDelay()
    {
        _isDelayed = false;
    }
}
