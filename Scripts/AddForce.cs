using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public float remainForce;
    public Vector2 curDir;
    private Stat s;
    private Rigidbody2D rb;

    private void Awake()
    {
        s = GetComponent<Stat>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 initialVec = new Vector2(s.initialXVelocity, s.initialYVelocity);
        remainForce = s.mass*(float)9.81*transform.position.y + (float)0.5 * s.mass * initialVec.magnitude * initialVec.magnitude;
        curDir = initialVec.normalized;
        Debug.Log(remainForce);
        Debug.Log(curDir);
        rb.AddForce(remainForce*curDir,ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //{
        //    curDir = rb.velocity.normalized * Vector2.up;
        //    remainForce -= s.FrictionForce;
        //    Debug.Log(remainForce);
        //    Debug.Log(curDir);
        //    rb.AddForce(remainForce*curDir,ForceMode2D.Impulse);
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log(rb.velocity.normalized);
            curDir = rb.velocity + Vector2.up;
            curDir = curDir.normalized;
            remainForce -= s.FrictionForce;
            Debug.Log(remainForce);
            Debug.Log(curDir);
            rb.AddForce(remainForce * curDir, ForceMode2D.Impulse);
        }
    }
}
