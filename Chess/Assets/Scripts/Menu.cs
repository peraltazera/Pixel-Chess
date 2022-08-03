using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Manager manager;

    public void Play(){
        manager.backGroundMenu.SetActive(false);
        manager.whitePiecesGroup.SetActive(true);
        manager.blackPiecesGroup.SetActive(true);
    }
}
