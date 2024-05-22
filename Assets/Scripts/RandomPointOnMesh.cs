using UnityEngine;
using UnityEngine.AI;

public class RandomPointOnMesh : MonoBehaviour
{
    public MeshCollider meshCollider; // Assign this in the inspector
    public Transform agentGoal;
    public Vector2 repeatRange = new Vector2(3f, 6f);

    void Start()
    {
        float repeatRate = Random.Range(repeatRange.x,repeatRange.y);
        InvokeRepeating(nameof(PickARandomSpot),0,repeatRate);
    }

    void PickARandomSpot()
    {
        Vector3 randomPointOnMesh = GetRandomPointOnMeshCollider(meshCollider);
        Vector3 randomPointInBounds = GetRandomPointInBounds(meshCollider.bounds);

        if (FindNearestPointOnNavMesh(randomPointInBounds, out var nearestPointOnNavMesh))
        {
            agentGoal.position = nearestPointOnNavMesh;
        }
        else
        {
            Debug.LogError("No valid NavMesh point found near the random mesh point.");
        }
    }

    Vector3 GetRandomPointOnMeshCollider(MeshCollider collider)
    {
        Mesh mesh = collider.sharedMesh;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;

        // Choose a random triangle in the mesh
        int index = Random.Range(0, triangles.Length / 3) * 3;

        // Get the vertices of the triangle
        Vector3 v1 = collider.transform.TransformPoint(vertices[triangles[index]]);
        Vector3 v2 = collider.transform.TransformPoint(vertices[triangles[index + 1]]);
        Vector3 v3 = collider.transform.TransformPoint(vertices[triangles[index + 2]]);

        // Generate a random point within the triangle
        float a = Random.value;
        float b = Random.value;
        if (a + b > 1)
        {
            a = 1 - a;
            b = 1 - b;
        }
        float c = 1 - a - b;

        return a * v1 + b * v2 + c * v3;
    }
    
    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    bool FindNearestPointOnNavMesh(Vector3 point, out Vector3 result, float maxDistance = 5.0f)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(point, out hit, maxDistance, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
}