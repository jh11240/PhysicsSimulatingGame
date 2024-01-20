using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initForce;
    public new Vector2 iPos;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        iPos = transform.position;
    }

    public void Shoot()
    {
        rigid.AddForce(new Vector2(3, 0) * initForce,ForceMode2D.Impulse);
    }
    public void InitPos()
    {
        transform.position = iPos;
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0;
        rigid.angularVelocity = 0f;
        transform.rotation = Quaternion.Euler(0,0,0);
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }
}
