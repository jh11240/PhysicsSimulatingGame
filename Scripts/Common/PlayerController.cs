using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public Camera main;
    Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
        }       
        if(Vector2.Distance(mousePosition,transform.position)>0.3f)
            transform.position= Vector2.MoveTowards(transform.position,mousePosition,walkSpeed* Time.deltaTime*walkSpeed);
    }
}
