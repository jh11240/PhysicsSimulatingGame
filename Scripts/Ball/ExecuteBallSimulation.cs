using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExecuteBallSimulation : MonoBehaviour
{
    public TMP_InputField Gravity;
    public TMP_InputField Mass;

    private string gravityStr; 
    private string massStr;

    public GameObject ball;
    private Rigidbody2D rigid;
    private Ball shootingBall;
    private void Awake()
    {
        rigid = ball.GetComponent<Rigidbody2D>();
        shootingBall = ball.GetComponent<Ball>();
    }

    public void Simulate()
    {
        gravityStr = Gravity.text;
        massStr = Mass.text;
        if(massStr!="")
        rigid.mass = float.Parse(massStr);
        if(gravityStr!="")
        Physics2D.gravity = Vector2.down*float.Parse(gravityStr);
        rigid.gravityScale = 1;
        shootingBall.Shoot();
    }
}
