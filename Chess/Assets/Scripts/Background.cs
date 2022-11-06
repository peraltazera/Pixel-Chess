using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private float speed;
    private Vector2 position;
    public Vector2 target;
    [Header("Positions")]
    public Vector2 white;
    public Vector2 black;
    public Vector2 defalt;

    void Update()
    {
        float step = speed * Time.deltaTime;
        position = gameObject.transform.position;

        if(Vector2.Distance(transform.position,target) > 0.1f){
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }else{
            this.enabled = false;
        }
    }
}
