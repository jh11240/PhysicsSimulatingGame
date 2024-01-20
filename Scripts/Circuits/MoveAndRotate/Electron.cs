using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour
{
    public float speed;
    private CircuitNode curCircuitNode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CircuitNode")
        {
            curCircuitNode= collision.gameObject.GetComponent<CircuitNode>();

        }
    }
}
