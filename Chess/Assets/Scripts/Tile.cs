using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer center;
    public SpriteRenderer left;
    public SpriteRenderer right;
    public SpriteRenderer top;
    public SpriteRenderer bottom;
    [SerializeField]
    private Manager manager;
    public SpriteRenderer pieceSprite;
    public Transform piecePosition;
    public Piece piece;
    [Header("Config")]
    [SerializeField]
    private int y;
    [SerializeField]
    private int x;
    [Header("Stats")]
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
                    manager.SetEfects(0.5f, manager.select);
                    center.enabled = true;
                    piecePosition.position = new Vector2(manager.tiles[piece.y,piece.x].gameObject.transform.position.x, manager.tiles[piece.y,piece.x].gameObject.transform.position.y + 0.73f);
                    piecePosition.position = new Vector2(piecePosition.position.x, piecePosition.position.y + 0.2f);
                    anim = true;
                }
                if (Input.GetMouseButtonDown(0)){
                    manager.SetEfects(0.8f, manager.check);
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
                            if(manager.theRook){
                                if(white){
                                    if(piece.x > manager.whitePieces[12].x){
                                        MovePiece(manager.tiles[piece.y,manager.whitePieces[12].x+1], manager.tiles[piece.y,piece.x]);
                                        MovePiece(manager.tiles[manager.whitePieces[12].y,manager.whitePieces[12].x+2], manager.tiles[manager.whitePieces[12].y,manager.whitePieces[12].x]);
                                        manager.ChangeTurn();
                                    }else{
                                        MovePiece(manager.tiles[piece.y,manager.whitePieces[12].x-1], manager.tiles[piece.y,piece.x]);
                                        MovePiece(manager.tiles[manager.whitePieces[12].y,manager.whitePieces[12].x-2], manager.tiles[manager.whitePieces[12].y,manager.whitePieces[12].x]);
                                        manager.ChangeTurn();
                                    }
                                }else{
                                    if(piece.x > manager.blackPieces[12].x){
                                        MovePiece(manager.tiles[piece.y,manager.blackPieces[12].x+1], manager.tiles[piece.y,piece.x]);
                                        MovePiece(manager.tiles[manager.blackPieces[12].y,manager.blackPieces[12].x+2], manager.tiles[manager.blackPieces[12].y,manager.blackPieces[12].x]);
                                        manager.ChangeTurn();
                                    }else{
                                        MovePiece(manager.tiles[piece.y,manager.blackPieces[12].x-1], manager.tiles[piece.y,piece.x]);
                                        MovePiece(manager.tiles[manager.blackPieces[12].y,manager.blackPieces[12].x-2], manager.tiles[manager.blackPieces[12].y,manager.blackPieces[12].x]);
                                        manager.ChangeTurn();
                                    }
                                }
                            }else{
                                piece.Rook();
                            }
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
                    manager.SetEfects(1f, manager.capture);
                    piece.gameObject.SetActive(false);
                    if(piece.type == Piece.typePiece.King){
                        if(piece.white){
                            Debug.Log("Black Wins");
                        }else{
                            Debug.Log("White Wins");
                        }
                        manager.Clear();
                        manager.theRook = false;
                        manager.PieceInitial();
                    }else{
                        MovePiece(this, manager.tileSelect);
                        manager.ChangeTurn();
                    }
                }
            }else{
                if (Input.GetMouseButtonDown(0)){
                    manager.Clear();
                    manager.theRook = false;
                }
            }
        }else if(enabled){
            if (Input.GetMouseButtonDown(0)){
                manager.SetEfects(1f, manager.move);
                MovePiece(this, manager.tileSelect);
                manager.ChangeTurn();
            }
        }else{
            if (Input.GetMouseButtonDown(0)){
                manager.Clear();
                manager.theRook = false;
            }
        }
    }

    void MovePiece(Tile pos, Tile pre){
        pos.white = pre.white;
        pos.pieceSprite = pre.pieceSprite;
        pos.piecePosition = pre.piecePosition;
        pos.piece = pre.piece;
        pre.piecePosition = null;
        pre.piece = null;
        pre.pieceSprite = null;
        pre.white = false;
        pos.piece.move = true;
        pos.piece.x = pos.x;
        pos.piece.y = pos.y;
        pos.pieceSprite.sortingOrder = 8-y;
        pos.piecePosition.position = new Vector2(manager.tiles[pos.piece.y,pos.piece.x].gameObject.transform.position.x, manager.tiles[pos.piece.y,pos.piece.x].gameObject.transform.position.y + 0.73f);
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
