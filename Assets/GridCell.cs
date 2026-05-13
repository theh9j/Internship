using UnityEngine;

[System.Serializable]
public struct GridCell
{
    public CubeFace face;
    public Vector2Int position;

    public GridCell(CubeFace face, Vector2Int position)
    {
        this.face = face;
        this.position = position;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is GridCell))
            return false;

        GridCell other = (GridCell)obj;

        return face == other.face && position == other.position;
    }

    public override int GetHashCode()
    {
        return ((int)face * 397) ^
               position.GetHashCode();
    }

    public override string ToString()
    {
        return $"{face} {position}";
    }
}