using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [Header("Components")]
    public GameObject obj;
    public SpriteRenderer sprite;
    [SerializeField]
    private Manager manager;
    [Header("Config")]
    public int xInitial;
    public int yInitial;
    public enum typePiece
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }
    public typePiece type;
    public bool white;
    [Header("Stats")]
    public int x;
    public int y;
    public bool move = false;
    private bool diagonal = false;

    void Start () {
        manager.tiles[y,x].pieceSprite = sprite;
        manager.tiles[y,x].piecePosition = obj.transform;
        manager.tiles[y,x].piece = this;
        manager.tiles[y,x].white = white;
        obj.transform.position = new Vector2(manager.tiles[y,x].gameObject.transform.position.x, manager.tiles[y,x].gameObject.transform.position.y + manager.difY);
    }

    public void Pawn () {
        if(white){
            if(!move){
                for(int i = 1; i < 3; i++){
                    bool loop = EnableTile(y+i,x);
                    if(!loop)
                        break;
                }
            }else{
                EnableTile(y+1,x);
            }
            TilePawn(y+1,x+1);
            TilePawn(y+1,x-1);
        }else{
            if(!move){
                for(int i = 1; i < 3; i++){
                    bool loop = EnableTile(y-i,x);
                    if(!loop)
                        break;
                }
            }else{
                EnableTile(y-1,x);
            }
            TilePawn(y-1,x+1);
            TilePawn(y-1,x-1);
        }
    }

    public void Rook () {
        LoopTile(true,false,false,false);
        LoopTile(false,true,false,false);
        LoopTile(false,false,true,false);
        LoopTile(false,false,false,true);
    }

    public void Knight () {
        EnableTile(y+2,x+1);
        EnableTile(y+2,x-1);
        EnableTile(y-2,x+1);
        EnableTile(y-2,x-1);
        EnableTile(y+1,x+2);
        EnableTile(y+1,x-2);
        EnableTile(y-1,x+2);
        EnableTile(y-1,x-2);
    }

    public void Bishop () {
        LoopTile(true,false,true,false);
        LoopTile(false,true,false,true);
        LoopTile(false,true,true,false);
        LoopTile(true,false,false,true);
    }

    public void Queen () {
        Bishop();
        Rook();
    }

    public void King () {
        EnableTile(y+1,x);
        EnableTile(y-1,x);
        EnableTile(y,x+1);
        EnableTile(y,x-1);
        EnableTile(y+1,x+1);
        EnableTile(y-1,x-1);
        EnableTile(y+1,x-1);
        EnableTile(y-1,x+1);
        if(!move){
            if(manager.tiles[y,x+1].pieceSprite == null && manager.tiles[y,x+2].pieceSprite == null && manager.tiles[y,x+3].piece != null){
                if(manager.tiles[y,x+3].piece.type == Piece.typePiece.Rook && !manager.tiles[y,x+3].piece.move){
                    Tile(y,x+3);
                    manager.theRook = true;
                }
            }
            if(manager.tiles[y,x-1].pieceSprite == null && manager.tiles[y,x-2].pieceSprite == null && manager.tiles[y,x-3].pieceSprite == null && manager.tiles[y,x-4].piece != null){
                if(manager.tiles[y,x-4].piece.type == Piece.typePiece.Rook && !manager.tiles[y,x+3].piece.move){
                    Tile(y,x-4);
                    manager.theRook = true;
                } 
            }
        }
    }

    void LoopTile(bool yPos, bool yNeg, bool xPos, bool xNeg){
        int yLocal = 0;
        int xLocal = 0;
        for(int i = 1; i < 8; i++){
            if(xPos){
                xLocal = x + i;
            }
            else if(xNeg){
                xLocal = x - i;
            }else{
                xLocal = x;
            }
            if(yPos){
                yLocal = y + i;
            }
            else if(yNeg){
                yLocal = y - i;
            }else{
                yLocal = y;
            }
            bool loop = EnableTile(yLocal,xLocal);
            if(!loop)
                break;
        }
    }

    void Tile(int y, int x){
        manager.tiles[y,x].center.enabled = true;
        manager.tiles[y,x].center.color = manager.tileEnebled;
        manager.tiles[y,x].enabled = true;
        manager.enabledTiles.Add(manager.tiles[y,x]);
    }

    void TilePawn(int y, int x){
        if(y >= 0 && y <=7 && x >= 0 && x <=7){
            if(manager.tiles[y,x].pieceSprite != null && manager.turnWhite != manager.tiles[y,x].white){
                diagonal = true;
                EnableTile(y,x);
            }
         }
    }

    bool EnableTile(int y, int x){
        if(y >= 0 && y <=7 && x >= 0 && x <=7){
            if(manager.tiles[y,x].pieceSprite == null || manager.tiles[y,x].pieceSprite != null && manager.turnWhite != manager.tiles[y,x].white && type != typePiece.Pawn || diagonal){
                diagonal = false;
                Tile(y,x);
                if(manager.tiles[y,x].pieceSprite != null){
                    manager.tiles[y,x].center.color = manager.tileEnemy;
                    return false;
                }else{
                    return true;
                }
            }else{
                return false;
            }
        }else{
            return false;
        }
    }
}
