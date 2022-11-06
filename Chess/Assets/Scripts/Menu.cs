using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Manager manager;
    private bool music = true;
    private bool efects = true;
    private bool menu = true;
    private bool pause = false;

    public void Play(){
        manager.SetEfects(1f, manager.start);
        manager.menu.SetActive(false);
        manager.whitePiecesGroup.SetActive(true);
        manager.blackPiecesGroup.SetActive(true);
        menu = false;
        manager.ChangeBack();
        manager.board.SetActive(true);
    }

    public void Pause(){
        if(!menu){
            if(!pause){
                manager.board.SetActive(false);
                pause = true;
                manager.pause.SetActive(true);
                manager.whitePiecesGroup.SetActive(false);
                manager.blackPiecesGroup.SetActive(false);
            }else{
                Resume();
            }
        }
    }

    public void ReturnMenu(){
        manager.board.SetActive(false);
        manager.pause.SetActive(false);
        manager.menu.SetActive(true);
        manager.whitePiecesGroup.SetActive(false);
        manager.blackPiecesGroup.SetActive(false);
        pause = false;
        menu = true;
        manager.back.target = manager.back.defalt;
        manager.back.enabled = true;
    }

    public void Resume(){
        pause = false;
        manager.pause.SetActive(false);
        manager.whitePiecesGroup.SetActive(true);
        manager.blackPiecesGroup.SetActive(true);
        manager.board.SetActive(true);
    }

    public void Restart(){
        manager.SetEfects(1f, manager.start);
        manager.Clear();
        manager.PieceInitial();
        Resume();
    }

    public void Efects(){
        manager.efectsX.SetActive(efects);
        efects = !efects;
        manager.efects.enabled = efects;
    }

    public void Music(){
        manager.musicX.SetActive(music);
        music = !music;
        manager.music.enabled = music;
    }

    public void Return(){
        manager.SetEfects(1f, manager.start);
        manager.Clear();
        manager.PieceInitial();
    }

    public void Quit(){
        Application.Quit();
    }
}
