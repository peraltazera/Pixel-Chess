using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    public int x;
    [SerializeField]
    public int y;
    [SerializeField]
    public int xInitial;
    [SerializeField]
    public int yInitial;
    [SerializeField]
    Manager manager;
    [SerializeField]
    bool white;
    bool diagonal = false;
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
    public bool move = false;

    void Start () {
        manager.tiles[y,x].pieceSprite = sprite;
        manager.tiles[y,x].piecePosition = obj.transform;
        manager.tiles[y,x].piece = this;
        manager.tiles[y,x].white = white;
        obj.transform.position = new Vector2(manager.tiles[y,x].gameObject.transform.position.x, manager.tiles[y,x].gameObject.transform.position.y + 0.73f);
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
        for(int i = 1; i < 8; i++){
            bool loop = EnableTile(y+i,x);
            if(!loop)
                break;
        }
        for(int i = 1; i < 8; i++){
            bool loop = EnableTile(y-i,x);
            if(!loop)
                break;
        }
        for(int i = 1; i < 8; i++){
            bool loop = EnableTile(y,x+i);
            if(!loop)
                break;
        }
        for(int i = 1; i < 8; i++){
            bool loop = EnableTile(y,x-i);
            if(!loop)
                break;
        }
    }

    public void Knight () {
        EnableTile(y+2,x+1);
        EnableTile(y+2,x-1);
        EnableTile(y-2,x+1);
        EnableTile(y-2,x-1);
    }

    public void Bishop () {
        for(int i = 1; i < 8; i++){
            bool loop = EnableTile(y+i,x+i);
            if(!loop)
                break;
        }
        for(int i = 1; i < 8; i++){
            bool loop = EnableTile(y-i,x-i);
            if(!loop)
                break;
        }
        for(int i = 1; i < 8; i++){
            bool loop = EnableTile(y-i,x+i);
            if(!loop)
                break;
        }
        for(int i = 1; i < 8; i++){
            bool loop = EnableTile(y+i,x-i);
            if(!loop)
                break;
        }
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
                manager.tiles[y,x].center.enabled = true;
                manager.tiles[y,x].center.color = manager.tileEnebled;
                manager.tiles[y,x].enabled = true;
                manager.enabledTiles.Add(manager.tiles[y,x]);
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
