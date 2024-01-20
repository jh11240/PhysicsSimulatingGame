using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject Ball;
    private Ball gObjBall;
    private void Awake()
    {
        gObjBall = Ball.GetComponent<Ball>();
    }

    public void ResetPos()
    {
        gObjBall.InitPos();
    }
}
