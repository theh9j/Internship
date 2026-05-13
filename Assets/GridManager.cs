using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Dictionary<GridCell, ArrowPiece> occupiedCells = new Dictionary<GridCell, ArrowPiece>();

    private List<ArrowPiece> arrows = new List<ArrowPiece>();

    public void RegisterArrow(ArrowPiece arrow)
    {
        arrows.Add(arrow);

        foreach (GridCell cell in arrow.occupiedCells)
        {

        }
    }

}
