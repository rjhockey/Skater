using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public PieceType type;
    private Piece currentPiece;

    public void Spawn()
    {
        currentPiece = LevelManager.Instance.GetPiece(type, 0); //Get new piece from the pool
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void DeSpawn()
    {
        //go back to the pool
        currentPiece.gameObject.SetActive(false);
    }
}
