using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public int SegId { set; get; }
    public bool transition;

    public int length;
    public int beginY1, beginY2, beginY3; // beg and end of segments
    public int endY1, endY2, endY3;

    private PieceSpawner[] pieces;

    private void Awake()
    {
        pieces = gameObject.GetComponentsInChildren<PieceSpawner>();

        //// remove before publish
        //for (int i = 0; i < pieces.Length; i++)
        //    foreach (MeshRenderer mr in pieces[i].GetComponentInChildren<MeshRenderer>()) ;
        //{
        //    mr.enabled = LevelManager.SHOW_COLLIDER;
        //}

    }


    public void Spawn()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].Spawn();
        }
    }

    public void DeSpawn()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].DeSpawn();
        }
    }
}
