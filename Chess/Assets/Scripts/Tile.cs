using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    int y;
    [SerializeField]
    int x;
    [SerializeField]
    public SpriteRenderer center;
    [SerializeField]
    public SpriteRenderer left;
    [SerializeField]
    public SpriteRenderer right;
    [SerializeField]
    public SpriteRenderer top;
    [SerializeField]
    public SpriteRenderer bottom;
    public SpriteRenderer pieceSprite;
    public Transform piecePosition;
    public Piece piece;
    [SerializeField]
    Manager manager;
    public bool white;
    public bool enabled = false;
    public bool anim = false;

    void Awake () {
        manager.tiles[y,x] = this;
        center.sortingOrder = -1;
        left.sortingOrder = 0;
        right.sortingOrder = 0;
        top.sortingOrder = 0;
        bottom.sortingOrder = 0;
        center.color = manager.tileOver;
        center.enabled = false;
        left.enabled = false;
        right.enabled = false;
        top.enabled = false;
        bottom.enabled = false;
    }

    void OnMouseOver()
    {
        if(pieceSprite != null){
            if(manager.turnWhite && white || !manager.turnWhite && !white){
                if(!anim){
                    manager.sound.clip = manager.select;
                    manager.sound.volume = 0.5f;
                    manager.sound.Play();
                    center.enabled = true;
                    piecePosition.position = new Vector2(manager.tiles[piece.y,piece.x].gameObject.transform.position.x, manager.tiles[piece.y,piece.x].gameObject.transform.position.y + 0.73f);
                    piecePosition.position = new Vector2(piecePosition.position.x, piecePosition.position.y + 0.2f);
                    anim = true;
                }
                if (Input.GetMouseButtonDown(0)){
                    manager.sound.clip = manager.check;
                    manager.sound.volume = 0.8f;
                    manager.sound.Play();
                    manager.tileSelect = this;
                    manager.Clear();
                    switch(piece.type)
                    {
                        case Piece.typePiece.Pawn:
                        {
                            piece.Pawn();
                            break;
                        }
                        case Piece.typePiece.Rook:
                        {
                            piece.Rook();
                            break;
                        }
                        case Piece.typePiece.Knight:
                        {
                            piece.Knight();
                            break;
                        }
                        case Piece.typePiece.Bishop:
                        {
                            piece.Bishop();
                            break;
                        }
                        case Piece.typePiece.Queen:
                        {
                            piece.Queen();
                            break;
                        }
                        case Piece.typePiece.King:
                        {
                            piece.King();
                            break;
                        }
                        default: break;
                    }
                }
            }else if(enabled){
                if (Input.GetMouseButtonDown(0)){
                    manager.sound.clip = manager.capture;
                    manager.sound.volume = 1f;
                    manager.sound.Play();
                    piece.gameObject.SetActive(false);
                    white = manager.tileSelect.white;
                    pieceSprite = manager.tileSelect.pieceSprite;
                    piecePosition = manager.tileSelect.piecePosition;
                    piece = manager.tileSelect.piece;
                    manager.tileSelect.piecePosition = null;
                    manager.tileSelect.piece = null;
                    manager.tileSelect.pieceSprite = null;
                    manager.tileSelect.white = false;
                    piece.move = true;
                    piece.x = x;
                    piece.y = y;
                    pieceSprite.sortingOrder = 8-y;
                    piecePosition.position = new Vector2(manager.tiles[piece.y,piece.x].gameObject.transform.position.x, manager.tiles[piece.y,piece.x].gameObject.transform.position.y + 0.73f);
                    manager.turnWhite = !manager.turnWhite;
                    if(manager.turnWhite){
                        manager.cam.backgroundColor = manager.white;
                    }else{
                        manager.cam.backgroundColor = manager.black;
                    }
                    manager.Clear();
                }
            }else{
                if (Input.GetMouseButtonDown(0)){
                    manager.Clear();
                }
            }
        }else if(enabled){
            if (Input.GetMouseButtonDown(0)){
                manager.sound.clip = manager.move;
                manager.sound.volume = 1f;
                manager.sound.Play();
                white = manager.tileSelect.white;
                pieceSprite = manager.tileSelect.pieceSprite;
                piecePosition = manager.tileSelect.piecePosition;
                piece = manager.tileSelect.piece;
                manager.tileSelect.piecePosition = null;
                manager.tileSelect.piece = null;
                manager.tileSelect.pieceSprite = null;
                manager.tileSelect.white = false;
                piece.move = true;
                piece.x = x;
                piece.y = y;
                pieceSprite.sortingOrder = 8-y;
                piecePosition.position = new Vector2(manager.tiles[piece.y,piece.x].gameObject.transform.position.x, manager.tiles[piece.y,piece.x].gameObject.transform.position.y + 0.73f);
                manager.turnWhite = !manager.turnWhite;
                if(manager.turnWhite){
                    manager.cam.backgroundColor = manager.white;
                }else{
                    manager.cam.backgroundColor = manager.black;
                }
                manager.Clear();
            }
        }else{
            if (Input.GetMouseButtonDown(0)){
                manager.Clear();
            }
        }
    }

    void OnMouseExit()
    {
        if(pieceSprite != null){
            if(manager.turnWhite && white || !manager.turnWhite && !white && anim){
               center.enabled = false;
               piecePosition.position = new Vector2(manager.tiles[piece.y,piece.x].gameObject.transform.position.x, manager.tiles[piece.y,piece.x].gameObject.transform.position.y + 0.73f);
               anim = false;
            }
        }
    }
}
