using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ArrowPiece : MonoBehaviour
{
    [Header("Arrow Data")]
    public List<GridCell> occupiedCells = new List<GridCell>();

    public GridCell headCell;

    public Direction headDirection;

    [Header("State")]
    public bool isRemoved = false;

    public void RemoveArrow()
    {
        isRemoved = true;

        gameObject.SetActive(false);
    }

    public Vector3 GetWorldDirection()
    {
        switch (headDirection)
        {
            case Direction.Up:
                return Vector3.up;

            case Direction.Down:
                return Vector3.down;

            case Direction.Left:
                return Vector3.left;

            case Direction.Right:
                return Vector3.right;

            default:
                return Vector3.zero;
        }
    }
}