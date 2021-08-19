using UnityEngine;

//SCRIPT attached to prefab pieces & used by PieceSpawner.cs
public enum PieceType
{
    none = -1,
    ramp = 0,
    longblock = 1,
    jump = 2,
    slide = 3,
}

public class Piece : MonoBehaviour
{
    public PieceType type;
    public int visualIndex;
}
