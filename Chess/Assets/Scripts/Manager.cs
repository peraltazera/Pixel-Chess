using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    public Piece[] blackPieces;
    [SerializeField]
    public Piece[] whitePieces;
    [SerializeField]
    public Tile[,] tiles = new Tile[8, 8];
    public bool turnWhite = true;
    public List<Tile> enabledTiles;
    public Color tileEnebled;
    public Color tileOver;
    public Color tileEnemy;
    public Color white;
    public Color black;
    public Tile tileSelect;
    [SerializeField]
    public Camera cam;
    public AudioClip capture;
    public AudioClip check;
    public AudioClip gameOver;
    public AudioClip move;
    public AudioClip select;
    public AudioSource sound;
    public GameObject backGroundMenu;
    public GameObject whitePiecesGroup;
    public GameObject blackPiecesGroup;

    public void Clear()
    {
        for(int i = 0; i < enabledTiles.Count; i++){
            enabledTiles[i].center.color = tileOver;
            enabledTiles[i].center.enabled = false;
            enabledTiles[i].enabled = false;
        }
        enabledTiles.Clear();
    }
}
