using UnityEngine;

public class FaceGridData : MonoBehaviour
{
    public CubeFace face;
    public Transform cubeTransform;
    public int gridSize = 5;

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        float cubeSize = cubeTransform.localScale.x;
        float cellSize = cubeSize / gridSize;
        float offset = (gridSize - 1) / 2f;

        float localX = (gridPosition.x - offset) * cellSize;
        float localZ = (gridPosition.y - offset) * cellSize;

        Vector3 localPosition = new Vector3(localX, 0.08f, localZ);

        return transform.TransformPoint(localPosition);
    }

    public Quaternion GetWorldRotation()
    {
        return transform.rotation;
    }
}