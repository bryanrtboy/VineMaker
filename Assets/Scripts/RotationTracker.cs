using UnityEngine;

public class RotationTracker : MonoBehaviour
{
    public float rotationThreshold = 10f; // Set your desired rotation change threshold
    public GameObject mBranchAgent;
    public float mStartDelay = 2;
    private float initialRotationY;
    public bool hasDirectionChanged = false;
    private float currentTime;
    private void Start()
    {
        // Store the initial Y-axis rotation
        initialRotationY = transform.eulerAngles.y;
        currentTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - currentTime < mStartDelay)
            return;
        
        // Get the current Y-axis rotation
        float currentRotationY = transform.eulerAngles.y;

        // Calculate the rotation change
        float rotationChange = Mathf.Abs(currentRotationY - initialRotationY);

        // Check if the rotation change exceeds the threshold
        if (rotationChange >= rotationThreshold)
        {
            hasDirectionChanged = true;
            Instantiate(mBranchAgent, transform.position, transform.rotation);
        }
        else
        {
            hasDirectionChanged = false;
        }

        // Update the initial rotation for the next frame
        initialRotationY = currentRotationY;
    }
}