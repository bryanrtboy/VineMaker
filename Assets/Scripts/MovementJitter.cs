using UnityEngine;

public class MovementJitter : MonoBehaviour
{
    public float moveDistance = 3f; // Distance to move back and forth
    public float moveSpeed = 1f; // Speed of movement

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingForward = true;

    private void Start()
    {
        // Initialize start and end positions
        startPos = transform.localPosition;
        endPos = startPos + Vector3.right * moveDistance;
    }

    private void Update()
    {
        // Move the object back and forth
        float step = moveSpeed * Time.deltaTime;
        if (movingForward)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, step);
            if (transform.localPosition == endPos)
                movingForward = false;
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos, step);
            if (transform.localPosition == startPos)
                movingForward = true;
        }
    }
}
