using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Components")]
    public Camera cam;
    public Background back;
    public Tile[,] tiles = new Tile[8, 8];
    [Header("Pieces")]
    public Piece[] whitePieces;
    public Piece[] blackPieces;
    [Header("UI")]
    public GameObject menu;
    public GameObject pause;
    public GameObject musicX;
    public GameObject efectsX;
    public GameObject board;
    public GameObject whitePiecesGroup;
    public GameObject blackPiecesGroup;
    [Header("Audio")]
    public AudioSource efects;
    public AudioSource music;
    public AudioClip start;
    public AudioClip capture;
    public AudioClip check;
    public AudioClip gameOver;
    public AudioClip move;
    public AudioClip select;
    [Header("Colors")]
    public Color tileEnebled;
    public Color tileOver;
    public Color tileEnemy;
    public Color white;
    public Color black;
    [Header("Stats")]
    public Tile tileSelect;
    public List<Tile> enabledTiles;
    public bool turnWhite = true;
    public bool theRook = false;

    public void Clear()
    {
        for(int i = 0; i < enabledTiles.Count; i++){
            enabledTiles[i].center.color = tileOver;
            enabledTiles[i].center.enabled = false;
            enabledTiles[i].enabled = false;
        }
        enabledTiles.Clear();
    }

    public void SetEfects(float volume, AudioClip audio){
        efects.clip = audio;
        efects.volume = volume;
        efects.Play();
    }

    public void ChangeBack(){
        if(turnWhite){
            cam.backgroundColor = white;
            back.target = back.white;
            back.enabled = true;
        }else{
            cam.backgroundColor = black;
            back.target = back.black;
            back.enabled = true;
        }
    }

    public void ChangeTurn(){
        turnWhite = !turnWhite;
        ChangeBack();
        Clear();
        theRook = false;
    }

    public void PieceInitial()
    {
        for(int i = 0; i < whitePieces.Length; i++){
            ClearBoard(blackPieces[i]);
            ClearBoard(whitePieces[i]);
        }
        for(int i = 0; i < whitePieces.Length; i++){
            Organize(blackPieces[i]);
            Organize(whitePieces[i]);
        }
        turnWhite = true;
        cam.backgroundColor = white;
        ChangeBack();
    }

    void ClearBoard(Piece piece){
        int x = piece.x;
        int y = piece.y;
        tiles[y,x].piecePosition = null;
        tiles[y,x].piece = null;
        tiles[y,x].pieceSprite = null;
        tiles[y,x].white = false;
        tiles[y,x].enabled = false;
        tiles[y,x].anim = false;
    }

    void Organize(Piece piece)
    {
        piece.obj.SetActive(true);
        piece.x = piece.xInitial;
        piece.y = piece.yInitial;
        int x = piece.x;
        int y = piece.y;
        piece.obj.transform.position = new Vector2(tiles[y,x].gameObject.transform.position.x, tiles[y,x].gameObject.transform.position.y + 0.73f);
        tiles[y,x].piecePosition = piece.obj.transform;
        tiles[y,x].piece = piece;
        tiles[y,x].pieceSprite = piece.sprite;
        tiles[y,x].white = piece.white;
        piece.move = false;
        piece.sprite.sortingOrder = 8-y;
    }
}
